using System;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace SURA.Views.AppUbicaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ubicaciones : ContentView
    {
        Map mapaPersonalizado;
        public Ubicaciones()
        {
            InitializeComponent();
            LoadView();
        }

        private async void LoadView()
        {
            try
            {
                MapsView();
                frmContenedorMapa.Children.RemoveAt(0);
                frmContenedorMapa.Children.Add(new SucursalesView());
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Load View",ex.ToString(),"Ok");
            }
        }

        private async void LoadView(string view)
        {
            try
            {
                if (view == "Maps")
                {
                    MapsView();
                    frmContenedorMapa.Children.RemoveAt(0);
                    frmContenedorMapa.Children.Add(new SucursalesView());
                }
                else
                {
                    if (view == "Sucursales")
                    {
                        frmContenedorMapa.Children.RemoveAt(0);
                        frmContenedorMapa.Children.Add(new SucursalesView());
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Load View con view", ex.ToString(), "Ok");
            }
        }

        private async void MapsView()
        {
            try
            {
                lblSucursales.HorizontalOptions = LayoutOptions.Center;
                frmSucursales.Focus();
                mapaPersonalizado = new Map
                {
                    MapType = MapType.Street
                };
                
                frmContenedorMapa.Children.Add(mapaPersonalizado);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("MapsView", ex.ToString(), "Ok");
            }
        }

        private void Sucursales_Tapped(object sender, EventArgs e)
        {
            frmOpcionesPago.IsVisible = false;
            frmContenedorMapa.IsVisible = true;
            frmSucursales.Focus();
            //lblLocationType.Text = lblSucursales.Text;
            LoadView("Sucursales");
        }

        private void Opcionespago_Tapped(object sender, EventArgs e)
        { 
            frmOpcionesPago.IsVisible = true;
            frmContenedorMapa.IsVisible = false;
            //lblLocationType.Text = lblOpcionespago.Text;
        }
    }
}