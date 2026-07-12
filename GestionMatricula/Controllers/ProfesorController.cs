
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionMatricula.Models;
using GestionMatricula.Data;

public class ProfesorController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProfesorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PROFESORS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Profesores.ToListAsync());
    }

    // GET: PROFESORS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var profesor = await _context.Profesores
            .FirstOrDefaultAsync(m => m.Id == id);
        if (profesor == null)
        {
            return NotFound();
        }

        return View(profesor);
    }

    // GET: PROFESORS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PROFESORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Especialidad,GradoAcademico,Cursos")] Profesor profesor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(profesor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(profesor);
    }

    // GET: PROFESORS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var profesor = await _context.Profesores.FindAsync(id);
        if (profesor == null)
        {
            return NotFound();
        }
        return View(profesor);
    }

    // POST: PROFESORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Especialidad,GradoAcademico,Cursos")] Profesor profesor)
    {
        if (id != profesor.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(profesor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(profesor.Id))
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
        return View(profesor);
    }

    // GET: PROFESORS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var profesor = await _context.Profesores
            .FirstOrDefaultAsync(m => m.Id == id);
        if (profesor == null)
        {
            return NotFound();
        }

        return View(profesor);
    }

    // POST: PROFESORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var profesor = await _context.Profesores.FindAsync(id);
        if (profesor != null)
        {
            _context.Profesores.Remove(profesor);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProfesorExists(int? id)
    {
        return _context.Profesores.Any(e => e.Id == id);
    }
}
