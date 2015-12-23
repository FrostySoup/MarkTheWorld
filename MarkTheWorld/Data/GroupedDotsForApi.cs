using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class GroupedDotsForApi
    {
        public int count { get; set; }
        public double nTemp { get; set; }
        public double sTemp { get; set; }

        public GroupedDotsForApi(List<Dot> dots)
        {
            count = dots.Count;
            nTemp = 0;
            sTemp = 0;
            for (int i = 0; i < dots.Count; i++)
            {
                nTemp += dots[i].lon;
                sTemp += dots[i].lat;
            }
            nTemp /= dots.Count;
            sTemp /= dots.Count;
        }
    }
}
