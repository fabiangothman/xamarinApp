using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SURA.Models;
using SURA.Services;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppUbicaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpDetalleUbicacion : PopupPage
    {
        ModeloResultadoDetalle modeloDetalles = new ModeloResultadoDetalle();
        public string _direccion;
        public string _sucursal;
        
        public PopUpDetalleUbicacion()
        {
            InitializeComponent();
        }

        public PopUpDetalleUbicacion(string direccion)
        {
            try
            {
                InitializeComponent();
                _direccion = direccion;

                if (Device.RuntimePlatform == Device.Android)
                {
                    CargarDatosAsync();
                }
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    CargarDatos();
                }
                
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("SURA","Ha sucedido un error cargando los datos","Ok");
                PopupNavigation.Instance.PopAllAsync();
            }
            
        }

        private void CargarDatos()
        {
            try
            {
                gridOuter.RowDefinitions.Clear();
                gridOuter.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15, GridUnitType.Absolute) });
                gridOuter.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) });
                gridOuter.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35, GridUnitType.Absolute) });
                gridOuter.Children.Remove(imgSucursal);
                gridOuter.Children.Remove(atribuciones);
                gridOuter.Children.Remove(Telefono);
                gridOuter.Children.Remove(lblCiudad);
                gridOuter.Children.Remove(lblSucursal);
                gridOuter.Children.Remove(lblDias);
                gridOuter.Children.Remove(lblFinDe);
                gridOuter.Children.Remove(lblLunesAViernes);
                gridOuter.Children.Remove(lblSabados);
                gridOuter.Children.Remove(lblTelefono);
                gridOuter.Children.Remove(btnLlegar);
                gridOuter.Children.Add(lblSucursal,0,1);
                gridOuter.Children.Add(lblCiudad,0,2);
                
                gridOuter.Children.Add(btnLlegar, 1, 1);
                Grid.SetRowSpan(btnLlegar, 2);

                btnLlegar.IsVisible = true;
                switch (_direccion)
                {
                    case "Chitre":
                        lblSucursal.Text = "Seguros SURA – Chitré";
                        lblCiudad.Text = "Plaza Azuero Carretera Nacional vía Las Tablas, Chitré";
                        break;
                    case "Empresas SURA":
                        lblSucursal.Text = "Empresas SURA";
                        lblCiudad.Text = "Av, Calle Aquilino de la Guardia, Panamá";
                        break;
                    case "Multicentro, Av Balboa, Panamá":
                        lblSucursal.Text = "Seguros SURA - Megapolis(Antiguo Multicentro)";
                        lblCiudad.Text = "Megapolis Outlets Panama, Av Balboa, Panamá";
                        break;
                    case "David":
                        lblSucursal.Text = "Seguros SURA – David";
                        lblCiudad.Text = "David";
                        break;
                    case "Marbella":
                        lblSucursal.Text = "Seguros SURA – Marbella";
                        lblCiudad.Text = "Plaza Marbella, Calle Aquilino De La Guardia, entre Calle 47 y 48";
                        break;
                    case "Autos sura":
                        lblSucursal.Text = "Autos SURA";
                        lblCiudad.Text = "Vía Ricardo J. Alfaro, Panamá";
                        break;
                    case "La Chorrera":
                        lblSucursal.Text = "Seguros SURA – Chorrera";
                        lblCiudad.Text = "Uniplaza, Costa Verde";
                        break;
                    case "Santiago":
                        lblSucursal.Text = "Seguros SURA – Santiago";
                        lblCiudad.Text = "Calle Eduardo Santos B., Santiago";
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {

            }
        }

        //Seguros SURA – Marbella: https://goo.gl/maps/6Q8kW5P1A5uGHtQi9
        //Seguros SURA - Megapolis(Antiguo Multicentro) : https://goo.gl/maps/Rj3i4Z6LHNyD7FG5A
        //Autos SURA: https://goo.gl/maps/d8iDzrYfzUD5C2MD6
        //Seguros SURA – Chorrera: https://goo.gl/maps/mUViHqFhLhRWUGPUA
        //Seguros SURA – Chitré: https://goo.gl/maps/TvgACoBoGFDvtmba9
        //Seguros SURA – Santiago: https://goo.gl/maps/jguEE89A6kqgYpqL6
        //Seguros SURA – David: https://goo.gl/maps/SGdAst8Lss7QZNLo8


        private async void CargarDatosAsync()
        {
            try
            {
                string busquedaInfo;
                if (_direccion == "Autos sura")
                    busquedaInfo = "Autos sura panama";
                else if (_direccion == "Empresas SURA")
                    busquedaInfo = _direccion + " panama";
                else
                    busquedaInfo = "Seguros Sura " + _direccion;

                var PlaceId = await DependencyService.Get<IServicioGooglePlaces>().GetPlaces(busquedaInfo);
                if(PlaceId != string.Empty)
                {
                    ModeloResultadoDetalle detalle = await DependencyService.Get<IServicioGooglePlaces>().GetDetails(PlaceId);

                    Telefono.IsVisible = true;
                    btnLlegar.IsVisible = true;

                    modeloDetalles = detalle;
                    lblSucursal.Text = detalle.Name;
                    lblCiudad.Text = detalle.FormattedAddress;
                    lblTelefono.Text = detalle.FormattedPhoneNumber;

                    imgSucursal.Source = ImageSource.FromStream(() => new MemoryStream(detalle.Photos));
                    if (detalle.photoAttribution.PhotoName != null)
                        atribuciones.Text = "Fotografía por " + detalle.photoAttribution.PhotoName;
                    if (detalle.WeekdayText != null)
                    {
                        lblLunesAViernes.IsVisible = true;
                        lblDias.IsVisible = true;
                        lblSabados.IsVisible = true;
                        lblFinDe.IsVisible = true;
                        var horaLunes = detalle.WeekdayText.Where(x => x.Contains("lunes")).ToArray().First().ToString();
                        var horaSabado = detalle.WeekdayText.Where(x => x.Contains("sábado")).ToArray().First().ToString();
                        string[] dias = horaLunes.Split(new char[] { ':' }, 2, StringSplitOptions.None);
                        string[] finde = horaSabado.Split(new char[] { ':' }, 2, StringSplitOptions.None);
                        lblDias.Text = dias[1];
                        lblFinDe.Text = finde[1];
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (SystemException sysEx)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Ocurrió un problema. Vuelva a intentarlo más tarde.", "Ok");
                await PopupNavigation.Instance.PopAllAsync();
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("SURA", "Lo sentimos, esta característica no está disponible.", "Ok");
                //await PopupNavigation.Instance.PopAllAsync();
                CargarDatos();
            }
        }

        private async void btnCerrar_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        private void btnLlegar_Clicked(object sender, EventArgs e)
        {
            var destino = lblSucursal.Text;
            if (destino == "Empresas SURA")
                destino = destino + " Aquilino Panama";
            else if (destino == "Autos SURA")
                destino = destino + " Panama";
            var sucursal = destino.Replace(" ", "+");
            //daddr es el end point y saddr es starting point
            EjecutarAplicacion("http://maps.google.com/maps?saddr=&daddr=" + sucursal + "&directionsmode=transit");
        }

        private void Telefono_Tapped(object sender, EventArgs e)
        {
            try
            {
                var telefono = Regex.Replace(modeloDetalles.FormattedPhoneNumber, "-", "");
                telefono = (telefono).Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[1];
                PhoneDialer.Open(telefono);
            }
            catch (FeatureNotSupportedException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        private async void EjecutarAplicacion(string strUri)
        {
            try
            {
                Uri objUri = new Uri(strUri);
                var supportsUri = await Launcher.CanOpenAsync(objUri);
                if (supportsUri)
                    await Launcher.OpenAsync(objUri);
                else
                    await DisplayAlert("SURA", "No se puede ejecutar la aplicación: " + strUri, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("SURA", "EjecutarAplicacion: " + ex.Message, "OK");
            }
        }

        void Attributions_Tapped(System.Object sender, System.EventArgs e)
        {
            EjecutarAplicacion(modeloDetalles.photoAttribution.PhotoUrl);
        }
    }
}