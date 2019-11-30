using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControlDeVehiculos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewVehiclePage : ContentPage
    {
        public NewVehiclePage()
        {
            InitializeComponent();
            dtpFechaInicial.MinimumDate = DateTime.UtcNow;
            dtpFechaInicial.MaximumDate = DateTime.UtcNow.Date.AddDays(2);
            dtpFechaFinal.MinimumDate = DateTime.UtcNow.Date.AddMonths(6);
            dtpFechaFinal.MaximumDate = DateTime.UtcNow.Date.AddMonths(7);
            txtModelo.MaxLength = 4;
            
        }

        private void dtpFechaInicial_DateSelected(object sender, DateChangedEventArgs e)
        {
            dtpFechaFinal.Date = dtpFechaInicial.Date.AddMonths(6);
        }
    }
}