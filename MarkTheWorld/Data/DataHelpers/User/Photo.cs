using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers.User
{
    public class Photo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }

        public Photo(string n, string p, long s)
        {
            Name = n;
            Path = p;
            Size = s;
        }
    }
}
