using Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTP_Identity.UserStore
{
    public partial class UserStore : IUserTwoFactorStore<AppUser, string>
    {

        public Task<bool> GetTwoFactorEnabledAsync(AppUser user)
        {
            return Task.FromResult(true);
        }

        public Task SetTwoFactorEnabledAsync(AppUser user, bool enabled)
        {
            return Task.FromResult(false);
        }
    }
}
