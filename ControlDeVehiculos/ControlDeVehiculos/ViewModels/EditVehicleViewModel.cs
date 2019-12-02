namespace ControlDeVehiculos.ViewModels
{

    using Common.Models;
    using ControlDeVehiculos.Helpers;
    using ControlDeVehiculos.Services;
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditVehicleViewModel : BaseViewModel
    {
        #region Attributes
        private Vehicle vehicle;

        private MediaFile file;

        private ImageSource imageSource;

        private ApiService apiService;

        private bool isRunning;

        private bool isEnabled;
        #endregion

        #region Properties

        public Vehicle Vehicle
        {
            get
            {
                return this.vehicle;
            }
            set { this.SetValue(ref this.vehicle, value); }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                this.SetValue(ref this.isRunning, value);
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.SetValue(ref this.isEnabled, value);
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
            set
            {
                this.SetValue(ref this.imageSource, value);
            }
        }
        #endregion




        #region Constructors
        public EditVehicleViewModel(Vehicle vehicle)
        {
            this.vehicle = vehicle;
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.ImageSource = vehicle.ImageFullPath;
        }
        #endregion

        #region Commands

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                         "¿De donde desea obtener la Imagen?",
                         "Cancelar",
                         null,
                         "Desde Galeria",
                         "Tomar Foto");

            if (source == "Cancelar")
            {
                this.file = null;
                return;
            }

            if (source == "Tomar Foto")
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                        //CustomPhotoSize = 
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Vehicle.Marca))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar la marca del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.Tipo))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Tipo del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.Color))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Color del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Vehicle.Modelo.ToString()) || this.Vehicle.Modelo == 0 || this.Vehicle.Modelo.ToString().Length < 4)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Modelo correcto del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.NoPlacas))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe el No. de placas del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.NoSerie))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el No. de serie del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.Resguardante))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Nombre Completo del resguardante", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.Cargo))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el cargo o puesto del Resguardante", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.Adscripcion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar la Adscripción del resguardante", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.NoAvPrevia))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar No Averiguación Previa", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.NoExpediente))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar No Expendiente DBA", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Vehicle.Origen))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Origen", "Aceptar");
                return;
            }


            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelpers.ReadFully(this.file.GetStream());
                this.Vehicle.ImageArray = imageArray;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var vController = Application.Current.Resources["UrlVehiclesController"].ToString();
            var response = await this.apiService.Put(url, prefix, vController,this.Vehicle,this.Vehicle.Id);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var newVehicle = (Vehicle)response.Result;
            var vehiclesViewModel = VehiclesViewModel.GetInstance();

            var oldVehicle = vehiclesViewModel.MyVehicles.Where(v => v.Id == this.Vehicle.Id).FirstOrDefault();
            if(oldVehicle != null)
            {
                vehiclesViewModel.MyVehicles.Remove(oldVehicle);
            }

            vehiclesViewModel.MyVehicles.Add(newVehicle);
            vehiclesViewModel.RefreshList();

            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();





        }

        #endregion
    }
}
