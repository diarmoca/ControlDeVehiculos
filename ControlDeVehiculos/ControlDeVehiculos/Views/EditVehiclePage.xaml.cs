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
    public partial class EditVehiclePage : ContentPage
    {
        public EditVehiclePage()
        {
            InitializeComponent();
            txtModelo.MaxLength = 4;
        }

        private void dtpFechaInicial_DateSelected(object sender, DateChangedEventArgs e)
        {
            dtpFechaFinal.Date = dtpFechaInicial.Date.AddMonths(6);
        }
    }
}