using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserEvent
    {
        public string events { get; set; }
        public double longt { get; set; }
        public double lat { get; set; }
        public string userName { get; set; }

        public UserEvent() {}

        public UserEvent(string ev, double lon, double lati, string userN)
        {
            events = ev;
            longt = lon;
            lat = lati;
            userName = userN;
        }
    }
}
