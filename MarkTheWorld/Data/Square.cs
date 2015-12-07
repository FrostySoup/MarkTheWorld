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
        public string message { get; set; }
        public DateTime date { get; set; }

        public Square(string messag, DateTime data, CornersCorrds corners)
        {
            neX = corners.neX;
            neY = corners.neY;
            swX = corners.swX;
            swY = corners.swY;
            message = messag;
            date = data;
        }
    }
}
