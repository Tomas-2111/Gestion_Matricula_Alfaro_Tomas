using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionMatricula.Models;
using GestionMatricula.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

[Authorize]
public class EstudianteController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public EstudianteController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: ESTUDIANTES
    public async Task<IActionResult> Index()
    {
        var estudiantes = await _context.Estudiantes
            .Include(e => e.Carrera)
            .Include(e => e.User)
            .ToListAsync();

        return View(estudiantes);
    }

    // GET: ESTUDIANTES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes
            .Include(e => e.Carrera)
            .Include(e => e.User)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (estudiante == null)
        {
            return NotFound();
        }

        return View(estudiante);
    }

    // GET: ESTUDIANTES/Create?carreraId=5
    public async Task<IActionResult> Create(int? carreraId)
    {
        if (carreraId == null)
        {
            return RedirectToAction("IndexStudent", "Carrera");
        }

        var userId = _userManager.GetUserId(User);

        // Validación: Comprobar si el usuario ya tiene un perfil de estudiante
        var estudianteExistente = await _context.Estudiantes.AnyAsync(e => e.UserId == userId);
        if (estudianteExistente)
        {
            TempData["MensajeError"] = "El usuario ya cuenta con una carrera asignada.";
            return RedirectToAction("IndexStudent", "Carrera");
        }

        var carrera = await _context.Carreras.FindAsync(carreraId);
        if (carrera == null)
        {
            return NotFound();
        }

        var estudiante = new Estudiante
        {
            CarreraId = carreraId.Value,
            UserId = userId
        };

        ViewBag.NombreCarrera = carrera.Nombre;

        return View(estudiante);
    }

    // POST: ESTUDIANTES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Cedula,CarreraId")] Estudiante estudiante)
    {
        var currentUserId = _userManager.GetUserId(User);
        estudiante.UserId = currentUserId;

        // Limpiamos las navegaciones del ModelState para que no causen falla en IsValid
        ModelState.Remove("UserId");
        ModelState.Remove("User");
        ModelState.Remove("Carrera");

        if (ModelState.IsValid)
        {
            _context.Add(estudiante);
            await _context.SaveChangesAsync();

            // Guardamos el mensaje en TempData para leerlo en el redireccionamiento
            TempData["MensajeExito"] = "¡Te has matriculado exitosamente en la carrera!";

            // Redirige a la vista IndexStudent del controlador Carrera
            return RedirectToAction("IndexStudent", "Carrera");
        }

        // Si no es válido, se queda en la misma vista
        var carrera = await _context.Carreras.FindAsync(estudiante.CarreraId);
        ViewBag.NombreCarrera = carrera?.Nombre;

        return View(estudiante);
    }

    // GET: ESTUDIANTES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes
            .Include(e => e.Carrera)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (estudiante == null)
        {
            return NotFound();
        }

        ViewBag.NombreCarrera = estudiante.Carrera?.Nombre;
        return View(estudiante);
    }

    // POST: ESTUDIANTES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cedula,CarreraId,UserId")] Estudiante estudiante)
    {
        if (id != estudiante.Id)
        {
            return NotFound();
        }

        ModelState.Remove("User");
        ModelState.Remove("Carrera");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(estudiante);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(estudiante.Id))
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

        var carrera = await _context.Carreras.FindAsync(estudiante.CarreraId);
        ViewBag.NombreCarrera = carrera?.Nombre;

        return View(estudiante);
    }

    // GET: ESTUDIANTES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes
            .Include(e => e.Carrera)
            .Include(e => e.User)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (estudiante == null)
        {
            return NotFound();
        }

        return View(estudiante);
    }

    // POST: ESTUDIANTES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante != null)
        {
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool EstudianteExists(int id)
    {
        return _context.Estudiantes.Any(e => e.Id == id);
    }
}