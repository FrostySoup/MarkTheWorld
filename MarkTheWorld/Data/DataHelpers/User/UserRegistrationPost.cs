using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ReceivePostData
{
    public class UserRegistrationPost
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string CountryCode { get; set; }

        public bool checkIfValiable()
        {
            if (UserName.Length > 25 || UserName.Length < 3)
                return false;
            if (PasswordHash.Length > 30 || PasswordHash.Length < 6)
                return false;
            return true;
        }
    }
}
