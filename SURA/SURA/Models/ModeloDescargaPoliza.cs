using System;
using Newtonsoft.Json;
namespace SURA.Models
{
    public class ModeloDescargaPoliza
    {
        [JsonProperty("generatedAt")]
        public object GeneratedAt { get; set; }

        [JsonProperty("pdf")]
        public string PDF { get; set; }
    }
}
