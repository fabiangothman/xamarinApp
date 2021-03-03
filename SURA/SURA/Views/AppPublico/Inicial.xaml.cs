using HtmlAgilityPack;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using SURA.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPublico
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicial : ContentView
    {
        public Inicial()
        {
            InitializeComponent();

        }




        private void btnSolucionesPersonas_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("SolucionesPersonas");
        }

        private async void btnSolucionesPymes_Tapped(object sender, EventArgs e)
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                App.objContenedor.MostrarVista("DetallesContenedor", "Negocio Protegido");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }

        }

        private void btnIngresarCuenta_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("Ingresar");
        }

        private void btnMiPago_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("MiPagoPublic");
        }

        private async void btnAsegurate_Tapped(object sender, EventArgs e)
        {
            Uri objUri = new Uri("https://asegurate.segurossura.com.pa/");
            await Browser.OpenAsync(objUri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.White,
                PreferredControlColor = Color.Blue
            });
        }
    }
}