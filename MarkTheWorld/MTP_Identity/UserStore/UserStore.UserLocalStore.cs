using Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTP_Identity.UserStore
{
    public partial class UserStore : IUserLockoutStore<AppUser, string>
    {

        public Task<int> GetAccessFailedCountAsync(AppUser user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(AppUser user)
        {
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(AppUser user)
        {
            return Task.FromResult(default(DateTimeOffset));
        }

        public Task<int> IncrementAccessFailedCountAsync(AppUser user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(AppUser user)
        {
            return Task.FromResult(true);
        }

        public Task SetLockoutEnabledAsync(AppUser user, bool enabled)
        {
            return Task.FromResult(true);
        }

        public Task SetLockoutEndDateAsync(AppUser user, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult(true);
        }
    }
}

