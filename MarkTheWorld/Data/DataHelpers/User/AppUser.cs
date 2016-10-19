using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Data
{
    public class AppUser : IUser<string>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public Guid Token { get; set; }

        public string PasswordHash { get; set; }

    }
}