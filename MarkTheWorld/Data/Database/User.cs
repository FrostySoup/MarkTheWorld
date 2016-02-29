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

        public string UserName { get; set; }

        public Guid Token { get; set; }

        public string PasswordHash { get; set; }

        public List<string> dotsId { get; set; }

        public int points { get; set; }

        public List<UserEvent> eventsHistory { get; set; }

        public bool pointsAvailable { get; set; }

        public Colors colors { get; set; }
    }
}