using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Dot
    {
        public string Id { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
        public string username { get; set; }
        public Dot() { }
        public Dot(double lanc, double latc)
        {
            lat = latc;
            lon = lanc;
        }
    }
}
