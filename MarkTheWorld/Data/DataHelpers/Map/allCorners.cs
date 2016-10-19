using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers.Map
{
    public class AllCorners
    {
        public AllCorners(double lon, double lat, double squareA)
        {
            neX = lon + squareA;
            neY = lat + squareA;
            nwX = lon - squareA;
            nwY = lat + squareA;
            swX = lon - squareA;
            swY = lat - squareA;
            seX = lon + squareA;
            seY = lat + squareA;
        }

        public double neX { get; set; }
        public double neY { get; set; }
        public double nwX { get; set; }
        public double nwY { get; set; }
        public double swX { get; set; }
        public double swY { get; set; }
        public double seX { get; set; }
        public double seY { get; set; }
    }
}
