using GestionMatricula.Data;
using GestionMatricula.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class CursoController : Controller
{
    private readonly ApplicationDbContext _context;

    public CursoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: CURSOS
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var cursos = await _context.Cursos
            .Include(c => c.Carrera)
            .Include(c => c.Profesor)
            .ToListAsync();

        return View(cursos);
    }

    // GET: CURSOS/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        CargarSelectLists();
        return View();
    }

    // POST: CURSOS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("Nombre,Creditos,ProfesorId,CarreraId")] Curso curso)
    {
        ModelState.Remove("Carrera");
        ModelState.Remove("Profesor");
        ModelState.Remove("MatriculaCursos");

        if (ModelState.IsValid)
        {
            _context.Add(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        CargarSelectLists(curso.CarreraId, curso.ProfesorId);
        return View(curso);
    
}

    // GET: CURSOS/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }

        CargarSelectLists(curso.CarreraId, curso.ProfesorId);
        return View(curso);
    }

    // POST: CURSOS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Creditos,ProfesorId,CarreraId")] Curso curso)
    {
        if (id != curso.Id)
        {
            return NotFound();
        }


        ModelState.Remove("Carrera");
        ModelState.Remove("Profesor");
        ModelState.Remove("MatriculaCursos");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(curso);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(curso.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        CargarSelectLists(curso.CarreraId, curso.ProfesorId);
        return View(curso);
    }

    // GET: CURSOS/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos
            .Include(c => c.Carrera)
            .Include(c => c.Profesor)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (curso == null)
        {
            return NotFound();
        }

        bool tieneEstudiantes = await _context.MatriculasCursos
            .AnyAsync(mc => mc.CursoId == id);

        if (tieneEstudiantes)
        {
            ViewBag.MensajeError = "No se puede eliminar porque hay estudiantes activos en el curso.";
        }

        return View(curso);
    }

    // POST: CURSOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
     
        bool tieneEstudiantes = await _context.MatriculasCursos
            .AnyAsync(mc => mc.CursoId == id);

        if (tieneEstudiantes)
        {
            TempData["MensajeError"] = "No se puede eliminar porque hay estudiantes activos en el curso.";
            return RedirectToAction(nameof(Delete), new { id = id });
        }

        var curso = await _context.Cursos.FindAsync(id);
        if (curso != null)
        {
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
    private bool CursoExists(int id)
    {
        return _context.Cursos.Any(e => e.Id == id);
    }

    private void CargarSelectLists(int? carreraId = null, int? profesorId = null)
    {
        ViewBag.CarreraId = new SelectList(_context.Carreras, "Id", "Nombre", carreraId);
        ViewBag.ProfesorId = new SelectList(_context.Profesores, "Id", "Nombre", profesorId);
    }
}