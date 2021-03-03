using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Models
{
    public class AsistenciaVialModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
