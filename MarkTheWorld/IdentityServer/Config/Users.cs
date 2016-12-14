using BusinessLayer.UserService;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace IdentityServer
{
    public static class Users
    {
        private static UserService userService = new UserService();
        public static List<InMemoryUser> Get()
        {
            return userService.GetUsersForIdentityServer();           
        }
    }
}