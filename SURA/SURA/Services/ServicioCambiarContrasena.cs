using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Services
{
    class ServicioCambiarContrasena
    {
        public void CambiarContrasena(string viejaContrasena, string nuevaContrasena, string confirmarContra)
        {
            try
            {
                var client = new RestSharp.RestClient(App.PathServiciosSURA);
                var request = new RestSharp.RestRequest("/api/Account/ChangePassword", RestSharp.Method.POST);
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Authorization", "Bearer " + App.Token);
                request.AddJsonBody(new { OldPassword = viejaContrasena, NewPassword = nuevaContrasena, ConfirmPassword = confirmarContra });
                request.Timeout = 30000;
                var response = client.Execute(request);
                var status = response.StatusCode;
                if ((int)status == 200)
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Cambio de contraseña exitoso", "Ok");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("SURA", "Vuelva a intentarlo", "Ok");
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA", "Cambiar Contraseña: " + ex.Message, "Ok");
            }
        }
    }
}
