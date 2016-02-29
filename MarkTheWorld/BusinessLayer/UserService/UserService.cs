using Data;
using Data.DataHelpers;
using Data.ReceivePostData;
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

        public UserRegistrationModel addUser(UserRegistrationPost user)
        {
            return repository.AddUser(user);
        }

        public UserRegistrationModel getOne(UserRegistrationPost user)
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

        public User getOneByName(string name)
        {
            return repository.GetOneByName(name);
        }

        public List<TopUser> getTopUsers()
        {
            return repository.GetTopUsers();
        }

        public bool checkUserDaily(string userName)
        {
            return repository.GetUserDaily(userName);
        }

        public List<UserEvent> getUserEvents(string userName)
        {
            return repository.GetUserEvents(userName);
        }

        public bool renewDailies()
        {
            return repository.SetDailies();
        }

        public bool postUserColors(string userName, Colors colors)
        {
            return repository.SetColors(userName, colors);
        }

        public Colors getUserColors(string userName)
        {
            return repository.GetColors(userName);
        }

        public bool checkUsername(string userName)
        {
            return repository.GetUsername(userName);
        }
    }
}
