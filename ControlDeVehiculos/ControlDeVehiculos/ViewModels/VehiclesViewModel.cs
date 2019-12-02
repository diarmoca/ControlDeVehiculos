namespace ControlDeVehiculos.ViewModels
{
    using ControlDeVehiculos.Common.Models;
    using ControlDeVehiculos.Services;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class VehiclesViewModel : BaseViewModel
    {


        #region Attributes
        private ApiService apiService;

        private bool isRefreshing;

        private ObservableCollection<VehiclesItemViewModel> vehicles;
        #endregion

        #region Properties

        public List<Vehicle> MyVehicles { get; set; }
        public ObservableCollection<VehiclesItemViewModel> Vehicles
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


        #endregion

        #region Constructors
        public VehiclesViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadVehicles();
        } 
        #endregion

        #region Singleton

        private static VehiclesViewModel instance;

        public static VehiclesViewModel GetInstance()
        {
            if (instance == null)
            {
                return new VehiclesViewModel();
            }

            return instance;
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

            this.MyVehicles = (List<Vehicle>)response.Result;
            this.RefreshList();
            this.IsRefreshing = false;

        }

        public void RefreshList()
        {
            var myListVehiclesItemViewModel = this.MyVehicles.Select(v => new VehiclesItemViewModel
            {
                Id = v.Id,
                Marca = v.Marca,
                Tipo = v.Tipo,
                Color = v.Color,
                Modelo = v.Modelo,
                NoPlacas = v.NoPlacas,
                NoSerie = v.NoSerie,
                Resguardante = v.Resguardante,
                Cargo = v.Cargo,
                Adscripcion = v.Adscripcion,
                NoAvPrevia = v.NoAvPrevia,
                NoExpediente = v.NoExpediente,
                Origen = v.Origen,
                FechaInicio = v.FechaInicio,
                FechaFinal = v.FechaFinal,
                ImageArray = v.ImageArray,
                ImagePath = v.ImagePath,
            });

            this.Vehicles = new ObservableCollection<VehiclesItemViewModel>(
                myListVehiclesItemViewModel.OrderBy(v => v.FechaFinal));
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
