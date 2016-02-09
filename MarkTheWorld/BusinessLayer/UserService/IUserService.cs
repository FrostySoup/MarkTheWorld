﻿using Data;
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
        User getOneByToken(Guid token);
        List<TopUser> getTopUsers();
        bool checkUserDaily(string userName);
        List<UserEvent> getUserEvents(string userName);
        bool renewDailies();
    }
}
