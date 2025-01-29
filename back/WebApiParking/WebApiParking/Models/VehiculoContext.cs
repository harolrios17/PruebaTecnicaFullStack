using Microsoft.EntityFrameworkCore;

namespace WebApiParking.Models
{
    public class VehiculoContext : DbContext
    {
        public VehiculoContext(DbContextOptions<VehiculoContext> options) : base(options) { }

        public DbSet<Vehiculo> Vehiculos { get; set; }
    }
}
