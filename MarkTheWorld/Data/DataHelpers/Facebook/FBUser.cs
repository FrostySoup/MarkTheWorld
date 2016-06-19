using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class Facebook
    {
        public class User
        {
            public string id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string locale { get; set; }
            public picture picture { get; set; }
        }       
    }
    public class picture
    {
        public data data { get; set; }
    }
    public class data
    {
        public bool is_silhouette { get; set; }
        public string url { get; set; }
    }
}
