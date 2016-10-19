using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class Events
    {
        public string id { get; set; }
        public string events { get; set; }
        public double longt { get; set; }
        public double lat { get; set; }
        public string userName { get; set; }

        public Events() { }

        public Events(string ev, double lon, double lati, string userN)
        {
            events = ev;
            longt = lon;
            lat = lati;
            userName = userN;
        }
        public Events(string ev)
        {
            events = ev;
        }
    }
}
