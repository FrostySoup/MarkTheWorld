using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.UserService
{
    public interface IUserService
    {
        List<User> getApplicationUserApplicationUsers(string ApplicationUserID);
        UserRegistrationModel addUser(User ApplicationUser);
        UserRegistrationModel getOne(User user);
        User deleteOne(string id);   //delete one
        User editApplicationUser(User ApplicationUser);
        User getOneByToken(Guid token);
    }
}
