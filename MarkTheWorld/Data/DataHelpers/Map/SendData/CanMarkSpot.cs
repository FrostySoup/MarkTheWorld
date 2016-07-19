using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers.Map
{
    public class CanMarkSpot
    {
        public bool CanMark { get; set; }
        public string MarkedUsername { get; set; }
        public CornersCorrds Coorners { get; set; }
        public double CircleR { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}
