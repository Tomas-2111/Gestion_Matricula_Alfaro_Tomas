using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GestionMatricula.Models;
using GestionMatricula.Data;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class MatriculaController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public MatriculaController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Matricula/MatricularCurso
    public async Task<IActionResult> MatricularCurso()
    {
        var userId = _userManager.GetUserId(User);

      
        var estudiante = await _context.Estudiantes
            .Include(e => e.Carrera)
            .FirstOrDefaultAsync(e => e.UserId == userId);

        if (estudiante == null)
        {
            TempData["MensajeError"] = "No tienes un perfil de estudiante registrado.";
            return RedirectToAction("Index", "Home");
        }

       
        var cursosCarrera = await _context.Cursos
            .Include(c => c.Profesor)
            .Where(c => c.CarreraId == estudiante.CarreraId)
            .ToListAsync();


        var cursosYaMatriculados = await _context.MatriculasCursos
            .Include(mc => mc.Matricula)
            .Where(mc => mc.Matricula.EstudianteId == estudiante.Id)
            .Select(mc => mc.CursoId)
            .ToListAsync();

        ViewBag.CursosYaMatriculados = cursosYaMatriculados;
        ViewBag.Estudiante = estudiante;

        return View(cursosCarrera);
    }

    // POST: Matricula/MatricularCurso
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MatricularCurso(List<int> cursosSeleccionados)
    {
        var userId = _userManager.GetUserId(User);
        var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.UserId == userId);

        if (estudiante == null)
        {
            return RedirectToAction("Index", "Home");
        }

        if (cursosSeleccionados == null || !cursosSeleccionados.Any())
        {
            TempData["MensajeErrorM"] = "Debe seleccionar al menos un curso para matricular.";
            return RedirectToAction(nameof(MatricularCurso));
        }

    
        var nuevaMatricula = new Matricula
        {
            EstudianteId = estudiante.Id,
            FechaMatricula = DateTime.Now
        };

        _context.Matriculas.Add(nuevaMatricula);
        await _context.SaveChangesAsync(); 


        foreach (var cursoId in cursosSeleccionados)
        {
            var detalle = new MatriculaCurso
            {
                MatriculaId = nuevaMatricula.Id,
                CursoId = cursoId
            };
            _context.MatriculasCursos.Add(detalle);
        }

        await _context.SaveChangesAsync();

        TempData["MensajeExitoM"] = "Cursos matriculados exitosamente!";
        return RedirectToAction("Panel", "Estudiante");
    }
}