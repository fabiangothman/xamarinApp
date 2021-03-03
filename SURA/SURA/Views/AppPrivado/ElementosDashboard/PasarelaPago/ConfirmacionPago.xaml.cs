using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using SURA.Models;
using SURA.Services;
using Xamarin.Forms;

namespace SURA.Views.AppPrivado.ElementosDashboard.PasarelaPago
{
    public partial class ConfirmacionPago : ContentView
    {
        public ConfirmacionPago()
        {
            InitializeComponent();

            CargarDatos();
        }

        public void CargarDatos()
        {
            ListAPagar.ItemsSource = App.modeloPlataformaPago.PolizasAPagar.Where(w => w.Selected).ToList();
            totalMontoConfirmacion.Text = App.modeloPlataformaPago.PagoTotal;
        }



        void Pagar_Clicked(System.Object sender, System.EventArgs e)
        {
            _ = PagarPolizas();
        }

        private async Task PagarPolizas()
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);

                await new ServicioPagos().PagarPolizas(EnviarCorreo.IsChecked);
            }
            catch(Exception e)
            {
                _ = App.Current.MainPage.DisplayAlert("", e.Message, "Ok");
                await PopupNavigation.Instance.PopAllAsync();
            }
        }
    }
}
