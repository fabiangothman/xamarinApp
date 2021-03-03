using System;
using System.Globalization;
using Newtonsoft.Json;

namespace SURA.Models
{
    public class ReciboPolizaModel
    {
        [JsonProperty("numeroPoliza", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string NumeroPoliza { get; set; }

        [JsonProperty("numeroRecibo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string NumeroRecibo { get; set; }

        [JsonProperty("montoPoliza", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public double MontoPoliza { get; set; }

        public string MontoPolizaString
        {
            get
            {
                return MontoPoliza.ToString("C", new CultureInfo("es-PA"));
            }
        }

        [JsonProperty("solucion", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Solucion { get; set; }
    }
}
