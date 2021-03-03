using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Fingerprint;
using SURA.Views.AppPrivado.Ingreso;
using Xamarin.Essentials;
using SURA.Helpers;

namespace SURA.Views.AppPrivado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ingresar : ContentView
    {
        Services.ServicioPreferencias servicioPreferencias = new Services.ServicioPreferencias();
        public Ingresar()
        {
            try
            {
                InitializeComponent();

                CargarItems();
                //DataPrueba.IsToggled = App.DatosPrueba;


                var viewModel = new Models.ModeloIngresar();
                this.BindingContext = viewModel;
                viewModel.DisplayInvalidLoginPrompt += () => App.Current.MainPage.DisplayAlert("Error", "El usuario o contraseña son incorrectos", "OK");

                entUserName.Completed += (object sender, EventArgs e) =>
                {
                    entPassword.Focus();
                };

                entPassword.Completed += (object sender, EventArgs e) =>
                {
                    if (entUserName.Text != null || entUserName.Text != "" && entPassword.Text != null || entPassword.Text != "")
                    {
                        viewModel.ComandoIngresar.Execute(null);
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Error", "El usuario o contraseña son incorrectos", "OK");
                    }
                };
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Ingresar: " + ex.Message, "Ok");
            }

            servicioPreferencias.ObtenerNombreUsuario("NombreUsuario");
            entUserName.Text = App.NombreUsuario;
            if (this.checkBoxRecordar.IsChecked)
            {
                servicioPreferencias.ObtenerPassword("pass");
                entPassword.Text = App.Password;
            }
            else {
                servicioPreferencias.EliminarPassword("pass");
            }
        }

        private async void frameHuella_Tapped(object sender, EventArgs e)
        {
            try
            {
                var result = await CrossFingerprint.Current.IsAvailableAsync();
                int tips = Convert.ToInt32(Preferences.Get("tips", ""));
                Models.ModeloIngresar modeloIngreso = new Models.ModeloIngresar();

                if (result)
                {
                    if (tips == 1)
                    {
                        modeloIngreso.ValidarHuella();
                    }

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error de huella", "Huella incorrecta", "Ok");
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("SURA", ex.Message, "Ok");
            }
        }

        //Carga nombre de Usuario que recuerde y muestra campo de huella si el celular lo permite
        private async void CargarItems()
        {
            try
            {
                string userName = "";
                string habilitarHuella = "";
                habilitarHuella = await servicioPreferencias.ObtenerLlave("estaHabilitado");
                var result = await CrossFingerprint.Current.IsAvailableAsync();
                string tips = Xamarin.Essentials.Preferences.Get("tips", "");

                if (result)
                {

                    if (tips == "1")
                    {
                        frameBiometrico.IsVisible = true;
                        if (await CrossFingerprint.Current.GetAuthenticationTypeAsync() == Plugin.Fingerprint.Abstractions.AuthenticationType.Face)
                        {
                            imgBiometrico.Source = "faceid";
                        }
                        else
                        {
                            imgBiometrico.Source = "huella";
                        }
                    }
                    else
                    {
                        frameBiometrico.IsVisible = false;
                    }
                }

            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("SURA", ex.Message, "OK");
            }

        }

        private void DirectLogin_Tapped(object sender, EventArgs e)
        {
            App.blnPrivado = true;
            App.objContenedor.MostrarVista("Dashboard");
        }

        private void Registrate_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Registrate");
            EjecutarAplicacion("https://miportal.segurossura.com.pa/Account/Suscripcion");
        }

        private async void EjecutarAplicacion(string strUri)
        {
            try
            {
                Uri objUri = new Uri(strUri);
                await Browser.OpenAsync(objUri, new BrowserLaunchOptions {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Color.White,
                    PreferredControlColor = Color.Blue
                });                    
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "No se puede ejecutar la aplicación: " + strUri, "OK");
            }
        }

        private void OlvidasteContrasena_Tapped(object sender, EventArgs e)
        {
            App.blnPrivado = true;
            App.objContenedor.MostrarVista("OlvidasteContrasena");
        }

        private void DataPrueba_Toggled(object sender, ToggledEventArgs e)
        {
            bool toggleStatus = e.Value;
            App.DatosPrueba = toggleStatus;
        }

        private void Terminos_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("TerminosYCondiciones");
            EjecutarAplicacion("https://segurossura.com.pa/terminos-y-condiciones/");
        }
    }
}