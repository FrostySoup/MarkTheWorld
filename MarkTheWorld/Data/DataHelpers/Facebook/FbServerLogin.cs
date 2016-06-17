using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class FbServerLogin
    {
        public string longToken { get; set; }
        public Country country { get; set; }
        public string username { get; set; }
        public bool newUser { get; set; }
    }
}
