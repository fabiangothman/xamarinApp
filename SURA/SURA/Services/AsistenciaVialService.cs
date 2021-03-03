using System;
using Xamarin.Forms;

namespace SURA.Services
{
    public class AsistenciaVialService
    {
        public void AsistenciaVial(string data)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(data))
                {
                    var client = new RestSharp.RestClient("https://helios-api-prod.herokuapp.com");
                    var request = new RestSharp.RestRequest("/api/v1/service-requests", RestSharp.Method.POST);
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddParameter("application/json", data, RestSharp.ParameterType.RequestBody);
                    request.Timeout = 3000;
                    RestSharp.IRestResponse response = client.Execute(request);
                    string DataResponse = response.Content;
                    Models.AsistenciaVialModel Result = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.AsistenciaVialModel>(DataResponse);

                    if (Result.Status == "false")
                    {
                        Application.Current.MainPage.DisplayAlert("SURA", "Su solicitud ha sido rechazada", "Ok");
                    }
                    else
                    {
                        if (Result.Status == "true")
                        {
                            Application.Current.MainPage.DisplayAlert("SURA", "Su solicitud ha sido enviada satisfactoriamente", "Ok");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
