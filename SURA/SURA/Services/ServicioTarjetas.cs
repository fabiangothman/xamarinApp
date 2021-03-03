using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Rg.Plugins.Popup.Services;
using SURA.Models;
namespace SURA.Services
{
    public class ServicioTarjetas
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

        public async Task ObtenerListaTarjetas(string miCedula)
        {
            try
            {
                var client = CreateClient();

                var response = await client.GetAsync($"/api/tarjeta/{miCedula}");

                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    var arrayTarjetas = Newtonsoft.Json.Linq.JObject.Parse(contenido).GetValue("data");

                    App.modeloPlataformaPago.TarjetasCreadas = arrayTarjetas.ToObject<ObservableCollection<ModelTarjeta>>();
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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task CrearTokenTarjetas(Dictionary<string, string> parameters, bool recordarTarjeta)
        {
            try
            {
                var client = CreateClient();

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/tarjeta/token", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    var data = Newtonsoft.Json.Linq.JObject.Parse(contenido)["data"];
                    var codigoResp = data["codigoRespuesta"];

                    if (codigoResp.ToString() != "T00" && codigoResp.ToString() != "T02")
                    {
                        throw new Exception(data["descripcionRespuesta"].ToString());
                    }

                    if (recordarTarjeta) { 
                        var tokenDetalles = data["tokenDetalle"];
                        Dictionary<string, string> parameterGuardar = new Dictionary<string, string>()
                        {
                            {"identificacion", App.modeloDashboard.Identificacion },
                            {"token", tokenDetalles["token"].ToString() },
                            {"tarjetaEnmascarada", tokenDetalles["tarjetaEnmascarada"].ToString() },
                            {"tarjetaHabiente", tokenDetalles["tarjetaHabiente"].ToString() },
                        };

                        await GuardarTokenTarjeta(parameterGuardar);
                    }
                    App.modeloPlataformaPago.TokenTarjeta = data["tokenDetalle"]["token"].ToString();
                    App.modeloPlataformaPago.CVVTarjeta = parameters["cvv"];
                    App.objContenedor.MostrarVista("ConfirmacionPago");
                    await PopupNavigation.Instance.PopAsync();
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
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task GuardarTokenTarjeta(Dictionary<string, string> parameters)
        {
            try
            {
                var client = CreateClient();

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/tarjeta", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    var data = Newtonsoft.Json.Linq.JObject.Parse(contenido)["token"];
                    App.modeloPlataformaPago.TarjetasCreadas.Add(data.ToObject<ModelTarjeta>());
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
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task EliminarTarjeta(Dictionary<string, string> parameters)
        {
            try
            {
                var client = CreateClient();

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);

                HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Delete;
                request.Content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                request.RequestUri = new Uri($"/api/tarjeta");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return;
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
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
