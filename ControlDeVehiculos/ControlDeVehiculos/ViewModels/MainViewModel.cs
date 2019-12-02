

namespace ControlDeVehiculos.ViewModels
{
    using System.Windows.Input;
    using ControlDeVehiculos.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    public class MainViewModel
    {
    
        #region Properties

        public EditVehicleViewModel EditVehicle { get; set; }
        public NewVehicleViewModel NewVehicle { get; set; }
        public VehiclesViewModel Vehicles { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Vehicles = new VehiclesViewModel();
        }

        #endregion

        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
        #region Commands
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
        #endregion
    }
}
