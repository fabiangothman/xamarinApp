using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System;
using System.Text;

namespace SURA.Models
{
    public class ModelTarjeta : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string _identificacion { get; set; }
        [JsonProperty("identificacion", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Identificacion
        {
            get
            {
                return _identificacion;
            }
            set
            {
                _identificacion = value;
                NotifyPropertyChanged("Identificacion");
            }
        }

        public string _tarjetaEnmascarada { get; set; }
        [JsonProperty("tarjetaEnmascarada", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string TarjetaEnmascarada
        {
            get
            {
                return _tarjetaEnmascarada;
            }
            set
            {
                _tarjetaEnmascarada = value;
                NotifyPropertyChanged("TarjetaEnmascarada");
                NotifyPropertyChanged("TarjetaEnmascaradaMasked");
            }
        }

        public string _tarjetaHabiente { get; set; }
        [JsonProperty("tarjetaHabiente", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string TarjetaHabiente
        {
            get
            {
                return _tarjetaHabiente;
            }
            set
            {
                _tarjetaHabiente = value;
                NotifyPropertyChanged("TarjetaHabiente");
            }
        }

        public string _token { get; set; }
        [JsonProperty("token", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Token
        {
            get
            { return _token; }
            set
            {
                _token = value;
                NotifyPropertyChanged("Token");
            }
        }

        public string _estado { get; set; }
        [JsonProperty("estado", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Estado
        {
            get
            { return _estado; }
            set
            {
                _estado = value;
                NotifyPropertyChanged("Estado");
            }
        }

        public string _fechaCreacion { get; set; }
        [JsonProperty("fechaCreacion", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FechaCreacion
        {
            get
            {
                return _fechaCreacion;
            }
            set
            {
                _fechaCreacion = value;
                NotifyPropertyChanged("FechaCreacion");
            }
        }

        public string TarjetaEnmascaradaMasked
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < TarjetaEnmascarada.Length; i++)
                {
                    if (i % 4 == 0 && i != 0)
                        sb.Append(' ');
                    sb.Append(TarjetaEnmascarada[i]);
                }
                return sb.ToString();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
