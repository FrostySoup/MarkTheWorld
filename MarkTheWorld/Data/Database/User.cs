using Data.DataHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class User
    {
        public string Id { get; set; }

        [StringLength(25, MinimumLength = 4)]
        public string UserName { get; set; }

        public string Token { get; set; }

        [StringLength(30, MinimumLength = 6)]
        public string PasswordHash { get; set; }

        public List<string> dotsId { get; set; }

        public int points { get; set; }

        public List<UserEvent> eventsHistory { get; set; }

        public Colors colors { get; set; }

        public string countryCode { get; set; }

        public string state { get; set; }

        public DateTime lastDailyTime { get; set; }

        public string profilePicture { get; set; }

        public string fbID { get; set; }
    }
}