using Data.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class User
    {
        public string Id { get; set; }

        // 3 < name < 25
        public string UserName { get; set; }

        public string Token { get; set; }

        public string PasswordHash { get; set; }

        public List<string> dotsId { get; set; }

        public int points { get; set; }

        public List<UserEvent> eventsHistory { get; set; }

        public Color colors { get; set; }

        public string countryCode { get; set; }

        public DateTime lastDailyTime { get; set; }

        public string profilePicture { get; set; }

        public int fbID { get; set; }
    }
}