using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class SquaresWithInfo
    {
        public double neX { get; set; }
        public double neY { get; set; }
        public double swX { get; set; }
        public double swY { get; set; }
        public List<Markers> markers { get; set; }
        public Colors Colors { get; set; }
        public SquaresWithInfo(double nex, double ney, double swx, double swy, Markers marker, Colors colors)
        {
            neX = nex;
            neY = ney;
            swX = swx;
            swY = swy;
            markers = new List<Markers>();
            markers.Add(marker);
            Colors = colors;
        }
    }
}
