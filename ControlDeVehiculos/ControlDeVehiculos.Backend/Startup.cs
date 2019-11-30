using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControlDeVehiculos.Backend.Startup))]
namespace ControlDeVehiculos.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
