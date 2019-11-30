

namespace ControlDeVehiculos.Backend.Models
{
    using Domain.Models;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<ControlDeVehiculos.Common.Models.Vehicle> Vehicles { get; set; }
    }
}