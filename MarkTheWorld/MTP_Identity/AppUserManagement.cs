using Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTP_Identity
{
    public class AppUserManagement : UserManager<AppUser>
    {
        public AppUserManagement(IUserStore<AppUser> store)
            : base(store)
        {

        }
    }
}