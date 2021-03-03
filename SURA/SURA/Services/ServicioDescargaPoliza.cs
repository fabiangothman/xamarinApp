using Plugin.Permissions;
using SURA.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace SURA.Services
{
    public class ServicioDescargaPoliza
    {
        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(App.PathServiciosSURA)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token);

            return httpClient;
        }

        public async Task DescargarPoliza(string miPoliza, CancellationToken ct)
        {
            try
            {
                var client = CreateClient();

                var response = await client.GetAsync($"api/DescargarPoliza?" +
                    $"poliza={miPoliza}", ct);

                if (response.IsSuccessStatusCode)
                {
                    
                    String contenido = await response.Content.ReadAsStringAsync();
                    
                    var parsed = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.ModeloDescargaPoliza>(contenido);
                    var sPDFDecoded = System.Convert.FromBase64String(parsed.PDF);
                    await SolicitarPermiso();

                    string localFilename = "_temp.pdf";
                    string filePath = Path.Combine(FileSystem.CacheDirectory, localFilename);
                    DependencyService.Get<IMensajes>().LongAlert("Descargando archivo...");
                    File.WriteAllBytes(filePath, sPDFDecoded);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Su sesión ha expirado. Por favor, inicie sesión de nuevo.");
                }
                else
                {
                    throw new Exception("Lo sentimos, no hemos podido procesar tu solicitud en este momento.\n\nSi necesitas asistencia por favor contáctanos a nuestra línea de atención al cliente al 800-8888");
                }
            }
            catch(TaskCanceledException tce)
            {
                throw tce;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Socket closed")
                    throw new TaskCanceledException();

                throw new Exception(ex.Message);
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
