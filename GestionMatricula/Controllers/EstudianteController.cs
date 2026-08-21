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


    public async Task<IActionResult> Panel()
    {
        var userId = _userManager.GetUserId(User);


        var estudiante = await _context.Estudiantes
            .Include(e => e.Carrera)
            .FirstOrDefaultAsync(e => e.UserId == userId);

        if (estudiante == null)
        {
            TempData["MensajeError"] = "No tienes una carrera matriculada aún.";
            return RedirectToAction("IndexStudent", "Carrera");
        }

  
        var cursosMatriculados = await _context.MatriculasCursos
            .Include(mc => mc.Curso)
                .ThenInclude(c => c.Profesor)
            .Include(mc => mc.Matricula)
            .Where(mc => mc.Matricula.EstudianteId == estudiante.Id)
            .ToListAsync();

        ViewBag.CursosMatriculados = cursosMatriculados;

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

        ModelState.Remove("UserId");
        ModelState.Remove("User");
        ModelState.Remove("Carrera");

        if (ModelState.IsValid)
        {
            _context.Add(estudiante);
            await _context.SaveChangesAsync();

            TempData["MensajeExito"] = "¡Te has matriculado exitosamente en la carrera!";

            return RedirectToAction("IndexStudent", "Carrera");
        }

        var carrera = await _context.Carreras.FindAsync(estudiante.CarreraId);
        ViewBag.NombreCarrera = carrera?.Nombre;

        return View(estudiante);
    }

    
    private bool EstudianteExists(int id)
    {
        return _context.Estudiantes.Any(e => e.Id == id);
    }
}