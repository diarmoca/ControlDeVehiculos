

namespace ControlDeVehiculos.ViewModels
{
    using ControlDeVehiculos.Common.Models;
    using ControlDeVehiculos.Services;
    using ControlDeVehiculos.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class VehiclesItemViewModel : Vehicle
    {
        #region Attributes
        private ApiService apiService;
        #endregion

        #region Constructors

        public VehiclesItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion
        #region Commands

        public ICommand EditVehicleCommand
        {
            get
            {
                return new RelayCommand(EditVehicle);
            }
        }

        private async void EditVehicle()
        {
            MainViewModel.GetInstance().EditVehicle = new EditVehicleViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditVehiclePage());
        }

        public ICommand DeleteVehicleCommand
         {
             get
             {
                 return new RelayCommand(DeleteVehicle);
             }
         }
        #endregion
        private async void DeleteVehicle()
         {
           var answer = await Application.Current.MainPage.DisplayAlert
               ("Confirmación",
               "Desea borrar el registro", 
               "Si", 
               "No");

           if(!answer)
           {
               return;
           }

           var connection = await this.apiService.CheckConnection();

           if (!connection.IsSuccess)
           {
               await Application.Current.MainPage.DisplayAlert("Error",
                   connection.Message
                   , "Acceptar");
               return;
           }

           var url = Application.Current.Resources["UrlAPI"].ToString();
           var prefix = Application.Current.Resources["UrlPrefix"].ToString();
           var vController = Application.Current.Resources["UrlVehiclesController"].ToString();
           var response = await this.apiService.Delete(url, prefix, vController, this.Id);
           if (!response.IsSuccess)
           {
               await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
               return;
           }

           var vehiclesViewModel = VehiclesViewModel.GetInstance();
           var deleteVehicle = vehiclesViewModel.Vehicles.Where(v => v.Id == this.Id).FirstOrDefault();

           if(deleteVehicle != null)
           {
               vehiclesViewModel.Vehicles.Remove(deleteVehicle);
           }

         }
    }
}
