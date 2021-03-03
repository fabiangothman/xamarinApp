using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Rg.Plugins.Popup.Services;
using SURA.Models;
using SURA.Views.AppPrivado.ElementosDashboard.PasarelaPago;

namespace SURA.Services
{
    public class ServicioPagos
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

        public async Task ObtenerDatos(string miCedula)
        {
            try
            {
                var client = CreateClient();

                var response = await client.GetAsync($"/api/boton-pago/cliente/{miCedula}/polizas");

                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    var arrayInsurances = Newtonsoft.Json.Linq.JObject.Parse(contenido).GetValue("data");
                    App.modeloPlataformaPago = new ModelPlataformaPago();
                    App.modeloPlataformaPago.PolizasAPagar = arrayInsurances.ToObject<List<ModelPolizaAPagar>>();
                    App.plataformaPagoShouldInitialize = false;
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

        public async Task PagarPolizas(bool enviarCorreo)
        {
            try
            {
                var client = CreateClient();

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(ObtenerParametrosPago(enviarCorreo));
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/boton-pago/pago", stringContent);

                await PopupNavigation.Instance.PopAllAsync();
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    var data = Newtonsoft.Json.Linq.JObject.Parse(contenido).GetValue("data");

                    if(data["estado"].ToString() == "Exitoso")
                    {
                        var reciboData = data["recibo"];
                        App.modeloPlataformaPago.NumeroRecibo = reciboData["numeroRecibo"].ToString();
                        App.modeloPlataformaPago.NombreCliente = reciboData["nombreCliente"].ToString();
                        App.modeloPlataformaPago.RecibosPolizas = reciboData["polizas"].ToObject<List<ReciboPolizaModel>>();
                        App.objContenedor.MostrarVista("PagoSatisfactorio");
                    }
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

        private Dictionary<string, object> ObtenerParametrosPago(bool enviarCorreo)
        {
            List<Dictionary<string, string>> listaPolizas = new List<Dictionary<string, string>>();

            foreach(var poliza in App.modeloPlataformaPago.PolizasAPagar.Where(w => w.Selected).ToList())
            {
                var dic = new Dictionary<string, string>()
                {
                    { "poliza", poliza.Poliza},
                    { "monto", poliza.TotalPoliza.ToString()},
                    { "solucion", poliza.Solucion},
                };
                listaPolizas.Add(dic);    
            }

            var parameters = new Dictionary<string, object>()
            {
                {"identificacion", App.modeloDashboard.Identificacion },
                {"montoPago", App.modeloPlataformaPago.PolizasAPagar.Where(w => w.Selected).Sum(s => Convert.ToDouble(s.TotalPoliza)).ToString() },
                {"token", App.modeloPlataformaPago.TokenTarjeta },
                {"correo", enviarCorreo ? App.modeloDashboard.Correo : ""},
                {"cvv", App.modeloPlataformaPago.CVVTarjeta},
                {"polizas", listaPolizas.ToArray() },
            };

            return parameters;
        }
    }
}
