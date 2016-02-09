﻿using Data;
using Data.ReceivePostData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public interface IUserRepository
    {
        User GetOneByName(string name);
        User GetOneByToken(Guid token);
        UserRegistrationModel AddUser(UserRegistrationPost user);
        Guid GetTokenByName(string name);
        UserRegistrationModel GetOneUser(UserRegistrationPost user);
        List<TopUser> GetTopUsers();
        bool GetUserDaily(string userName);
        List<UserEvent> GetUserEvents(string userName);
        bool SetDailies();
    }
}
