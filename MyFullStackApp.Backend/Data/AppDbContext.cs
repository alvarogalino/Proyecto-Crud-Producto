using Microsoft.EntityFrameworkCore;
using MyFullStackApp.Backend.Models;

namespace MyFullStackApp.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet representa una colección de entidades en el contexto o la base de datos.
        // Aquí, 'Products' será el nombre de nuestra tabla en SQL Server.
        public DbSet<Product> Products { get; set; }

        // Puedes configurar el modelo aquí si necesitas más detalles,
        // por ejemplo, para índices o relaciones más complejas.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ejemplo de configuración adicional (opcional):
            // modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
        }
    }
}
