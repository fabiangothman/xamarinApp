using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Plugin.Permissions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SURA.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPrivado.ElementosDashboard.PasarelaPago
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagoSatisfactorio : ContentView
    {
        public PagoSatisfactorio()
        {
            InitializeComponent();
            txtClienteNombre.Text = App.modeloPlataformaPago.NombreCliente;

            BindableLayout.SetItemsSource(StkRecibos, App.modeloPlataformaPago.RecibosPolizas);

            var values = App.modeloPlataformaPago.RecibosPolizas.Sum(x => x.MontoPoliza);
            TotalRecibos.Text = values.ToString("C", new CultureInfo("es-PA"));
        }

        void RegresarTapped(System.Object sender, System.EventArgs e)
        {

            App.objContenedor.MostrarVista("Dashboard");
        }

        void DescargarReciboTapped(System.Object sender, System.EventArgs e)
        {
            DescargarRecibo();
        }

        private async void DescargarRecibo()
        {
            try
            {
                var screenshotBytes = await DependencyService.Get<IScreenshotManager>().CaptureAsync(StkContent);
                await SolicitarPermiso();
                var img = "recibo_" + App.modeloPlataformaPago.NumeroRecibo + ".png";
                var file = Path.Combine(FileSystem.CacheDirectory, img);

                File.WriteAllBytes(file, screenshotBytes);

                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(Path.Combine(FileSystem.CacheDirectory, img))
                });

            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "No se pudo descargar el recibo. Intentelo de nuevo, más tarde", "Ok");
            }
        }

        private async System.Threading.Tasks.Task SolicitarPermiso()
        {
            try
            {
                var res = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();
                if (res == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                    return;
                else
                {
                    await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Permiso estado de cuenta: " + ex.Message, "Ok");
            }
        }

        
    }
}
