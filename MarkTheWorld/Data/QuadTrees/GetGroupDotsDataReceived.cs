using Data.Database;
using Data.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.QuadTrees
{
    public class GetGroupDotsDataReceived
    {
        public List<Dot> dots { get; set; }
        public CornersCorrds corners { get; set; }
        public double zoomLevel { get; set; }
    }
}
