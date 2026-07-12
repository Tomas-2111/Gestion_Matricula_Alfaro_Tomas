
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionMatricula.Models;
using GestionMatricula.Data;

public class MatriculaController : Controller
{
    private readonly ApplicationDbContext _context;

    public MatriculaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: MATRICULAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Matriculas.ToListAsync());
    }

    // GET: MATRICULAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var matricula = await _context.Matriculas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (matricula == null)
        {
            return NotFound();
        }

        return View(matricula);
    }

    // GET: MATRICULAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MATRICULAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FechaMatricula,EstudianteId,Estudiante,MatriculaCursos")] Matricula matricula)
    {
        if (ModelState.IsValid)
        {
            _context.Add(matricula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(matricula);
    }

    // GET: MATRICULAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var matricula = await _context.Matriculas.FindAsync(id);
        if (matricula == null)
        {
            return NotFound();
        }
        return View(matricula);
    }

    // POST: MATRICULAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,FechaMatricula,EstudianteId,Estudiante,MatriculaCursos")] Matricula matricula)
    {
        if (id != matricula.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(matricula);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(matricula.Id))
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
        return View(matricula);
    }

    // GET: MATRICULAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var matricula = await _context.Matriculas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (matricula == null)
        {
            return NotFound();
        }

        return View(matricula);
    }

    // POST: MATRICULAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var matricula = await _context.Matriculas.FindAsync(id);
        if (matricula != null)
        {
            _context.Matriculas.Remove(matricula);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MatriculaExists(int? id)
    {
        return _context.Matriculas.Any(e => e.Id == id);
    }
}
