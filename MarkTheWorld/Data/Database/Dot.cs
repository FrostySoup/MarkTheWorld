using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class Dot
    {      

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "nextCapLat")]
        public double nextCapLat { get; set; }

        [JsonProperty(PropertyName = "nextCapLon")]
        public double nextCapLon { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double lat { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public double lon { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string message { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime date { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }

        [JsonProperty(PropertyName = "photoPath")]
        public string photoPath { get; set; }

        public Dot() { }
        public Dot(double lanc, double latc)
        {
            lat = latc;
            lon = lanc;
        }
    }
}
