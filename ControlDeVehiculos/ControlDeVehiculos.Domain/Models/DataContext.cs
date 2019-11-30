namespace ControlDeVehiculos.Domain.Models
{
    using ControlDeVehiculos.Common.Models;
    using System.Data.Entity;
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
