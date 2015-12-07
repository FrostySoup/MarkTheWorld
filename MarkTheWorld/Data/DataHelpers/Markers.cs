using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class Markers
    {
        public DateTime date { get; set; }
        public string username{ get; set; }
        public string message { get; set; }
        public Markers(DateTime data, string user, string mess)
        {
            date = data;
            username = user;
            message = mess;
        }
    }
}
