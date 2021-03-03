using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Rg.Plugins.Popup.Services;
using SURA.Services;
using SURA.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SURA.Models
{
    class ModeloIngresar
    {
        public Action DisplayInvalidLoginPrompt;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged = delegate { };
        ServicioPreferencias servicioPreferencias = new ServicioPreferencias();
        private string user_name;
        private string password;

        public string User_Name
        {
            get { return user_name; }
            set
            {
                user_name = value;
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("User_Name"));
            }
        }

        public string Password
        {
            //get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Password"));
            }
        }

        
        public System.Windows.Input.ICommand ComandoIngresar { protected set; get; }

        public ModeloIngresar()
        {
            try
            {
                ComandoIngresar = new Command(Ingresando);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                App.Current.MainPage.DisplayAlert("SURA", "Modelo Ingresar: " + ex.Message, "Ok");
            }
        }

        public async void Ingresando()
        {
            try
            {
                if (user_name == null || user_name == "" || password == null || password == "")
                {
                    await Application.Current.MainPage.DisplayAlert("SURA", "Asegúrese de llenar todos los campos", "OK");
                }
                else
                {
                    Login();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Ingresando: "+ex.Message, "Ok");
            }
        }

        public async void Login()
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                await validarIngresoAsync();

                if (App.Token != "")
                {
                    var tips = Preferences.Get("tips", "");

                    var result = await CrossFingerprint.Current.IsAvailableAsync();
                    if (result)
                    {
                        if (tips == "1")
                        {
                            AbrirDashboard();
                        }
                        else
                        {
                            App.objContenedor.MostrarVista("HabilitarHuella");
                        }
                    }
                    else
                    {
                        if (App.Token == "" || App.Token == null)
                        {
                            DisplayInvalidLoginPrompt();
                        }
                        else
                        {
                            if (App.DatosPrueba == true)
                            {
                                AbrirDashboard();
                            }
                            else
                            {
                                AbrirDashboard();
                            }
                        }

                    }
                }
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                await App.Current.MainPage.DisplayAlert("SURA", "Login: " + ex.Message, "Ok");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private void AbrirDashboard()
        {
            DependencyService.Get<Helpers.IAjustarTeclado>().EsconderTeclado();

            App.blnPrivado = true;
            Preferences.Set("sesionPrivada", App.blnPrivado);
            App.objContenedor.MostrarVista("Dashboard");
        }

        private async Task validarIngresoAsync()
        {
            try
            {
                ServicioDashboard servicioDashboard = new ServicioDashboard();
                ServicioIngresar loginService = new ServicioIngresar();

                if (App.DatosPrueba == true)
                {
                    servicioDashboard.DataPrueba();
                    await GuardarUsuarioLoginAsync();
                }
                else
                {
                    App.Token = loginService.ObtenerToken(user_name, password);
                    if (App.Token == "" || App.Token == null)
                    {
                        throw new Exception("Usuario o contraseña incorrecta");
                    }
                    else
                    {
                        servicioDashboard.ObtenerDatos();
                        await GuardarUsuarioLoginAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", ex.Message, "Ok");
            }

        }

        public async void ValidarHuella()
        {
            try
            {
                await AuthenticateAsync("Coloque su huella en el lector");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("SURA", "Sucedió un error: "+ ex.Message, "OK");
            }
        }

        private async Task AuthenticateAsync(string razon, string cancel = null, string fallback = null, string muyRapido = null)
        {
            var dialogConfig = new AuthenticationRequestConfiguration("SURA", razon)
            { // all optional
                CancelTitle = cancel,
                FallbackTitle = fallback,
            };

            // optional
            dialogConfig.HelpTexts.MovedTooFast = muyRapido;

            var result = await CrossFingerprint.Current.AuthenticateAsync(dialogConfig);

            await SetResultAsync(result);
        }

        private async Task SetResultAsync(FingerprintAuthenticationResult resultado)
        {
            if (resultado.Authenticated)
            {
                string username = "NombreUsuario";
                string pass = "pass";
                password = await servicioPreferencias.ObtenerLlave(pass);
                user_name = await servicioPreferencias.ObtenerNombreUsuario(username);
                Login();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("SURA", resultado.Status.ToString()+": " + resultado.ErrorMessage.ToString(), "OK");
            }
        }

        private async Task GuardarUsuarioLoginAsync()
        {
            try
            {
                string username = "NombreUsuario";
                var user_viejo = await servicioPreferencias.ObtenerNombreUsuario(username);
       
                if (user_name != user_viejo)
                    servicioPreferencias.HabilitarMensaje(false);

                if (user_name != "" || user_name != "NombreUsuario")
                {
                    servicioPreferencias.GuardarNombreUsuario(user_name);
                    servicioPreferencias.GuardarPassword(password);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Guardar Usuario: " + ex.Message, "Ok");
            }
        }
    }
}
