﻿using Data;
using Data.DataHelpers;
using Data.ReceivePostData;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DotService;

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
            return false;// repository.GetUserDaily(userName);
        }

        public List<UserEvent> getUserEvents(string userName)
        {
            return repository.GetUserEvents(userName);
        }

        public bool postUserColors(string userName, Color colors)
        {
            return repository.SetColors(userName, colors);
        }

        public Color getUserColors(string userName)
        {
            return repository.GetColors(userName);
        }

        public bool checkUsername(string userName)
        {
            return repository.GetUsername(userName);
        }

        public UserProfile GetProfile(string userName)
        {
            UserProfile user = new UserProfile();
            user.colors = repository.GetColors(userName);
            user.name = userName;
            user.points = repository.GetTotalPoints(userName);
            //Not implemented
            user.pictureAdress = "profpicTest.png";
            //user.pictureAdress = HttpContext.Current.Server.MapPath("~/App_Data/ProfilePictures/profpicTest.png");

            DotServices dotService = new DotServices();
            user.dailies = new DailyReward();
            int points = 0;
            Dot[] dots = dotService.getAlluserDots(userName);
            points = dotService.getUserPointsName(dots);
            user.dailies.points = points;
            Country country = repository.GetCountry(userName);
            user.countryName = country.name;
            user.flagAdress = country.code + ".png";

            TimeSpan time = repository.GetUserDaily(userName);
            if (time.TotalDays >= 1)
                user.dailies.timeLeft = new TimeSpan(0, 0, 0);
            else {
                TimeSpan newSpan = new TimeSpan(24, 0, 0);
                user.dailies.timeLeft = newSpan - time;
            }

            return user;
        }
    }
}
