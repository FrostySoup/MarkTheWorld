using Data;
using Data.DataHelpers;
using Data.ReceivePostData;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DotService;
using Data.DataHelpers.User;
using Data.Database;
using IdentityServer3.Core.Services.InMemory;
using System.Security.Claims;
using IdentityServer3.Core;

namespace BusinessLayer.UserService
{
    public class UserService
    {
        private Repository.UserRepository.UserRepository repository = new Repository.UserRepository.UserRepository();

        public List<InMemoryUser> GetUsersForIdentityServer()
        {
            List<User> users = repository.GetAll();
            List<InMemoryUser> identityUsers = new List<InMemoryUser>();

            int i = 1;
            foreach(User user in users)
            {
                identityUsers.Add(new InMemoryUser
                {
                    Username = user.UserName,
                    Password = user.PasswordHash,
                    Subject = i.ToString()
                });
                i++;
            }

            return identityUsers;
        }

        public UserService()
        {
            repository = new Repository.UserRepository.UserRepository();
        }

        public async Task<UserRegistrationModel> addUser(UserRegistrationPost user)
        {
            return await repository.AddUser(user);
        }

        public async Task<UserRegistrationModel> getOne(UserRegistrationPost user)
        {
            return await repository.GetOneUser(user);
        }

       /* public async Task<User> editApplicationUser(User ApplicationUser)
        {
            return await repository.Edit(ApplicationUser);
        }*/

        public async Task<User> getOneByToken(string token)
        {
            return await repository.GetOneByToken(token);
        }

        public async Task<User> getOneByName(string name)
        {
            return await repository.GetOneByName(name);
        }

        public async Task<List<TopUser>> getTopUsers(string countryCode, int number, int startingNumber)
        {
            return await repository.GetTopUsers(countryCode, number, startingNumber);
        }

        public bool checkUserDaily(string userName)
        {
            return false;// repository.GetUserDaily(userName);
        }

        /*public List<UserEvent> getUserEvents(string userName)
        {
            return repository.GetUserEvents(userName);
        }*/

        public async Task<bool> postUserColors(string userName, Colors colors)
        {
            return await repository.SetColors(userName, colors);
        }

        public async Task<Colors> getUserColors(string userName)
        {
            return await repository.GetColors(userName);
        }

        public async Task<bool> editApplicationUser(User user)
        {
            return await repository.EditUser(user);
        }

        public async Task<bool> checkUsername(string userName)
        {
            return await repository.GetUsername(userName);
        }

        public async Task<UserDailyReward> takeUserDaily(string userName)
        {
            DotServices dotService = new DotServices();
            Dot[] dots = await dotService.getAlluserDots(userName);
            int points = dotService.getUserPointsName(dots);
            return await repository.GetUserDailyReward(userName, points);
        }

        public async Task<UserProfile> GetProfile(string userName)
        {
            UserProfile user = new UserProfile();
            user.colors = await repository.GetColors(userName);
            user.name = userName;
            user.points = await repository.GetTotalPoints(userName);
            user.pictureAddress = await repository.GetProfilePic(userName);
            if (!user.pictureAddress.Contains("facebook"))
                user.pictureAddress = "/Content/img/avatars/" + user.pictureAddress;
            DotServices dotService = new DotServices();
            user.dailies = new DailyReward();
            int points = 0;
            Dot[] dots = await dotService.getAlluserDots(userName);
            points = dotService.getUserPointsName(dots);
            user.dailies.points = points;
            Country country = await repository.GetCountry(userName);
            user.countryName = country.name;
            user.flagAddress = country.code.ToLower() + ".png";

            TimeSpan time = await repository.GetUserDaily(userName);
            TimeSpan saveTime = new TimeSpan(0, 0, 0);
            if (time.TotalDays >= 1)
                user.dailies.timeLeft = 0;
            else {
                TimeSpan newSpan = new TimeSpan(24, 0, 0);
                user.dailies.timeLeft = (int)(newSpan - time).TotalSeconds + 1;
            }

            return user;
        }

        public List<string> GetUsersAutoComplete(string filter, int number)
        {
            List<string> usernames = (repository.GetAll()).Select(x => x.UserName).ToList();
            usernames = filterUsernames(filter, number, usernames);
            return usernames;
        }

        private List<string> filterUsernames(string filter, int number, List<string> usernames)
        {
            List<StringFiltering> filterer = new List<StringFiltering>();
            foreach (string name in usernames)
            {
                filterer.Add(new StringFiltering(name, filter));
            }
            List<StringFiltering> sorted = filterer.OrderBy(x => x.value).ToList();

            usernames = new List<string>();
            int i = 0;
            while (i < sorted.Count && i < number && sorted[i].value != 100)
            {
                usernames.Add(sorted[i].username);
                i++;
            }
            return usernames;
        }
    }
}
