using Microsoft.EntityFrameworkCore;
using TecnoGasHogar.Models;

namespace TecnoGasHogar.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<SolicitudServicio> SolicitudesServicio { get; set; }
}
