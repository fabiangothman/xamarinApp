using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SURA.Services
{
    class ServicioIngresar
    {
        public Action DisplayInvalidLoginPrompt;
        public string ObtenerToken(string username, string password)
        {
            try
            {
                string token = null;
                var client = new RestClient(App.PathServiciosSURA);
                var request = new RestRequest("/api/Account/Authenticate", RestSharp.Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new {User = username, password = password, gran_type = "password"});
                var response = client.Execute(request);
                string DataResponse = response.Content;
                if (DataResponse == "{\"Message\":\"An error has occurred.\"}")
                {
                    throw new Exception("Usuario o contraseña incorrecta");
                }
                else
                {
                    if (DataResponse == "")
                    {
                        throw new Exception("Usuario o contraseña incorrecta");
                    }
                    else if((int)response.StatusCode == 200)
                    {
                        token = Newtonsoft.Json.JsonConvert.DeserializeObject(DataResponse).ToString();
                    }
                }
                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
