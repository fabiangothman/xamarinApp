using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SURA.Services
{
    class ServicioOlvidaContrasena
    {
        //No sirve con restsharp en iOS
        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(App.PathServiciosSURA)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public async Task<System.Net.HttpStatusCode> OlvideContrasena(string email)
        {
            try
            {
                var client = CreateClient();

                var response = await client.GetAsync($"/api/account/forgot?" +
                    $"email={email}");
                System.Net.HttpStatusCode status = response.StatusCode;

                return status;

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Olvide Contraseña: " + ex.Message, "Ok");
                throw ex;
            }

        }

            public bool VerificarCodigo(string email, string codigo)
            {
                try
                {
                    var client = new RestSharp.RestClient(App.PathServiciosSURA);
                    var request = new RestSharp.RestRequest("/api/account/accesscode", RestSharp.Method.GET);
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddParameter("email", email);
                    request.AddParameter("code", codigo);
                    request.Timeout = 3000;
                    var response = client.Execute(request);
                    var status = response.StatusCode;
                    string DataResponse = response.Content;
                    if (status != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }
                    else
                    {
                        App.Token = Newtonsoft.Json.JsonConvert.DeserializeObject(DataResponse).ToString();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Verificar Codigo: " + ex.Message, "Ok");
                    return false;
                }
            }
        
    }
}
