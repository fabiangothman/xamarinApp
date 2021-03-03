using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Models
{
    public class ModeloDashboard
    {
        [JsonProperty("NOMBRE_COMPLETO", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string NombreCompleto { get; set; }

        [JsonProperty("NOMBRE", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Nombre { get; set; }

        [JsonProperty("APELLIDO", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Apellido { get; set; }

        [JsonProperty("IDENTIFICACION", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Identificacion { get; set; }

        [JsonProperty("TIPO_PERSONA", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TipoPersona { get; set; }

        [JsonProperty("TIPO_IDENTIFICACION", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TipoIdentificacion { get; set; }

        [JsonProperty("FECHA_NACIMIENTO", Required=Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string FechaNacimiento { get; set; }

        [JsonProperty("EDAD", Required=Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int Edad { get; set; } = 0;

        [JsonProperty("GENERO", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Genero { get; set; }

        [JsonProperty("CORREO", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Correo { get; set; }
        
        [JsonProperty("POLIZAS", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<MisPolizas> Polizas { get; set; }
    }
}