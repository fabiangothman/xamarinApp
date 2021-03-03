using System;
using SURA.Models;
using Xamarin.Forms;
using SURA.Views;
using SURA.Services.Database;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Essentials;
using SURA.Services;

namespace SURA
{
    public partial class App : Xamarin.Forms.Application
    {
        public static string strVista;
        public static Contenedor objContenedor;
        public static bool blnPrivado;
        public static bool blnSolucionPersonal;
        public static bool ensenarMensaje;
        public static string DataType;

        public static string NombreUsuario = "";
        public static string Password = "";
        public static string Token = "";
        public static ModeloDashboard modeloDashboard = null;
        public static ModelPlataformaPago modeloPlataformaPago = null;
        public static bool plataformaPagoShouldInitialize = false;
        public static bool DatosPrueba = false;
        public static string NumeroPoliza = null;
        public static int NumeroRiesgo = 1;
        public static System.Collections.Generic.List<Coberturas> CoberturaList = null;
        public static System.Collections.Generic.List<CoordenadasModel> Sucursales = null;
        public static System.Collections.Generic.List<CoordenadasModel> PuntosPago = null;
        public static CoordenadasModel AsistenciaVialCoordenadas = null;
        public static ConnectContactModel connectContactModel = null;
        public static ConnectRegisteredUser connectRegisteredUser = null;

        public static string PathServiciosSURA = "https://resources.segurossura.com.pa";
        //public static string PathServiciosSURA = "http://ptyprueba:8090";

        public static string PathServicios = "http://10.240.3.117:8090";
        public static SURADatabase MyDatabasePath;

        public App()
        {
            InitializeComponent();

            Xamarin.Forms.Application.Current.On<iOS>().SetEnableAccessibilityScalingForNamedFontSizes(false);
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.Application.SetWindowSoftInputModeAdjust(this, Xamarin.Forms.PlatformConfiguration.AndroidSpecific.WindowSoftInputModeAdjust.Pan);

            App.blnPrivado = false;
            App.strVista = "Inicial";
            objContenedor = new Contenedor();
            MainPage = objContenedor;
        }

        protected override void OnStart()
        {
            try
            {
                var resumirEn = Preferences.Get("sesionPrivada", false);
                if (resumirEn && modeloDashboard != null && Token != "" && !string.IsNullOrEmpty(Token))
                {
                    ServicioDashboard servicioDashboard = new ServicioDashboard();
                    servicioDashboard.ObtenerDatos();
                    App.objContenedor.MostrarVista("Dashboard");
                }
                else
                    modeloDashboard = new ModeloDashboard();

                Sucursales = new System.Collections.Generic.List<CoordenadasModel>();
                PuntosPago = new System.Collections.Generic.List<CoordenadasModel>();
                AsistenciaVialCoordenadas = new CoordenadasModel();
                connectContactModel = new ConnectContactModel();
                InicializarCoordenadas();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        //inicializa el listado de coordenadas del mapa
        private void InicializarCoordenadas()
        {
            //Inicia el listado de Sucursales de SURA
            Sucursales.Add(new CoordenadasModel { latitude = 8.977563, longitude = -79.523115, address = "Empresas SURA" });
            Sucursales.Add(new CoordenadasModel { latitude = 8.976356, longitude = -79.516958, address = "Multicentro, Av Balboa, Panamá" });
            Sucursales.Add(new CoordenadasModel { latitude = 8.436255, longitude = -82.427027, address = "David" });
            Sucursales.Add(new CoordenadasModel { latitude = 8.977596, longitude = -79.523014, address = "Marbella" });
            Sucursales.Add(new CoordenadasModel { latitude = 9.004183, longitude = -79.533515, address = "Autos sura" });
            Sucursales.Add(new CoordenadasModel { latitude = 8.897113, longitude = -79.753969, address = "La Chorrera" });
            Sucursales.Add(new CoordenadasModel { latitude = 8.098677, longitude = -80.978828, address = "Santiago" });
            Sucursales.Add(new CoordenadasModel { latitude = 7.953370, longitude = -80.425606, address = "Chitre" });
        }

        public static SURADatabase Database
        {
            get
            {
                try
                {
                    if (MyDatabasePath == null)
                    {
                        MyDatabasePath = new SURADatabase(DependencyService.Get<ISQLiteDb>().GetConnection());
                    }
                }
                catch (Exception ex)
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Error en la base de datos: " + ex.ToString(), "Ok");
                }
                return MyDatabasePath;
            }
        }
    }
}
