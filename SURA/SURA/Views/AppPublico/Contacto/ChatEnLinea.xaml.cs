using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;


namespace SURA.Views.AppPublico.Contacto
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatEnLinea : ContentView
    {
        private HtmlWebViewSource htmlSource;
        public ChatEnLinea()
        {
            try
            {
                InitializeComponent();
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
                        .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
                    CargarWebViewChat();
                    lblInternet.IsVisible = false;
                }
                else
                {
                    lblInternet.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Chat en linea: " + ex.Message, "Ok");
            }
        }

        private async void BotonAtras()
        {
            try
            {
                if (App.blnPrivado)
                    App.objContenedor.MostrarVista("Dashboard");
                else
                    App.objContenedor.MostrarVista("Inicial");
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA","Chat en linea: "+ex.Message,"Ok");
            }
        }

        private void ContenidoWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            try
            {
                var url = e.Url;

                if (url == "https://segurossura.com.pa/")
                {
                    contenidoWebView.IsVisible = false;
                    lblInternet.IsVisible = true;
                    lblInternet.Text = "¡Gracias por contactarnos!";
                    e.Cancel = true;
                    if(Device.RuntimePlatform == Device.iOS)
                    {
                        BotonAtras();
                    }
                    
                }
            }
            catch(Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA","Hubo un problema con el chat: "+ex.Message,"Ok");
            }
        }

        void CargarWebViewChat()
        {
            try
            {
                htmlSource = new HtmlWebViewSource
                {
                    Html = @"<html>
                    <head>
                        <meta name='viewport' content='width=devide-width,initial-scale=1,maximum=scale=1' />
                        <style>
                            .spinner {
                              margin: 80px auto 0;
                              width: 70px;
                              text-align: center;
                            }
                            
                            .spinner > div {
                              width: 18px;
                              height: 18px;
                              background-color: #676767;
                            
                              border-radius: 100%;
                              display: inline-block;
                              -webkit-animation: sk-bouncedelay 1.4s infinite ease-in-out both;
                              animation: sk-bouncedelay 1.4s infinite ease-in-out both;
                            }
                            
                            .spinner .bounce1 {
                              -webkit-animation-delay: -0.32s;
                              animation-delay: -0.32s;
                            }
                            
                            .spinner .bounce2 {
                              -webkit-animation-delay: -0.16s;
                              animation-delay: -0.16s;
                            }
                            
                            @-webkit-keyframes sk-bouncedelay {
                              0%, 80%, 100% { -webkit-transform: scale(0) }
                              40% { -webkit-transform: scale(1.0) }
                            }
                            
                            @keyframes sk-bouncedelay {
                              0%, 80%, 100% { 
                                -webkit-transform: scale(0);
                                transform: scale(0);
                              } 40% { 
                                -webkit-transform: scale(1.0);
                                transform: scale(1.0);
                              }
                            }
                            
                        </style>
                    </head>
                    <body>

                    <script type='text/javascript' src='https://c.la1-c1-iad.salesforceliveagent.com/content/g/js/47.0/deployment.js'></script>
                    <script type='text/javascript'>
                    window.name = ""mywindow"";
                    liveagent.init('https://d.la1-c1-iad.salesforceliveagent.com/chat', '5720b00000096ag', '00D0b000000wbal');
                    </script>

                    <div style = ""height: 100vh; position: relative; text-align:center;"">
                        <div align=""center"" style=""position: absolute; top: 35%; left: 50%; transform: translateY(-40%); transform: translateX(-50%);"" >
                            <div class=""spinner"" id=""loader"">
                              <div class=""bounce1""></div>
                              <div class=""bounce2""></div>
                              <div class=""bounce3""></div>
                            </div>
                            <div id=""chatonline"" style=""display: none;""></div>
                            <span id =""lblNoDisponibilidad"" style=""display: none;"">¡Gracias por contactarnos!</span>
                            <p> </p>
                            <img id=""liveagent_button_offline_5730b00000097Iz"" style=""display: none; border: 0px none;;"" src=""https://sura-pa.secure.force.com/encuestas/resource/1553015675000/IcChat2"" />
                            <span id =""lblNoDisponibilidad2"" style=""display: none;"">Nuestros representantes no están disponibles.</span>
                        </div>
                    </div>

                    <script type=""text/javascript"">
                    if (!window._laq) { window._laq = []; }

                    window._laq.push(function(){
                        liveagent.showWhenOnline('5730b00000097Iz', document.getElementById('chatonline'));
                        liveagent.showWhenOnline('5730b00000097Iz', document.getElementById('loader'));

                        liveagent.showWhenOffline('5730b00000097Iz', document.getElementById('liveagent_button_offline_5730b00000097Iz'));
                        liveagent.showWhenOffline('5730b00000097Iz', document.getElementById('lblNoDisponibilidad'));
                        liveagent.showWhenOffline('5730b00000097Iz', document.getElementById('lblNoDisponibilidad2'));
                        
                    });
                    
                    liveagent.addButtonEventHandler('5730b00000097Iz',function (e){
                        if(e == liveagent.BUTTON_EVENT.BUTTON_AVAILABLE){
                            liveagent.startChatWithWindow('5730b00000097Iz','mywindow');
                         }       
                    });
                    document.getElementById('loader').style.display = 'none';
                    
                    </script>

                    </body>
                    </html>"
                };
                contenidoWebView.Source = htmlSource;
            }
            catch
            {

            }
            
        }

        /*<span id =""lblDisponibilidad"" style=""display: none;"">¿Tienes alguna consulta?</span>
         * <img id=""liveagent_button_online_5730b00000097Iz"" style=""display: none; border: 0px none; cursor: pointer; "" onclick=""liveagent.startChatWithWindow('5730b00000097Iz','mywindow')"" src=""https://sura-pa.secure.force.com/encuestas/resource/1553015667000/IcChat1"" />
         * <span id =""lblDisponibilidad2"" style=""display: none;"">Haz clic aquí</span>
         * liveagent.showWhenOnline('5730b00000097Iz', document.getElementById('liveagent_button_online_5730b00000097Iz'));
                liveagent.showWhenOnline('5730b00000097Iz', document.getElementById('lblDisponibilidad'));
                liveagent.showWhenOnline('5730b00000097Iz', document.getElementById('lblDisponibilidad2'));*/
    }
}