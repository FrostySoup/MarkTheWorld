using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class GroupedSquares
    {
        public double neX { get; set; }
        public double neY { get; set; }
        public double swX { get; set; }
        public double swY { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
        public string username { get; set; }
        public GroupedSquares(CornersCorrds cords, string messag, DateTime data, string usernam)
        {
            neX = cords.neX;
            neY = cords.neY;
            swX = cords.swX;
            swY = cords.swY;
            message = messag;
            date = data;
            username = usernam;
        }
    }
}
