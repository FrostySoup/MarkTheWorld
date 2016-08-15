using Data.DataHelpers;
using Data.ReceivePostData;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.Database;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public partial class UserRepository
    {
        public async Task<UserDailyReward> GetUserDailyReward(string userName, int points)
        {
            TimeSpan timePassed = new TimeSpan(0, 0, 0);
            UserDailyReward dailies = new UserDailyReward();
            try
            {
                User user = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.UserName.Equals(userName));
                timePassed = DateTime.Now - user.lastDailyTime;
                if (timePassed.TotalDays >= 1)
                {
                    user.lastDailyTime = DateTime.Now;
                    user.points += points;
                    dailies.totalPoints = user.points;
                    dailies.received = points;
                    TimeSpan day = new TimeSpan(24, 0, 0);
                    dailies.timeLeft = (int)day.TotalSeconds;
                    await DocumentDBRepository<User>.UpdateItemAsync(user.Id, user);
                    return dailies;
                }
                else return dailies;
            }
            catch
            {
                return dailies;
            }
        }
        

        public async Task<string> GetProfilePic(string userName)
        {
            try
            {
                User user = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.UserName.Equals(userName));
                if (user.profilePicture == null)
                    return "defaultAvatar1.png";
                else
                    return user.profilePicture;
            }
            catch
            {
                return "defaultAvatar1.png";
            }
        }

        
        public async Task<bool> SetColors(string userName, Colors colors)
        {
            try
            {
                User user = await DocumentDBRepository<User>
                    .GetItemAsync(x => x.UserName.Equals(userName));

                user.colors = colors;
                await DocumentDBRepository<User>
                    .UpdateItemAsync(user.Id, user);
                return true;
            }
            catch
            {
                return false;
            }
        }       

        public async Task<int> GetTotalPoints(string userName)
        {
            try
            {
                User user = await DocumentDBRepository<User>
                    .GetItemAsync(x => x.UserName.Equals(userName));
                if (user == null)
                    return -1;
                if (user.points < 0)
                    return 0;
                else return user.points;
            }
            catch
            {
                return -1;
            }
        }

        public async Task<Country> GetCountry(string userName)
        {
            try
            {
                User user = await DocumentDBRepository<User>
                    .GetItemAsync(x => x.UserName.Equals(userName));
                if (user != null)
                    return CountriesList.getCountry(user.countryCode);
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TimeSpan> GetUserDaily(string userName)
        {
            TimeSpan timePassed = new TimeSpan(0, 0, 0);
            try
            {
                User user = await DocumentDBRepository<User>
                    .GetItemAsync(x => x.UserName.Equals(userName));
                timePassed = DateTime.Now - user.lastDailyTime;
                return timePassed;
            }
            catch
            {
                return timePassed;
            }
        }

        public async Task<Colors> GetColors(string userName)
        {
            try
            {
                User user = await DocumentDBRepository<User>
                    .GetItemAsync(x => x.UserName.Equals(userName));
                if (user != null)
                    return user.colors;
                else return null;
            }
            catch
            {
                return null;
            }
        }

    }
}
