using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.UserService
{
    public class UserService : IUserService
    {
        private Repository.UserRepository.UserRepository repository = new Repository.UserRepository.UserRepository();

        public UserService()
        {
            repository = new Repository.UserRepository.UserRepository();
        }

        public List<User> getApplicationUserApplicationUsers(string ids)
        {
            return repository.GetAll<User>();
        }

        public UserRegistrationModel addUser(User user)
        {
            return repository.AddUser(user);
        }


        public UserRegistrationModel getOne(User user)
        {
            return repository.GetOneUser(user);
        }

        public User deleteOne(string id)
        {
            return repository.Delete<User>(id);
        }

        public User editApplicationUser(User ApplicationUser)
        {
            return repository.Edit(ApplicationUser);
        }

        public User getOneByToken(Guid token)
        {
            return repository.GetOneByToken(token);
        }

        public List<TopUser> getTopUsers()
        {
            return repository.GetTopUsers();
        }
    }
}
