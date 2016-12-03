using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuadTree.Models
{
    public class GetGroupDotsDataReceived
    {
        public List<Dot> dots { get; set; }
        public CornersCorrds corners { get; set; }
        public double zoomLevel { get; set; }
    }
}