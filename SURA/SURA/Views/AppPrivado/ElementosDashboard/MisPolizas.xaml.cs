using Rg.Plugins.Popup.Services;
using SURA.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.ElementosDashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisPolizas : ContentView
    {
        public MisPolizas()
        {
            try
            {
                InitializeComponent();

                CargarItems();
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Mis Polizas: " + ex.Message, "Ok");
            }
        }

        public void CargarItems()
        {
            try
            {
                //PolizasListview.ItemsSource = App.modeloDashboard.Polizas;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Cargar Items: " + ex.Message, "Ok");
            }
        }

        public async void ExpandirOpciones_Tapped(object sender, EventArgs e)
        {
            try
            {
                var expander = sender as Expander;

                var stack = expander.FindByName<StackLayout>("stackAsegurado");
                var ultimoStack = stack.Children[1];
                var chevron = ultimoStack.FindByName<Image>("imgchevron");

                if (expander.IsExpanded)
                {
                    await chevron.RotateTo(90, 200, Easing.Linear);
                }
                else
                {
                    await chevron.RotateTo(0, 200, Easing.Linear);
                }
                //expander.ForceUpdateSize();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("SURA", "Expandir opciones: " + ex.Message, "Ok");
            }
        }

        public async void Riesgos_Tapped(object sender, EventArgs e)
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                var varCelda = (StackLayout)sender;
                Models.MisPolizas varDato = (Models.MisPolizas)varCelda.BindingContext;
                App.objContenedor.MostrarVista("MisPolizasDetalle", varDato.Poliza.ToString());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Riesgos tapped: " + ex.Message, "Ok");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        public async void Pagos_Tapped(object sender, EventArgs e)
        {
            try
            {
                /*Uri objUri = new Uri("https://mipago.segurossura.com.pa/");
                await Browser.OpenAsync(objUri, BrowserLaunchMode.SystemPreferred);*/
                App.objContenedor.MostrarVista("MiPago1");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Pagos Clicked: " + ex.Message, "Ok");
            }
        }

        /*public async void EstadoCuenta_Tapped(object sender, EventArgs e)
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

        }*/

        /*public async void Caratula_Tapped(object sender, EventArgs e)
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
        }*/
    }
}