using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using SURA.Models;
using SURA.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.ElementosDashboard.PasarelaPago
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallePago : ContentView
    {
        public DetallePago()
        {
            InitializeComponent();
            _ = CargarItems();
        }

        public async Task CargarItems()
        {
            try
            {

                if (App.plataformaPagoShouldInitialize)
                {
                    CargandoPopUp _cargandoPopup = new CargandoPopUp();
                    await PopupNavigation.Instance.PushAsync(_cargandoPopup);

                    ServicioPagos servicioPagos = new ServicioPagos();
                    await servicioPagos.ObtenerDatos(App.modeloDashboard.Identificacion);
                }

                ValidateNextButtonColor();
                PolizasListview.ItemsSource = App.modeloPlataformaPago.PolizasAPagar;

            }
            catch (Exception ex)
            {
                _ = App.Current.MainPage.DisplayAlert("SURA", "Cargar Items: " + ex.Message, "Ok");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private bool ValidateNextButtonColor()
        {
            var listPolizas = App.modeloPlataformaPago.PolizasAPagar;

            if (listPolizas.Where(w => w.Selected).Count() > 0 && listPolizas.Where(w => w.PagoMinimoValidation != "").Count() == 0)
            {
                frmSiguiente.BackgroundColor = Color.FromHex("#0033A0");
                return true;
            }
            else
            {
                frmSiguiente.BackgroundColor = Color.FromHex("#5F5F5F");
                return false;
            }
        }


        void Selected_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (App.modeloPlataformaPago != null)
            {
                totalDescuento.Text = App.modeloPlataformaPago.DescuentoTotal;
                totalMonto.Text = App.modeloPlataformaPago.PagoTotal;
                ValidateNextButtonColor();
            }
        }

        void Monto_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var listText = new List<char>();
            ModelPolizaAPagar model = (ModelPolizaAPagar)(sender as Entry).BindingContext;

            if (e.NewTextValue == null)
                return;

            foreach (var c in e.NewTextValue.ToList())
            {
                if (Char.IsDigit(c))
                {
                    listText.Add(c);
                }
            }
            while (listText.Contains('.'))
            {
                listText.Remove('.');
            }
            
            while(listText.Count < 3)
            {
                listText.Insert(0, '0');
            }
            listText.Insert(listText.Count - 2, '.');
            var value = string.Join("", listText);
            double d = Double.Parse(value);
            if (d > model.Saldo)
                d = model.Saldo;

            string s = $"{d:0.00}";
            if (s != e.NewTextValue)
                ((Entry)sender).Text = s;
            
            if (App.modeloPlataformaPago != null)
            {
                totalMonto.Text = App.modeloPlataformaPago.PagoTotal;
                totalDescuento.Text = App.modeloPlataformaPago.DescuentoTotal;
                ValidateNextButtonColor();
            }
        }

        private void Pagar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(ValidateNextButtonColor())
                    App.objContenedor.MostrarVista("MiPago2");
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Cargar Items: " + ex.Message, "Ok");
            }
        }



    }
}