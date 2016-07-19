using Data;
using Data.DataHelpers;
using Data.ReceivePostData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataHelpers.Facebook;

namespace Repository.UserRepository
{
    public partial class UserRepository
    {
        public UserDailyReward GetUserDailyReward(string userName, int points)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                TimeSpan timePassed = new TimeSpan(0, 0, 0);
                UserDailyReward dailies = new UserDailyReward();
                try
                {
                    User user = session.Query<User>().First(x => x.UserName.Equals(userName));
                    timePassed = DateTime.Now - user.lastDailyTime;
                    if (timePassed.TotalDays >= 1)
                    {
                        user.lastDailyTime = DateTime.Now;
                        user.points += points;
                        dailies.totalPoints = user.points;
                        dailies.received = points;
                        TimeSpan day = new TimeSpan(24, 0, 0);
                        dailies.timeLeft = (int)day.TotalSeconds;
                        session.Store(user);
                        session.SaveChanges();
                        return dailies;
                    }
                    else return dailies;
                }
                catch
                {
                    return dailies;
                }
            }
        }
        

        public string GetProfilePic(string userName)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>()
                        .First(x => x.UserName.Equals(userName));
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
        }

        
        public bool SetColors(string userName, Colors colors)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>()
                        .First(x => x.UserName.Equals(userName));

                    user.colors = colors;
                    session.Store(user);

                    session.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public int GetTotalPoints(string userName)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>()
                        .First(x => x.UserName.Equals(userName));
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
        }

        public Country GetCountry(string userName)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>()
                        .First(x => x.UserName.Equals(userName));
                    if (user != null)
                        return CountriesList.getCountry(user.countryCode);
                    else return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        public TimeSpan GetUserDaily(string userName)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                TimeSpan timePassed = new TimeSpan(0, 0, 0);
                try
                {
                    User user = session.Query<User>().First(x => x.UserName.Equals(userName));
                    timePassed = DateTime.Now - user.lastDailyTime;
                    return timePassed;
                }
                catch
                {
                    return timePassed;
                }
            }
        }

        public Colors GetColors(string userName)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>()
                        .First(x => x.UserName.Equals(userName));
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
}
