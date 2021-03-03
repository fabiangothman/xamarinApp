using Rg.Plugins.Popup.Services;
using SURA.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.Documentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentosDescarga : ContentView
    {
        public DocumentosDescarga()
        {
            InitializeComponent();
        }

        private async void btnCartaDeRenta_Clicked(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;
            try
            {
                DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_DescargaCartaRenta");
                CargandoPopUp _cargandoPopup = new CargandoPopUp(true);
                var cerrarPopup = new TapGestureRecognizer();
                cerrarPopup.Tapped += (object tapSender, EventArgs args) =>
                {
                    cts.Cancel();
                    PopupNavigation.Instance.PopAllAsync();
                };
                _cargandoPopup.ShowCerrar(cerrarPopup);
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                Services.ServicioCartaRenta servicioCartaRenta = new Services.ServicioCartaRenta();
                await servicioCartaRenta.DescargarCartaRenta(ct);
                await PopupNavigation.Instance.PopAllAsync();
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(Path.Combine(FileSystem.CacheDirectory, "_temp.pdf"))
                });
            }
            catch (TaskCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", ex.Message, "Ok");
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        public async void Caratula_Tapped(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;
            try
            {
                DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_DescargaPoliza");
                CargandoPopUp _cargandoPopup = new CargandoPopUp(true);
                var cerrarPopup = new TapGestureRecognizer();
                cerrarPopup.Tapped += (object tapSender, EventArgs args) =>
                {
                    cts.Cancel();
                    PopupNavigation.Instance.PopAllAsync();
                };
                _cargandoPopup.ShowCerrar(cerrarPopup);
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                var varCelda = (StackLayout)sender;
                Models.MisPolizas varDato = (Models.MisPolizas)varCelda.BindingContext;
                Services.ServicioDescargaPoliza servicioDescargaPoliza = new Services.ServicioDescargaPoliza();

                await servicioDescargaPoliza.DescargarPoliza(varDato.PolizaCaratula, ct);
                await PopupNavigation.Instance.PopAllAsync();
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(Path.Combine(FileSystem.CacheDirectory, "_temp.pdf"))
                });

            }
            catch (TaskCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Carátula: " + ex.Message, "Ok");
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        public async void EstadoCuenta_Tapped(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;
            try
            {
                DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_DescargaEstCuenta");
                CargandoPopUp _cargandoPopup = new CargandoPopUp(true);
                var cerrarPopup = new TapGestureRecognizer();
                cerrarPopup.Tapped += (object tapSender, EventArgs args) =>
                {
                    cts.Cancel();
                    PopupNavigation.Instance.PopAllAsync();
                };
                _cargandoPopup.ShowCerrar(cerrarPopup);
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                var varCelda = (StackLayout)sender;
                Models.MisPolizas varDato = (Models.MisPolizas)varCelda.BindingContext;
                Services.ServicioEstadoDeCuenta servicioEstadoDeCuenta = new Services.ServicioEstadoDeCuenta();

                await servicioEstadoDeCuenta.DescargarEstadoDeCuenta(varDato.Poliza.ToString(), ct);
                await PopupNavigation.Instance.PopAllAsync();
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(Path.Combine(FileSystem.CacheDirectory, "_temp.pdf"))
                });
            }
            catch (TaskCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Estado de cuenta: " + ex.Message, "Ok");
                await PopupNavigation.Instance.PopAllAsync();
            }

        }

    }
}