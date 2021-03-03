using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SURA.Models;
using SURA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.ElementosDashboard.PasarelaPago
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidarTarjeta : ContentView
    {
        public ValidarTarjeta()
        {
            InitializeComponent();
            _ = CargarItems();
        }

        public async Task CargarItems()
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);

                ServicioTarjetas servicioTarjetas = new ServicioTarjetas();
                await servicioTarjetas.ObtenerListaTarjetas(App.modeloDashboard.Identificacion);

                BindableLayout.SetItemsSource(TarjetasListView, App.modeloPlataformaPago.TarjetasCreadas);

                lblTotalCardP.Text = App.modeloPlataformaPago.PagoTotal;
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

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var AgregarPopup = new AgregarTarjetaPopup();
                await PopupNavigation.Instance.PushAsync(AgregarPopup);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "Ok");
            }
        }

        void CollectionItem_Tapped(System.Object sender, System.EventArgs e)
        {
            ModelTarjeta model = (ModelTarjeta)(sender as Frame).BindingContext;
            App.modeloPlataformaPago.TokenTarjeta = model.Token;

            FrmCCVBottom.IsVisible = true;
            CCVListEntry.Text = "";
            ccvError.Text = " ";

        }

        void DeleteItem_Tapped(System.Object sender, System.EventArgs e)
        {
            ModelTarjeta model = (ModelTarjeta)(sender as Frame).BindingContext;
            _= DeleteItem(model);
        }

        private async Task DeleteItem(ModelTarjeta model)
        {
            try
            {
                var response = await App.Current.MainPage.DisplayAlert("Remover tarjeta de crédito", $"¿Desea remover la tarjeta de crédito {model.TarjetaEnmascaradaMasked}?", "Si", "No");
                if (!response)
                {
                    return;
                }

                Dictionary<string, string> deleteParameters = new Dictionary<string, string>()
                {
                    {"identificacion", model.Identificacion },
                    {"token", model.Token }
                };
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);

                ServicioTarjetas servicioTarjetas = new ServicioTarjetas();
                await servicioTarjetas.EliminarTarjeta(deleteParameters);

                App.modeloPlataformaPago.TarjetasCreadas.Remove(model);
                
            }
            catch (Exception ex)
            {
                _ = App.Current.MainPage.DisplayAlert("", ex.Message, "Ok");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        void DismissCCVBottomFrame(System.Object sender, System.EventArgs e)
        {
            ccvError.Text = " ";
            
            CCVListEntry.Text = "";
            FrmCCVBottom.IsVisible = false;
        }

        void AceptarCCVBottom(System.Object sender, System.EventArgs e)
        {
            if(CCVListEntry.Text.Length < 3)
            {
                
                ccvError.Text = "El código CVV debe tener de 3 a 4 dígitos";
                return;
            }

            ccvError.Text = " ";
            
            App.modeloPlataformaPago.CVVTarjeta = CCVListEntry.Text;
            CCVListEntry.Text = "";
            FrmCCVBottom.IsVisible = false;

            App.objContenedor.MostrarVista("ConfirmacionPago");
        }
    }
}