using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TecnoGasHogar.Data;
using TecnoGasHogar.Models;

namespace TecnoGasHogar.Controllers;

public class SolicitudServicioController : Controller
{
    private static readonly string[] TiposServicio =
    {
        "Instalación", "Mantenimiento", "Revisión", "Fuga"
    };

    private readonly AppDbContext _context;

    public SolicitudServicioController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var solicitudes = await _context.SolicitudesServicio
            .OrderByDescending(s => s.FechaRegistro)
            .ToListAsync();

        ViewBag.TotalSolicitudes = solicitudes.Count;
        ViewBag.ConteoPorTipo = TiposServicio
            .Select(tipo => (Tipo: tipo, Cantidad: solicitudes.Count(s => s.TipoServicio == tipo)))
            .ToList();

        return View(solicitudes);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.TiposServicio = new SelectList(TiposServicio);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SolicitudServicio solicitud)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.TiposServicio = new SelectList(TiposServicio, solicitud.TipoServicio);
            return View(solicitud);
        }

        _context.Add(solicitud);
        await _context.SaveChangesAsync();

        TempData["MensajeExito"] = "Solicitud registrada correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
