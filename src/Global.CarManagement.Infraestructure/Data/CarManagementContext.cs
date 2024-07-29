using Global.CarManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Global.CarManagement.Infraestructure.Data
{
    public class CarManagementContext(DbContextOptions<CarManagementContext> options) : DbContext(options)
    {
        public DbSet<Car> Car { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Photo> Photo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
