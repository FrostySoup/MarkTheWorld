using Data.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Square
    {
        public double neX { get; set; }
        public double neY { get; set; }
        public double swX { get; set; }
        public double swY { get; set; }
        public Color colors { get; set; }
        public string dotId { get; set; }

        public Square(CornersCorrds corners, Color color, string id)
        {
            neX = corners.neX;
            neY = corners.neY;
            swX = corners.swX;
            swY = corners.swY;
            colors = color;
            dotId = id;
        }
    }
}
