using Data.DataHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserName")]
        [StringLength(25, MinimumLength = 4)]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "Token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "PasswordHash")]
        [StringLength(30, MinimumLength = 6)]
        public string PasswordHash { get; set; }

        [JsonProperty(PropertyName = "dotsId")]
        public List<string> dotsId { get; set; }

        [JsonProperty(PropertyName = "points")]
        public int points { get; set; }

        [JsonProperty(PropertyName = "eventsID")]
        public List<string> eventsID { get; set; }

        [JsonProperty(PropertyName = "colors")]
        public Colors colors { get; set; }

        [JsonProperty(PropertyName = "countryCode")]
        public string countryCode { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string state { get; set; }

        [JsonProperty(PropertyName = "lastDailyTime")]
        public DateTime lastDailyTime { get; set; }

        [JsonProperty(PropertyName = "profilePicture")]
        public string profilePicture { get; set; }

        [JsonProperty(PropertyName = "fbID")]
        public string fbID { get; set; }
    }
}