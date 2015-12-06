using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DotFromViewModel
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
        public Guid username { get; set; }
    }
}
