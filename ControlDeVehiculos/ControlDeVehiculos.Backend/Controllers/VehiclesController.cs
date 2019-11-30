


namespace ControlDeVehiculos.Backend.Controllers
{
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ControlDeVehiculos.Backend.Models;
    using ControlDeVehiculos.Common.Models;
    using System.Linq;
    using ControlDeVehiculos.Backend.Helpers;

    public class VehiclesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Vehicles
        public async Task<ActionResult> Index()
        {
            return View(await this.db.Vehicles.OrderBy(p => p.FechaFinal).ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<ActionResult> Create([Bind(Include = "Id,Marca,ImagePath,Tipo,Color,Modelo,NoPlacas,NoSerie,Resguardante,Cargo,Adscripcion,NoAvPrevia,NoExpediente,Origen,FechaInicio,FechaFinal")] Vehicle vehicle)
        public async Task<ActionResult> Create(VehicleView view)
        {
            var pic = string.Empty;
            var folder = "~/Content/Vehicles";

            if (view.ImageFile != null)
            {
                pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                pic = $"{ folder}/{ pic}";
            }

            var vehicle = this.ToVehicle(view, pic);

            db.Vehicles.Add(vehicle);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

            return View(view);
        }
        private Vehicle ToVehicle(VehicleView view, string pic)
        {
            return new Vehicle
            {
                Id = view.Id,
                Marca = view.Marca,
                Tipo = view.Tipo,
                Color = view.Color,
                Modelo = view.Modelo,
                NoPlacas = view.NoPlacas,
                NoSerie = view.NoSerie,
                Resguardante = view.Resguardante,
                Cargo = view.Cargo,
                Adscripcion = view.Adscripcion,
                NoAvPrevia = view.NoAvPrevia,
                NoExpediente = view.NoExpediente,
                Origen = view.Origen,
                FechaInicio = view.FechaInicio,
                FechaFinal = view.FechaFinal,
                ImagePath = pic,
            };
        }

        // GET: Vehicles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = await db.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return HttpNotFound();
            }
            var view = this.ToView(vehicle);
            return View(view);
        }

        private VehicleView ToView(Vehicle vehicle)
        {
            return new VehicleView
            {
                Id = vehicle.Id,
                Marca = vehicle.Marca,
                Tipo = vehicle.Tipo,
                Color = vehicle.Color,
                Modelo = vehicle.Modelo,
                NoPlacas = vehicle.NoPlacas,
                NoSerie = vehicle.NoSerie,
                Resguardante = vehicle.Resguardante,
                Cargo = vehicle.Cargo,
                Adscripcion = vehicle.Adscripcion,
                NoAvPrevia = vehicle.NoAvPrevia,
                NoExpediente = vehicle.NoExpediente,
                Origen = vehicle.Origen,
                FechaInicio = vehicle.FechaInicio,
                FechaFinal = vehicle.FechaFinal,
                ImagePath = vehicle.ImagePath,



            };
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Marca,ImagePath,Tipo,Color,Modelo,NoPlacas,NoSerie,Resguardante,Cargo,Adscripcion,NoAvPrevia,NoExpediente,Origen,FechaInicio,FechaFinal")] Vehicle vehicle)
        public async Task<ActionResult> Edit(VehicleView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Vehicles";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{ folder}/{ pic}";
                }

                var vehicle = this.ToVehicle(view, pic);
                db.Entry(vehicle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: Vehicles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            db.Vehicles.Remove(vehicle);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
