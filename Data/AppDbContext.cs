using Microsoft.EntityFrameworkCore;
using ConsultorioApi.Models;

namespace ConsultorioApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
    }
}