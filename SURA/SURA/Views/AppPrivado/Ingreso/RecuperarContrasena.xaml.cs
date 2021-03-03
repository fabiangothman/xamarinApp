using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.Ingreso
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecuperarContrasena : ContentView
    {
        public RecuperarContrasena()
        {
            InitializeComponent();
            CargarItems();
        }

        private void CargarItems()
        {
            try
            {
                //Si entro desde dashboard, cambiar contraseña
                if (App.blnPrivado)
                    frmViejaContrasena.IsVisible = true;
                else
                    frmViejaContrasena.IsVisible = false;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Cargar datos: " + ex.Message, "Ok");
            }
        }

        private async void btnEnviar_Tapped(object sender, EventArgs e)
        {
            string nuevaContrasena = entNuevaContrasena.Text;
            string confirmarContra = entConfirmarContrasena.Text;
            //Si Viene de Olvidar contraseña
            if (!App.blnPrivado)
            {
                if (string.IsNullOrWhiteSpace(nuevaContrasena) || string.IsNullOrWhiteSpace(confirmarContra))
                {
                    await App.Current.MainPage.DisplayAlert("SURA", "Asegúrese de llenar todos los campos", "Ok");
                }
                else
                {
                    Services.ServicioEstablecerContrasena servicioEstablecerContrasena = new Services.ServicioEstablecerContrasena();
                    bool respuesta = servicioEstablecerContrasena.EstablecerContrasena(nuevaContrasena, confirmarContra);
                    if (respuesta == true)
                    {
                        App.objContenedor.MostrarVista("Ingresar");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("SURA", "Vuelva a intentarlo", "Ok");
                    }
                }
            }
            else
            {
                //Si viene de Cambiar contraseña
                string viejaContrasena = entViejaContrasena.Text;
                Services.ServicioCambiarContrasena servicioCambiarContrasena = new Services.ServicioCambiarContrasena();

                if (viejaContrasena == null || nuevaContrasena == null || confirmarContra == null)
                {
                    await App.Current.MainPage.DisplayAlert("SURA", "Asegúrese de llenar todos los campos", "Ok");
                }
                else
                {
                    servicioCambiarContrasena.CambiarContrasena(viejaContrasena, nuevaContrasena, confirmarContra);
                    entViejaContrasena.Text = string.Empty;
                    entNuevaContrasena.Text = string.Empty;
                    entConfirmarContrasena.Text = string.Empty;
                }
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
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) });
                }
                else
                {
                    OuterGrid.RowDefinitions.Clear();
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
                    OuterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(65, GridUnitType.Absolute) });
                }
            }
        }
    }
}