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
    }
}
