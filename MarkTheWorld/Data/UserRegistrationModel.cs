using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserRegistrationModel
    {
        public Boolean success { get; set; }
        public string message { get; set; }
        public enum message2 : int
        {
            Unknown,
            Fail,
            Success
        }; 
        public Guid Token { get; set; }
    }
}
