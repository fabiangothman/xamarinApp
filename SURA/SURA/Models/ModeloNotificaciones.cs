using System;
using System.Text;

namespace SURA.Models
{
    [Serializable()]
    public class ModeloNotificaciones
    {
        [SQLite.PrimaryKey]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string MessageDateTime { get; set; }
        public bool Read { get; set; }
    }

    public class ResultadoNotificationsInfo
    {
        [System.Runtime.Serialization.DataMember(Name = "PushNotification")]
        public System.Collections.Generic.IEnumerable<ModeloNotificaciones> PushNotification { get; set; }

    }
}
