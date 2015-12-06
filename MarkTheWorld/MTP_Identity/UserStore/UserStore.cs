using Data;
using Microsoft.AspNet.Identity;
using Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTP_Identity.UserStore
{
    public partial class UserStore : IUserStore<AppUser>
    {
        private readonly Repository.UserRepository.UserRepository repository;

        public UserStore(UserRepository repos)
        {
            this.repository = repos;
        }

        public Task CreateAsync(AppUser user)
        {
            repository.Add(user);

            return Task.FromResult(true);
        }

        public Task DeleteAsync(AppUser user)
        {
            repository.Delete<AppUser>(user.Id);
            return Task.FromResult(true);
        }

        public Task<AppUser> FindByIdAsync(string userId)
        {
            var user = repository.GetOne<AppUser>(userId);
            return Task.FromResult(user);
        }

        public Task<AppUser> FindByNameAsync(string userName)
        {
            var user = repository.GetOneByName(userName);
            return Task.FromResult(user);
        }

        public Task UpdateAsync(AppUser user)
        {
            repository.Edit(user);
            return Task.FromResult(true);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}