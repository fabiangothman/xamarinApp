using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SURA.Services
{
    public class ServicioPreferencias
    {

        public async Task<string> ObtenerLlave(string llave)
        {
            string contrasena = await SecureStorage.GetAsync(llave);
            return contrasena;
        }

        public string HabilitarBiometrico()
        {
            string start = Preferences.Get("tips", "");
            if (start == "")
            {
                Preferences.Set("tips", "1");
            }
            return "Done";
        }

        public bool HabilitarHuella(bool estaHabilitado)
        {
            Preferences.Remove("estaHabilitado");
            Preferences.Set("estaHabilitado", estaHabilitado);
            return estaHabilitado;
        }

        public async void GuardarNombreUsuario(string nombreUsuario)
        {
            SecureStorage.Remove("NombreUsuario");
            await SecureStorage.SetAsync("NombreUsuario", nombreUsuario);
            //Preferences.Set("NombreUsuario", nombreUsuario);
        }

        public async void GuardarPassword(string pass)
        {
            SecureStorage.Remove("pass");
            //Preferences.Set("pass", pass);
            await SecureStorage.SetAsync("pass", pass);
        }

        public async void EliminarPassword(string pass)
        {
            SecureStorage.Remove("pass");
        }

        public async Task<string> ObtenerNombreUsuario(string nombreUsuario)
        {
            string nombre = await SecureStorage.GetAsync(nombreUsuario);
            App.NombreUsuario = nombre;
            return nombre;
        }

        public async Task<string> ObtenerPassword(string password)
        {
            string pass = await SecureStorage.GetAsync(password);
            App.Password = pass;
            return pass;
        }

        public void HabilitarMensaje(bool estaHabilitado)
        {
            Preferences.Remove("MensajePronto");
            Preferences.Set("MensajePronto", estaHabilitado);
        }

        public bool VerMensaje()
        {
            bool NoVolverMostrar = Preferences.Get("MensajePronto", false);
            return NoVolverMostrar;
        }


    }
}
