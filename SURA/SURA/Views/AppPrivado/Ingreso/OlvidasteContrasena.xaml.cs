using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.Ingreso
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OlvidasteContrasena : ContentView
    {
        public OlvidasteContrasena()
        {
            InitializeComponent();
        }

        private async void BtnEnviar_Clicked(object sender, EventArgs e)
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                string correo = entCorreo.Text;
                //lblRevisaCorreo.IsVisible = false;
                if (String.IsNullOrWhiteSpace(correo))
                {
                    await App.Current.MainPage.DisplayAlert("SURA", "Asegurese de llenar todos los campos", "Ok");
                    lblRevisaCorreo.IsVisible = false;
                }
                else if (!Regex.Match(correo, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                {
                    await App.Current.MainPage.DisplayAlert("SURA", "El correo electrónico ingresado no es correcto, vuelva a ingresarlo", "Ok");
                    lblRevisaCorreo.IsVisible = false;
                }
                else
                {
                    btnEnviar.IsVisible = false;
                    Services.ServicioOlvidaContrasena servicioOlvidaContrasena = new Services.ServicioOlvidaContrasena();

                    System.Net.HttpStatusCode status = await servicioOlvidaContrasena.OlvideContrasena(correo);

                    if (status == System.Net.HttpStatusCode.OK)
                    {
                        await Application.Current.MainPage.DisplayAlert("SURA", "Su solicitud ha sido enviado satisfactoriamente", "Ok");
                        btnEnviar.IsVisible = false;
                        btnEnviarCodigo.IsVisible = true;
                        frmCodigo.IsVisible = true;
                        lblRevisaCorreo.IsVisible = true;
                        lblRevisaCorreo.Text = "Revise su correo electrónico para ver el código";
                    }
                    else
                    {
                        lblRevisaCorreo.IsVisible = true;
                        lblRevisaCorreo.Text = "Correo electrónico no válido";
                    }
                }
                btnEnviar.IsVisible = true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("SURA", "Ocurrió un problema: " + ex.Message, "Ok");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private async void btnEnviarCodigo_Clicked(object sender, EventArgs e)
        {
            try
            {
                CargandoPopUp _cargandoPopup = new CargandoPopUp();
                await PopupNavigation.Instance.PushAsync(_cargandoPopup);
                btnEnviarCodigo.IsVisible = false;
                Services.ServicioOlvidaContrasena servicioOlvidaContrasena = new Services.ServicioOlvidaContrasena();
                string correo = entCorreo.Text;
                string codigo = entCodigo.Text;
                if (correo == null || codigo == null)
                {
                    await App.Current.MainPage.DisplayAlert("SURA", "Asegurese de llenar todos los campos", "Ok");
                }
                else
                {
                    bool respuesta = servicioOlvidaContrasena.VerificarCodigo(correo, codigo);

                    if (respuesta == true)
                    {
                        //Ir a CambiarContraseña
                        await App.Current.MainPage.DisplayAlert("SURA", "Su código es correcto.\nIntroduzca su nueva contraseña", "Ok");
                        App.objContenedor.MostrarVista("RecuperarContra");
                    }
                    else
                    {
                        lblRevisaCorreo.Text = "Código no valido";
                    }

                }

                btnEnviarCodigo.IsVisible = true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Ocurrió un error: " + ex.Message, "Ok");
            }
            finally
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

        private double width;
        private double height;
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    OuterGrid.RowDefinitions.Clear();
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Absolute) });
                }
                else
                {
                    OuterGrid.RowDefinitions.Clear();
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15, GridUnitType.Absolute) });
                }
            }
        }

    }
}