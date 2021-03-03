using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SURA.Models
{
    public class ConnectRegisteredUser
    {
        [JsonProperty("account_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string AccountId { get; set; }

        [JsonProperty("service", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Service ServiceList { get; set; }
    }

    public class Service
    {
        [JsonProperty("situation", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Situation { get; set; }

        [JsonProperty("external", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public External ExternalList { get; set; }

        [JsonProperty("client", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Client ClientList { get; set; }

        [JsonProperty("vehicle", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Vehicle VehicleList { get; set; }

        [JsonProperty("route", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Route RouteList { get; set; }
    }

    public class Client
    {
        [JsonProperty("firstname", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Firstname { get; set; }

        [JsonProperty("lastname", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Lastname { get; set; }

        [JsonProperty("phone1", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Phone1 { get; set; }

        [JsonProperty("email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
    }

    public class External
    {
        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string IdString { get; set; }
    }

    public class Route
    {
        [JsonProperty("situation", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Situation Situation { get; set; }
    }

    public class Situation
    {
        [JsonProperty("address", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty("latitude", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Latitude { get; set; }

        [JsonProperty("longitude", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Longitude { get; set; }
    }

    public class Vehicle
    {
        [JsonProperty("year", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Year { get; set; }

        [JsonProperty("make", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Make { get; set; }

        [JsonProperty("model", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }

        [JsonProperty("plate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Plate { get; set; }

        [JsonProperty("color", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("vin", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Vin { get; set; }
    }
}
