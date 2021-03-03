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

namespace SURA.Views.AppPrivado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentView
    {
        Services.ServicioPreferencias servicioPreferencias = new Services.ServicioPreferencias();
        public Dashboard()
        {
            InitializeComponent();
            CargarItems();
        }

        private async void CargarItems()
        {
            try
            {
                //Descomentar senccion y cambiar key de servicioPreferencias.VerMensaje para mostrar nuevo
                /*if (!App.ensenarMensaje && !servicioPreferencias.VerMensaje())
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var mensajePopup = new MensajePopup();
                        App.ensenarMensaje = true;
                        await PopupNavigation.Instance.PushAsync(mensajePopup);
                    });
                }*/

                /*string primerNombre = (App.modeloDashboard.Nombre).Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[0].ToLower();
                lblNombreCliente.Text = estandarizarNombre(primerNombre);*/

                lblPagoMinimo.Text = "B/.1.00";
                lblSaldoTotal.Text = "B/.43.78";
                /*List<decimal> dataSaldoExigible = App.modeloDashboard.Polizas.Where(o => Convert.ToDecimal(o.PAGO_MINIMO) >= 0).Select(o => Convert.ToDecimal(o.PAGO_MINIMO)).ToList();
                List<decimal> dataSaldoTotal = App.modeloDashboard.Polizas.Where(o => Convert.ToDecimal(o.Saldo) >= 0).Select(o => Convert.ToDecimal(o.Saldo)).ToList();
                lblPagoMinimo.Text = dataSaldoExigible.Sum().ToString("C", new CultureInfo("es-PA"));
                lblSaldoTotal.Text = dataSaldoTotal.Sum().ToString("C", new CultureInfo("es-PA"));*/

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "No se pudo obtener sus datos.Intentelo de nuevo, más tarde", "Ok");
                App.blnPrivado = false;
                Preferences.Set("sesionPrivada", App.blnPrivado);
                App.Token = "";
                App.ensenarMensaje = false;
                App.objContenedor.MostrarVista("Inicial");
            }
        }

        private string estandarizarNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return string.Empty;
            }
            char[] nombreEstandar = nombre.ToCharArray();
            nombreEstandar[0] = char.ToUpper(nombreEstandar[0]);
            return new string(nombreEstandar);
        }

        private async void btnMisPolizas_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.objContenedor.MostrarVista("MisPolizas");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Mis Polizas Click: " + ex.Message, "Ok");
            }
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

        private async void btnPagos_Clicked(object sender, EventArgs e)
        {
            try
            {
                /*Uri objUri = new Uri("https://mipago.segurossura.com.pa/");
                await Browser.OpenAsync(objUri, new BrowserLaunchOptions
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Color.White,
                    PreferredControlColor = Color.Blue
                });*/
                App.objContenedor.MostrarVista("MiPago1");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Pagos Clicked: " + ex.Message, "Ok");
            }
        }

        private async void btnCambiarContrasena_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.objContenedor.MostrarVista("RecuperarContra");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Pagos Clicked: " + ex.Message, "Ok");
            }
        }

        private async void btnNotificaciones_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.objContenedor.MostrarVista("Notificaciones");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Notificaciones Clicked: " + ex.Message, "Ok");
            }
        }

        private async void btnMiSalud_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.objContenedor.MostrarVista("MiSalud");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "MiSalud Clicked: " + ex.Message, "Ok");
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

        public async void VerTodos_Tapped(object sender, EventArgs e)
        {
            try
            {
                App.objContenedor.MostrarVista("TodosDocumentos");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "TodosDocumentos Clicked: " + ex.Message, "Ok");
            }
        }
    }
}