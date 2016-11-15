using CSharpQuadTree;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class GroupedDotsForApi
    {
        public int count { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }

        public GroupedDotsForApi() { }

        public void addDots(GroupedDotsForApi otherDots)
        {            
            double bigger = (double)(count / otherDots.count);
            lon = (lon * bigger + otherDots.lon) / (1 + bigger);
            lat = (lat * bigger + otherDots.lat) / (1 + bigger);
            count += otherDots.count;
            otherDots = null;
        }

        public GroupedDotsForApi(List<TBQuadTreeNodeData> dots)
        {
            count = dots.Count;
            lon = 0;
            lat = 0;
            for (int i = 0; i < dots.Count; i++)
            {
                lon += dots[i].x;
                lat += dots[i].y;
            }
            lon /= dots.Count;
            lat /= dots.Count;
        }
    }
}
