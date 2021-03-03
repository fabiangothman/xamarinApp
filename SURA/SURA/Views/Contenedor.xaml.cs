using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Newtonsoft.Json;
using System.Net.Http;
using SURA.Helpers;

namespace SURA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contenedor : ContentPage
    {
        private bool alreadyShowUpdate = false;

        public Contenedor()
        {
            InitializeComponent();

            MostrarVista("Inicial");

        }

        private void Titulo(bool blnVisible)
        {
            if (blnVisible)
            {
                if (!this.lblTitulo.IsVisible)
                {
                    this.imgLogo.IsVisible = false;
                    this.lblTitulo.IsVisible = true;
                }
            }
            else
            {
                if (this.lblTitulo.IsVisible)
                {
                    this.lblTitulo.IsVisible = false;
                    this.imgLogo.IsVisible = true;
                }
            }
        }

        public void MostrarVista(string strVistaNueva)
        {
            try
            {
                string strTitulo = " ";
                ContentView objContentView;
                scrlContenedor.InputTransparent = false;
                /*this.frmNotificaciones.IsVisible = false;
                this.lineaNotificaciones.IsVisible = false;*/
                this.imgBackIcon.Source = "flecha_volver_blanca";
                Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                    .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);

                /*Inicializadores de comportamiento dinamico, dependiendo de la pantalla*/
                this.frmNavBtnUsuario.IsVisible = false;
                this.frmNavBtnUsuario.BackgroundColor = Color.FromRgb(0, 51, 160);

                this.hDerecha_misalud.IsVisible = false;
                this.hDerecha_chat.IsVisible = true;
                this.imgBackIcon.HeightRequest = 20;
                this.lyBarraInferPagar.IsVisible = false;
                this.lyBarraIniciarConsulta.IsVisible = false;
                this.navCol5.BackgroundColor = Color.FromRgb(0, 51, 160);

                switch (strVistaNueva)
                {
                    case "Inicial":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Inicio");
                        objContentView = new AppPublico.Inicial();
                        this.frmBarraSuperior.IsVisible = false;
                        break;
                    case "SolucionesPersonas":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("SolucionesPersonas");
                        objContentView = new AppPublico.SolucionesPersonas();
                        this.frmBarraSuperior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Soluciones para Personas";
                        break;
                    case "Ingresar":
                        objContentView = new AppPrivado.Ingresar();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Ingresar");
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraSuperior.BackgroundColor = Color.FromRgb(0, 51, 160);
                        Titulo(true);
                        strTitulo = "";
                        break;
                    case "MiPagoPublic":
                        //objContentView = new AppPrivado.Ingresar();
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Ingresar");
                        objContentView = new AppPublico.MiPago.MiPagoPublic();
                        this.frmBarraSuperior.IsVisible = true;
                        strTitulo = "";
                        Titulo(true);
                        break;
                    case "MiPagoResultados":
                        objContentView = new AppPublico.MiPago.MiPagoResultados();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraSuperior.BackgroundColor = Color.FromRgb(0, 51, 160);
                        this.imgLogo.Source = "mi_pago_logo";

                        this.lyBarraInferPagar.IsVisible = true;

                        Titulo(false);
                        break;
                    case "AsistenciaVial":
                        objContentView = new AppPublico.AsistenciaVial.AsistenciaVial1();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("AsistenciaVial1/3");
                        this.frmBarraSuperior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Asistencia Vial 1/3";
                        break;
                    case "AsistenciaVial2":
                        objContentView = new AppPublico.AsistenciaVial.AsistenciaVial2();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("AsistenciaVial2/3");
                        this.frmBarraSuperior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Asistencia Vial 2/3";
                        if (Device.RuntimePlatform == Device.iOS)
                            scrlContenedor.InputTransparent = false;
                        else
                            scrlContenedor.InputTransparent = true;
                        break;
                    case "AsistenciaVial3":
                        objContentView = new AppPublico.AsistenciaVial.AsistenciaVial3();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("AsistenciaVial3/3");
                        this.frmBarraSuperior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Asistencia Vial 3/3";
                        break;
                    case "Ubicaciones":
                        objContentView = new AppUbicaciones.Ubicaciones();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Ubicaciones");
                        this.frmBarraSuperior.IsVisible = true;
                        if (Device.RuntimePlatform == Device.iOS)
                            scrlContenedor.InputTransparent = false;
                        else
                            scrlContenedor.InputTransparent = true;
                        strTitulo = "Ubicaciones";
                        Titulo(true);
                        break;
                    case "Dashboard":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Dashboard");
                        DependencyService.Get<Helpers.IAjustarTeclado>().EsconderTeclado();
                        objContentView = new AppPrivado.Dashboard();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraSuperior.BackgroundColor = Color.White;

                        this.frmNavBtnUsuario.IsVisible = true;

                        Titulo(false);
                        //this.imgBackIcon.Source = "flecha_volver_blanca";
                        this.imgBackIcon.Source = "boton_salir";
                        this.imgBackIcon.HeightRequest = 30;
                        //this.imgBackIcon.Margin = 3;
                        App.plataformaPagoShouldInitialize = true;
                        App.modeloPlataformaPago = null;

                        break;
                    case "OlvidasteContrasena":
                        objContentView = new AppPrivado.Ingreso.OlvidasteContrasena();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("OlvidarContrasena");
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        App.blnPrivado = false;
                        Titulo(true);
                        strTitulo = "Recuperar Contraseña";
                        break;
                    case "FormularioContacto":
                        objContentView = new AppPublico.Contacto.FormularioContacto();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("FormularioContacto");
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Contáctenos";
                        break;
                    case "HabilitarHuella":
                        objContentView = new AppPrivado.Ingreso.HabilitarHuella();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("HabilitarHuella");
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Habilite su huella";
                        break;
                    case "RecuperarContra":
                        objContentView = new AppPrivado.Ingreso.RecuperarContrasena();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("RecuperarContrasena");
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Cambiar contraseña";
                        break;
                    case "MisPolizas":
                        objContentView = new AppPrivado.ElementosDashboard.MisPolizas();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("MisPolizas");
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        this.frmBarraSuperior.BackgroundColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.TextColor = Color.White;

                        this.frmNavBtnUsuario.IsVisible = true;

                        Titulo(true);
                        strTitulo = "Detalle de pólizas";
                        break;
                    case "Perfil":
                        objContentView = new AppPrivado.Usuario.Perfil();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        //this.frmBarraSuperior.BackgroundColor = Color.FromRgb(0, 51, 160);
                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);

                        this.frmNavBtnUsuario.IsVisible = true;
                        this.frmNavBtnUsuario.BackgroundColor = Color.FromRgb(51, 92, 179);
                        this.navCol5.BackgroundColor = Color.FromRgb(51, 92, 179);

                        Titulo(true);
                        strTitulo = "Mi perfil";
                        break;
                    case "EditarPerfil":
                        objContentView = new AppPrivado.Usuario.EditarPerfil();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        //this.frmBarraSuperior.BackgroundColor = Color.FromRgb(0, 51, 160);
                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);

                        this.frmNavBtnUsuario.IsVisible = true;
                        this.frmNavBtnUsuario.BackgroundColor = Color.FromRgb(51, 92, 179);
                        this.navCol5.BackgroundColor = Color.FromRgb(51, 92, 179);

                        Titulo(true);
                        strTitulo = "Editar perfil";
                        break;
                    case "Contrasena":
                        objContentView = new AppPrivado.Ingreso.RecuperarContrasena();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("RecuperarContrasena");
                        //objContentView = new AppPrivado.Usuario.Contrasena();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        //this.frmBarraSuperior.BackgroundColor = Color.FromRgb(0, 51, 160);
                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);

                        this.frmNavBtnUsuario.IsVisible = true;
                        this.frmNavBtnUsuario.BackgroundColor = Color.FromRgb(51, 92, 179);
                        this.navCol5.BackgroundColor = Color.FromRgb(51, 92, 179);

                        Titulo(true);
                        strTitulo = "Cambiar contraseña";
                        break;
                    case "ChatEnLinea":
                        objContentView = new AppPublico.Contacto.ChatEnLinea();
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ChatEnLine");
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Chat en línea";
                        break;
                    case "SalidaChatEnLinea":
                        if (App.blnPrivado)
                        {
                            objContentView = new AppPrivado.Dashboard();
                            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Dashboard");
                            this.frmBarraSuperior.IsVisible = true;
                            Titulo(false);
                            strVistaNueva = "Dashboard";
                        }
                        else
                        {
                            objContentView = new AppPublico.Inicial();
                            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Inicial");
                            this.frmBarraSuperior.IsVisible = false;
                            strVistaNueva = "Inicial";
                        }
                        break;
                    case "Notificaciones":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Notificaciones();
                        this.frmBarraSuperior.IsVisible = true;
                        strTitulo = "Notificaciones";
                        Titulo(true);
                        break;
                    case "MiSalud":
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Salud.MiSalud();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;

                        this.frmNavBtnUsuario.IsVisible = true;

                        this.hDerecha_chat.IsVisible = false;
                        this.hDerecha_misalud.IsVisible = true;

                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.FontSize = 18;
                        strTitulo = "Mis servicios de Salud";
                        Titulo(true);
                        break;
                    case "TodosDocumentos":
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Documentos.DocumentosDescarga();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;

                        this.frmNavBtnUsuario.IsVisible = true;

                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.FontSize = 18;
                        strTitulo = "Tus documentos";
                        Titulo(true);
                        break;
                    case "CitasMedicas":
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Salud.CitasMedicas();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;

                        this.frmNavBtnUsuario.IsVisible = true;

                        this.hDerecha_chat.IsVisible = false;
                        this.hDerecha_misalud.IsVisible = true;

                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.FontSize = 18;
                        strTitulo = "Citas médicas";
                        Titulo(true);
                        break;
                    case "ProximasCitas":
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Salud.ProximasCitas();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;

                        this.frmNavBtnUsuario.IsVisible = true;

                        this.hDerecha_chat.IsVisible = false;
                        this.hDerecha_misalud.IsVisible = true;

                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.FontSize = 18;
                        strTitulo = "Próximas citas";
                        Titulo(true);
                        break;
                    case "CitasAnteriores":
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Salud.CitasAnteriores();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;

                        this.frmNavBtnUsuario.IsVisible = true;

                        this.hDerecha_chat.IsVisible = false;
                        this.hDerecha_misalud.IsVisible = true;

                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.FontSize = 18;
                        strTitulo = "Citas anteriores";
                        Titulo(true);
                        break;
                    case "ConsultaVirtual":
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Salud.ConsultaVirtual();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        this.lyBarraIniciarConsulta.IsVisible = true;

                        this.frmNavBtnUsuario.IsVisible = true;

                        this.hDerecha_chat.IsVisible = false;
                        this.hDerecha_misalud.IsVisible = true;

                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.FontSize = 18;
                        strTitulo = "Consulta virtual";
                        Titulo(true);
                        break;
                    case "DirectorioProveedores":
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Proveedores.DirectorioProveedores();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;

                        this.frmNavBtnUsuario.IsVisible = true;

                        this.hDerecha_chat.IsVisible = false;
                        this.hDerecha_misalud.IsVisible = true;

                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.FontSize = 18;
                        strTitulo = "Directorio proveedores";
                        Titulo(true);
                        break;
                    case "ProfesionalesSalud":
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Proveedores.ProfesionalesSalud();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;

                        this.frmNavBtnUsuario.IsVisible = true;

                        this.hDerecha_chat.IsVisible = false;
                        this.hDerecha_misalud.IsVisible = true;

                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.FontSize = 18;
                        strTitulo = "Profesionales de la salud";
                        Titulo(true);
                        break;
                    case "ResultadosProfesionalesSalud":
                        //DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Notificaciones");
                        objContentView = new AppPrivado.Proveedores.ResultadosProfesionalesSalud();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;

                        this.frmNavBtnUsuario.IsVisible = true;

                        this.hDerecha_chat.IsVisible = false;
                        this.hDerecha_misalud.IsVisible = true;

                        this.imgBackIcon.Source = "flecha_volver_azul";
                        this.lblTitulo.TextColor = Color.FromRgb(0, 51, 160);
                        this.lblTitulo.FontSize = 18;
                        strTitulo = "Resultados";
                        Titulo(true);
                        break;
                    case "MiPago1":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("DetallesPago");
                        objContentView = new AppPrivado.ElementosDashboard.PasarelaPago.DetallePago();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Mi Pago 1/2";
                        if (App.plataformaPagoShouldInitialize)
                        {
                            App.modeloPlataformaPago = new Models.ModelPlataformaPago();
                        }
                        break;
                    case "MiPago2":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ValidarTarjeta");
                        objContentView = new AppPrivado.ElementosDashboard.PasarelaPago.ValidarTarjeta();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Mi Pago 2/2";
                        break;
                    case "ConfirmacionPago":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ConfirmacionPago");
                        objContentView = new AppPrivado.ElementosDashboard.PasarelaPago.ConfirmacionPago();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Confirmación del pago";
                        break;
                    case "PagoSatisfactorio":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("PagoSatisfactorio");
                        objContentView = new AppPrivado.ElementosDashboard.PasarelaPago.PagoSatisfactorio();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Pago Satisfactorio";
                        break;
                    default:
                        throw new Exception("No existe vista: " + strVistaNueva);
                }
                if (this.frmBarraSuperior.IsVisible)
                    this.lblTitulo.Text = strTitulo;
                if (App.blnPrivado)
                {
                    /*this.frmNotificaciones.IsVisible = true;
                    this.lineaNotificaciones.IsVisible = true;*/
                }

                this.grdContenedor.Children.Clear();

                this.grdContenedor.Children.Add(objContentView);
                App.strVista = strVistaNueva;

            }
            catch (Exception ex)
            {
                DisplayAlert("SURA", "MostrarVista: " + ex.ToString(), "OK");
            }
        }

        //Para las vistas de detalles en Soluciones para persona y pymes
        public void MostrarVista(string strVistaNueva, string Detalle)
        {
            try
            {
                string strTitulo = "*** TITULO ***";
                ContentView objContentView;
                switch (strVistaNueva)
                {
                    case "DetallesContenedor":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("Dashboard");
                        objContentView = new AppPublico.Soluciones.DetallesContenedor(Detalle);
                        Titulo(true);
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        strTitulo = Detalle;
                        break;
                    case "Coberturas":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("RiesgosAsegurados");
                        objContentView = new AppPrivado.ElementosDashboard.Cobertura();
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Riesgos asegurados: " + Detalle;
                        break;
                    case "MisPolizasDetalle":
                        DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("DetallePoliza");
                        objContentView = new AppPrivado.ElementosDashboard.DetallePoliza(Detalle);
                        this.frmBarraSuperior.IsVisible = true;
                        this.frmBarraInferior.IsVisible = true;
                        Titulo(true);
                        strTitulo = "Poliza: " + Detalle;
                        break;
                    default:
                        throw new Exception("No existe vista: " + strVistaNueva);
                }

                if (this.frmBarraSuperior.IsVisible)
                    this.lblTitulo.Text = strTitulo;
                this.grdContenedor.Children.Clear();
                this.grdContenedor.Children.Add(objContentView);
                App.strVista = strVistaNueva;
            }
            catch (Exception ex)
            {
                DisplayAlert("SURA", "MostrarVista Detalle: " + ex.Message, "OK");
            }
        }

        //Para pasar diccionario con Info de Asunto de contacto
        public void MostrarVista(string strVistaNueva, Dictionary<string, string> informacionAsunto)
        {
            try
            {
                string strTitulo = "*** CONTACTENOS ***";
                ContentView objContentView;
                DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("FormularioContacto");
                objContentView = new AppPublico.Contacto.FormularioContacto(informacionAsunto);

                Titulo(true);
                strTitulo = "Contáctenos";

                if (this.frmBarraSuperior.IsVisible)
                    this.lblTitulo.Text = strTitulo;
                this.grdContenedor.Children.Clear();
                this.grdContenedor.Children.Add(objContentView);
                App.strVista = strVistaNueva;
            }
            catch (Exception ex)
            {
                DisplayAlert("SURA", "MostrarVista Contacto: " + ex.Message, "OK");
            }
        }

        private void frmAtras_Tapped(object sender, EventArgs e)
        {
            IrAtras();
        }

        private async void IrAtras()
        {
            string strVistaAtras = "";
            switch (App.strVista)
            {
                case "SolucionesPersonas":
                    strVistaAtras = "Inicial";
                    break;
                case "SolucionesPymes":
                    strVistaAtras = "Inicial";
                    break;
                case "Ingresar":
                    strVistaAtras = "Inicial";
                    break;
                case "MiPagoPublic":
                    strVistaAtras = "Inicial";
                    break;
                case "MiPagoResultados":
                    strVistaAtras = "MiPagoPublic";
                    break;
                case "AsistenciaVial":
                    if (App.blnPrivado)
                        strVistaAtras = "Dashboard";
                    else
                        strVistaAtras = "Inicial";
                    break;
                case "AsistenciaVial2":
                    strVistaAtras = "AsistenciaVial";
                    break;
                case "AsistenciaVial3":
                    strVistaAtras = "AsistenciaVial2";
                    break;
                case "Ubicaciones":
                    if (App.blnPrivado)
                        strVistaAtras = "Dashboard";
                    else
                        strVistaAtras = "Inicial";
                    break;
                case "Notificaciones":
                    strVistaAtras = "Dashboard";
                    break;
                case "MiSalud":
                    strVistaAtras = "Dashboard";
                    break;
                case "TodosDocumentos":
                    strVistaAtras = "Dashboard";
                    break;
                case "CitasMedicas":
                    strVistaAtras = "MiSalud";
                    break;
                case "ProximasCitas":
                    strVistaAtras = "CitasMedicas";
                    break;
                case "CitasAnteriores":
                    strVistaAtras = "CitasMedicas";
                    break;
                case "ConsultaVirtual":
                    strVistaAtras = "MiSalud";
                    break;
                case "DirectorioProveedores":
                    strVistaAtras = "MiSalud";
                    break;
                case "ProfesionalesSalud":
                    strVistaAtras = "DirectorioProveedores";
                    break;
                case "ResultadosProfesionalesSalud":
                    strVistaAtras = "ProfesionalesSalud";
                    break;
                case "Perfil":
                    strVistaAtras = "Dashboard";
                    break;
                case "EditarPerfil":
                    strVistaAtras = "Perfil";
                    break;
                case "Contrasena":
                    if (App.blnPrivado)
                        strVistaAtras = "Perfil";
                    else
                        strVistaAtras = "Ingresar";
                    break;
                case "Dashboard":
                    var objCerrar = await DisplayAlert("SURA", "¿Desea cerrar la sesión?", "Si", "No");
                    if (objCerrar)
                    {
                        strVistaAtras = "Inicial";
                        App.blnPrivado = false;
                        Preferences.Set("sesionPrivada", App.blnPrivado);
                        App.Token = "";
                        App.ensenarMensaje = false;
                    }
                    break;
                case "OlvidasteContrasena":
                    strVistaAtras = "Ingresar";
                    this.frmBarraInferior.IsVisible = true;
                    App.blnPrivado = false;
                    break;
                case "FormularioContacto":
                    if (App.blnPrivado)
                        strVistaAtras = "Dashboard";
                    else
                        strVistaAtras = "Inicial";
                    this.frmBarraInferior.IsVisible = true;
                    break;
                case "HabilitarHuella":
                    strVistaAtras = "Ingresar";
                    App.blnPrivado = false;
                    break;
                case "RecuperarContra":
                    if (App.blnPrivado)
                        strVistaAtras = "Dashboard";
                    else
                        strVistaAtras = "Ingresar";
                    break;
                case "MisPolizas":
                    strVistaAtras = "Dashboard";
                    break;
                case "MisPolizasDetalle":
                    strVistaAtras = "MisPolizas";
                    break;
                case "ChatEnLinea":
                    if (App.blnPrivado)
                        strVistaAtras = "Dashboard";
                    else
                        strVistaAtras = "Inicial";
                    break;
                case "DetallesContenedor":
                    if (App.blnSolucionPersonal)
                        strVistaAtras = "SolucionesPersonas";
                    else
                        strVistaAtras = "Inicial";
                    break;
                case "Coberturas":
                    strVistaAtras = "MisPolizas";
                    break;
                case "MiPago1":
                    strVistaAtras = "Dashboard";
                    break;
                case "MiPago2":
                    strVistaAtras = "MiPago1";
                    break;
                case "ConfirmacionPago":
                    strVistaAtras = "MiPago2";
                    break;

                case "PagoSatisfactorio":
                    strVistaAtras = "Dashboard";
                    break;
                default:
                    strVistaAtras = "";
                    break;
            }
            if (strVistaAtras.Length > 0)
                MostrarVista(strVistaAtras);
        }

        private void frmContactenos_Tapped(object sender, EventArgs e)
        {
            this.popupContactar.IsVisible = true;
        }

        private void btnIniciarConsulta_Tapped(object sender, EventArgs e)
        {
            //Lo que hace cuando inicia la consulta
            this.popupErrorConsultaVirtual.IsVisible = true;
        }

        private void frmMiSaludDesenglosar_Tapped(object sender, EventArgs e)
        {
            this.popupMiSalud.IsVisible = true;
        }

        private void frmErrorConsultaVirtual_Tapped(object sender, EventArgs e)
        {
            this.popupErrorConsultaVirtual.IsVisible = true;
        }

        private void frmUsuario_Tapped(object sender, EventArgs e)
        {
            this.popupUsuario.IsVisible = true;
        }

        private void frmCancelarContactanos_Tapped(object sender, EventArgs e)
        {
            this.popupContactar.IsVisible = false;
        }

        private void frmCancelarUsuario_Tapped(object sender, EventArgs e)
        {
            this.popupUsuario.IsVisible = false;
        }

        private void frmCancelarMiSaludDesenglosar_Tapped(object sender, EventArgs e)
        {
            this.popupMiSalud.IsVisible = false;
        }

        private void frmInternalMiSaludSubMenu_Tapped(object sender, EventArgs e)
        {
            //this.popupMiSalud.IsVisible = true;
        }

        private void frmCancelarErrorConsultaVirtual_Tapped(object sender, EventArgs e)
        {
            this.popupErrorConsultaVirtual.IsVisible = false;
        }

        private void frmEmergencias_Tapped(object sender, EventArgs e)
        {
            this.popupEmergencias.IsVisible = true;
        }

        private void frmCancelarEmergencias_Tapped(object sender, EventArgs e)
        {
            this.popupEmergencias.IsVisible = false;
        }

        private void frmPolicia_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_Llamar104");
            this.popupEmergencias.IsVisible = false;
            Llamar(104);
        }

        private async void Llamar(int intNumeroTelefono)
        {
            try
            {
                PhoneDialer.Open(intNumeroTelefono.ToString());
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("SURA", "No se encuentra marcador de teléfono", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("SURA", "No se puede llamar al teléfono: " + string.Format("te:{0}", intNumeroTelefono), "OK");
            }
        }

        private void frmBomberos_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_Llamar103");
            this.popupEmergencias.IsVisible = false;
            Llamar(103);
        }

        private void frmLlamarSURA_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_Llamar8008888");
            this.popupContactar.IsVisible = false;
            Llamar(8008888);
        }

        private void frmLlamar911_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_Llamar911");
            this.popupContactar.IsVisible = false;
            Llamar(911);
        }

        private void frmAsistenciaVialTel_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_AsistTelefonica");
            this.popupContactar.IsVisible = false;
            Llamar(8007872);
        }

        private void frmAsistenciaVialWhatsapp_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_WhatsApp");
            long WhatsApp = 50763287872;
            EjecutarAplicacion("https://wa.me/" + WhatsApp);
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

        private void frmWhatsApp_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_WhatsApp");
            this.popupContactar.IsVisible = false;
            long WhatsApp = 50762694636;
            EjecutarAplicacion("https://wa.me/" + WhatsApp);
        }

        private void frmCorreoElectronico_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_ContactoCorreo");
            this.popupContactar.IsVisible = false;
            EjecutarAplicacion(string.Format("mailto: atencionalasegurado@sura.com.pa?subject=Atención al cliente"));
        }

        private async void frmSitioWeb_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_SitioWeb");
            this.popupContactar.IsVisible = false;
            Uri objUri = new Uri("https://segurossura.com.pa/");
            await Browser.OpenAsync(objUri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.White,
                PreferredControlColor = Color.Blue
            });
        }

        private async void frmCentroAutos_Tapped(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAnalyticsService>().LogEvent("ACT_CentroAutos");
            this.popupContactar.IsVisible = false;
            Uri objUri = new Uri("https://segurossura.com.pa/citas-autos-sura/");
            await Browser.OpenAsync(objUri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.White,
                PreferredControlColor = Color.Blue
            });
        }



        private void frmAsistenciaVial_Tapped(object sender, EventArgs e)
        {
            this.popupEmergencias.IsVisible = false;
            MostrarVista("AsistenciaVial");
        }

        private void FormularioContacto_Tapped(object sender, EventArgs e)
        {
            this.popupContactar.IsVisible = false;
            MostrarVista("FormularioContacto");
        }

        private void frmChatEnLinea_Tapped(object sender, EventArgs e)
        {
            this.popupContactar.IsVisible = false;
            MostrarVista("ChatEnLinea");
        }

        private void btnUbicaciones_Tapped(object sender, EventArgs e)
        {
            App.objContenedor.MostrarVista("Ubicaciones");
        }

        private void btnUsuario_Tapped(object sender, EventArgs e)
        {
            this.popupUsuario.IsVisible = false;
            App.objContenedor.MostrarVista("Perfil");
        }

        private void btnIntDirectorioProvSubMenu_Tapped(object sender, EventArgs e)
        {
            this.popupMiSalud.IsVisible = false;
            App.objContenedor.MostrarVista("DirectorioProveedores");
        }

        private void btnContrasena_Tapped(object sender, EventArgs e)
        {
            this.popupUsuario.IsVisible = false;
            App.objContenedor.MostrarVista("Contrasena");
        }

        private void frmNotificaciones_Tapped(object sender, EventArgs e)
        {
            this.popupContactar.IsVisible = false;
            App.objContenedor.MostrarVista("Notificaciones");
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (App.strVista != "Inicial")
                {
                    IrAtras();
                }
                else
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeAreaios = On<iOS>().SafeAreaInsets();
            gridMayor.Padding = safeAreaios;
            imgBackground.Margin = safeAreaios;

            if (!this.alreadyShowUpdate)
            {
                this.alreadyShowUpdate = true;

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        _ = GetiOSStoreAppVersion();
                        break;
                    case Device.Android:
                        _ = GetAndroidStoreAppVersion();
                        break;
                    default:
                        break;
                }
            }
        }

        public async Task showAlertAsync(Uri uri)
        {
            var response = await DisplayAlert("Nueva versión de la aplicación", "Existe una nueva versión de la aplicación. ¿Desea ir a descargarla?", "Si", "No");

            if (response)
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
        }



        public async Task GetiOSStoreAppVersion()
        {
            string bundleId = AppInfo.PackageName;

            string url = "http://itunes.apple.com/lookup?bundleId=" + bundleId;

            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    string jsonString = webClient.DownloadString(string.Format(url));

                    var lookup = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);


                    if (lookup != null
                        && lookup.Count >= 1
                        && lookup["resultCount"] != null
                        && Convert.ToInt32(lookup["resultCount"].ToString()) > 0)
                    {

                        var results = JsonConvert.DeserializeObject<List<object>>(lookup[@"results"].ToString());


                        if (results != null && results.Count > 0)
                        {
                            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(results[0].ToString());
                            string iOsStoreAppVersion = values.ContainsKey("version") ? values["version"].ToString() : string.Empty;

                            //if (Convert.ToDouble(iOsStoreAppVersion) > Convert.ToDouble(1))
                            if (Convert.ToDouble(iOsStoreAppVersion) > Convert.ToDouble(AppInfo.VersionString))
                            {
                                Uri objUri;
                                if (DeviceInfo.DeviceType == DeviceType.Virtual)
                                {
                                    objUri = new Uri(@"https://apps.apple.com/us/app/apple-store/id1520462862");
                                }
                                else
                                {
                                    objUri = new Uri(@"itms-apps://itunes.apple.com/app/id1520462862");
                                }

                                await showAlertAsync(objUri);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<string> LoadParseWebAsync()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync("https://play.google.com/store/apps/details?id=" + AppInfo.PackageName + "&hl=en_CA");

                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStringAsync();
                    return response;
                }
            }

            return "";
        }

        public async Task GetAndroidStoreAppVersion()
        {

            try
            {
                var content = await LoadParseWebAsync();

                if (String.IsNullOrEmpty(content))
                {
                    return;
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);

                var currentVersionTag = doc.DocumentNode.Descendants().Where(x => x.Name == "div")
                                        .FirstOrDefault(fod => fod.InnerText == "Current Version");

                var valueVersion = currentVersionTag.NextSibling.InnerText;


                //if (Convert.ToDouble(valueVersion) > Convert.ToDouble(1))
                if (Convert.ToDouble(valueVersion) > Convert.ToDouble(AppInfo.VersionString))
                {
                    Uri objUri = new Uri("https://play.google.com/store/apps/details?id=" + AppInfo.PackageName + "&hl=en_CA");

                    await showAlertAsync(objUri);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
