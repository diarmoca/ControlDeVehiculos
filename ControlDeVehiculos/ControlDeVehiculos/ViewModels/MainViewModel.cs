

namespace ControlDeVehiculos.ViewModels
{
    using System.Windows.Input;
    using ControlDeVehiculos.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    public class MainViewModel
    {
        public NewVehicleViewModel NewVehicle { get; set; }
        public VehiclesViewModel Vehicles { get; set; }

        public MainViewModel()
        {
            this.Vehicles = new VehiclesViewModel();
        }

        public ICommand NewVehicleCommand
        {
            get
            {
                return new RelayCommand(GoToNewVehicle);
            }
        }

        private async void GoToNewVehicle()
        {
            this.NewVehicle = new NewVehicleViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new NewVehiclePage());
        }
    }
}
