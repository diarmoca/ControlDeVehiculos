namespace ControlDeVehiculos.ViewModels
{
    using ControlDeVehiculos.Common.Models;
    using ControlDeVehiculos.Services;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class VehiclesViewModel : BaseViewModel
    {


        #region Attributes
        private ApiService apiService;

        private bool isRefreshing;

        private ObservableCollection<Vehicle> vehicles;
        #endregion

        #region Properties
        public ObservableCollection<Vehicle> Vehicles
        {
            get { return this.vehicles; }
            set { this.SetValue(ref this.vehicles, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                this.SetValue(ref this.isRefreshing, value);
            }
        }
        public VehiclesViewModel()
        {
          //  instance = this;
            this.apiService = new ApiService();
            this.LoadVehicles();
        }

        #endregion

        #region Methods
        private async void LoadVehicles()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var vController = Application.Current.Resources["UrlVehiclesController"].ToString();
            var response = await this.apiService.GetList<Vehicle>(url, prefix, vController);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var list = (List<Vehicle>)response.Result;
            this.Vehicles = new ObservableCollection<Vehicle>(list);
            this.IsRefreshing = false;

        }
        #endregion


        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadVehicles);
            }
        }

        #endregion
    }
}
