using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Services
{
    class ServicioEstablecerContrasena
    {
        public bool EstablecerContrasena(string contrasena, string confirmarContrasena)
        {
            try
            {
                var client = new RestSharp.RestClient(App.PathServiciosSURA);
                var request = new RestSharp.RestRequest("/api/account/SetPassword", RestSharp.Method.POST);
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Authorization", "Bearer " + App.Token);
                request.AddJsonBody(new { NewPassword = contrasena, ConfirmPassword = confirmarContrasena });
                request.Timeout = 30000;
                var response = client.Execute(request);
                System.Net.HttpStatusCode status = response.StatusCode;
                if (status == 0)
                {
                    return false;
                }
                else
                {
                    if (status == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return false;
                    }
                    else
                    {
                        if (status == System.Net.HttpStatusCode.OK)
                        {
                            App.Current.MainPage.DisplayAlert("SURA", "Cambio de contraseña exitoso", "Ok");
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("SURA","Establecer Contraseña: " + ex.Message,"Ok");
                return false;
            }
        }
    }
}
