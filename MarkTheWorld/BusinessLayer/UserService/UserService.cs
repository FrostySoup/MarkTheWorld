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

        public User getOneByToken(string token)
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

        public UserDailyReward takeUserDaily(string userName)
        {
            DotServices dotService = new DotServices();
            Dot[] dots = dotService.getAlluserDots(userName);
            int points = dotService.getUserPointsName(dots);
            UserDailyReward tookDaily = repository.GetUserDailyReward(userName, points);
            return tookDaily;
        }

        public UserProfile GetProfile(string userName)
        {
            UserProfile user = new UserProfile();
            user.colors = repository.GetColors(userName);
            user.name = userName;
            user.points = repository.GetTotalPoints(userName);
            user.pictureAddress = repository.GetProfilePic(userName);
            if (!user.pictureAddress.Contains("facebook"))
                user.pictureAddress = "/Content/img/avatars/" + user.pictureAddress;
            DotServices dotService = new DotServices();
            user.dailies = new DailyReward();
            int points = 0;
            Dot[] dots = dotService.getAlluserDots(userName);
            points = dotService.getUserPointsName(dots);
            user.dailies.points = points;
            Country country = repository.GetCountry(userName);
            user.countryName = country.name;
            user.flagAddress = country.code.ToLower() + ".png";

            TimeSpan time = repository.GetUserDaily(userName);
            TimeSpan saveTime = new TimeSpan(0, 0, 0);
            if (time.TotalDays >= 1)
                user.dailies.timeLeft = 0;
            else {
                TimeSpan newSpan = new TimeSpan(24, 0, 0);
                user.dailies.timeLeft = (int)(newSpan - time).TotalSeconds + 1;
            }

            return user;
        }
    }
}
