

namespace ControlDeVehiculos.ViewModels
{
    using System;
    using System.Windows.Input;
    using Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using ControlDeVehiculos.Common.Models;

    public class NewVehicleViewModel : BaseViewModel
    {
        #region Attributes
       // private MediaFile file;

        private ImageSource imageSource;

        private ApiService apiService;

        private bool isRunning;

        private bool isEnabled;

        #endregion

        #region Properties
        public string Marca { get; set; }

        public string Tipo { get; set; }

        public string Color { get; set; }

        public int Modelo { get; set; }

        public string NoPlacas { get; set; }

        public string NoSerie { get; set; }

        public string NombreResguardante { get; set; }

        public string CargoDelResguardante { get; set; }

        public string AdscripcionResguardante { get; set; }

        public string NoAvePrevia { get; set; }

        public string NoExpdienteDBA { get; set; }

        public string Origen { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaTermina { get; set; }

        public string ImagePath { get; set; }

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

        public NewVehicleViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.ImageSource = "VehiclesDBA";

        }

        #endregion

        #region Commands

       /* public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }*/

        /*private async void ChangeImage()
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
                        CustomPhotoSize = 75
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
                }
                );
            }
        }*/

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Marca))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar la marca del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Tipo))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Tipo del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Color))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Color del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Modelo.ToString()) || this.Modelo == 0 || this.Modelo.ToString().Length < 4)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Modelo correcto del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.NoPlacas))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe el No. de placas del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.NoSerie))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el No. de serie del Vehículo", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.NombreResguardante))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Nombre Completo del resguardante", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.CargoDelResguardante))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el cargo o puesto del Resguardante", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.AdscripcionResguardante))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar la Adscripción del resguardante", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.NoAvePrevia))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar No Averiguación Previa", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.NoExpdienteDBA))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar No Expendiente DBA", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Origen))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar el Origen", "Aceptar");
                return;
            }

            if (FechaTermina <= FechaInicio )
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La Fecha Final no puede ser Menor o Igual a la Fecha de Inicio", "Aceptar");
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

            /*byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelpers.ReadFully(this.file.GetStream());
            }*/
            var vehicle = new Vehicle
            {
                //id = "0000000258", //(apiService.tr + 1).ToString("0000000000"),
                Marca = this.Marca,
                Tipo = this.Tipo,
                Color = this.Color,
                Modelo = this.Modelo,
                NoPlacas = this.NoPlacas,
                NoSerie = this.NoSerie,
                Resguardante = this.NombreResguardante,
                Cargo = this.CargoDelResguardante,
                Adscripcion = this.AdscripcionResguardante,
                NoAvPrevia = this.NoAvePrevia,
                NoExpediente = this.NoExpdienteDBA,
                Origen = this.Origen,
                FechaInicio = this.FechaInicio,
                FechaFinal = this.FechaTermina,
              //  ImageArray = imageArray,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var vController = Application.Current.Resources["UrlVehiclesController"].ToString();
            var response = await this.apiService.Post(url, prefix, vController, vehicle);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();

            /* var newVehicle = (Vehicle)response.Result;
             var vehiclesViewModel = VehiclesViewModel.GetInstance();
             vehiclesViewModel.MyVehicles.Add(newVehicle);
             vehiclesViewModel.RefreshList();*/



        }

        #endregion
    }
}
