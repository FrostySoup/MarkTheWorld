using Data.DataHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Data
{
    public class Square
    {
        public double neX { get; set; }
        public double neY { get; set; }
        public double swX { get; set; }
        public double swY { get; set; }
        public string colors { get; set; }
        public string borderColors { get; set; }
        public string dotId { get; set; }

        public Square(CornersCorrds corners, Colors color, string id, string borders)
        {
            neX = corners.neX;
            neY = corners.neY;
            swX = corners.swX;
            swY = corners.swY;
            borderColors = borders;
            colors = string.Format("rgb({0}, {1}, {2})", color.red, color.green, color.blue);
            dotId = id;
        }
    }
}
