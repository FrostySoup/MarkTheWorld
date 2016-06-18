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
    public interface IUserService
    {
        List<User> getApplicationUserApplicationUsers(string ApplicationUserID);
        UserRegistrationModel addUser(UserRegistrationPost ApplicationUser);
        UserRegistrationModel getOne(UserRegistrationPost user);
        User deleteOne(string id);   //delete one
        User editApplicationUser(User ApplicationUser);
        User getOneByToken(string token);
        List<TopUser> getTopUsers();
        UserDailyReward takeUserDaily(string userName);
        List<UserEvent> getUserEvents(string userName);
        bool postUserColors(string userName, Color colors);
        Color getUserColors(string userName);
        bool checkUsername(string userName);
        UserProfile GetProfile(string userName);
    }
}
