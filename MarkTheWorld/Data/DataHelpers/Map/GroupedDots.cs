using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class GroupedDots
    {
        public double neX { get; set; }
        public double neY { get; set; }
        public double swX { get; set; }
        public double swY { get; set; }
        public List<Dot> dots { get; set; }
        public GroupedDots()
        {
            dots = new List<Dot>();
        }
    }
}
