

namespace ControlDeVehiculos.Backend.Models
{
    using System.Web;
    using Common.Models;

    public class VehicleView : Vehicle
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}