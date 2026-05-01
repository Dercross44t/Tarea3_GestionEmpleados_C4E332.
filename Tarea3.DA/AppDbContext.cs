using Microsoft.EntityFrameworkCore;
using Tarea3.MODELS;

namespace Tarea3.DA
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  
        }

        public DbSet<Empleado> DbEmpleados { get; set; }
    }
}
