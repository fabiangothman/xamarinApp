using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Models
{
    public class ConnectUnregisteredUser
    {
        public string account_id { get; set; }

        public ServiceU service { get; set; }
    }

    public class ServiceU
    {
        public string situation { get; set; }

        public ClientU client { get; set; }

        public VehicleU vehicle { get; set; }

        public RouteU route { get; set; }
    }

    public class ClientU
    {
        public string firstname { get; set; }

        public string lastname { get; set; }

        public string phone1 { get; set; }

        public string email { get; set; }
    }

    public class RouteU
    {
        public SituationU situation { get; set; }
    }

    public class SituationU
    {
        public string address { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }
    }

    public class VehicleU
    {
        public string year { get; set; }

        public string make { get; set; }

        public string model { get; set; }

        public string plate { get; set; }

        public string color { get; set; }

        public string vin { get; set; }
    }
}
