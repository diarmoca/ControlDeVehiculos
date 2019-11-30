using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeVehiculos.Domain.Models
{
    using System.Data.Entity;
    using ControlDeVehiculos.Common.Models;
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<ControlDeVehiculos.Common.Models.Vehicle> Vehicles { get; set; }
    }
}
