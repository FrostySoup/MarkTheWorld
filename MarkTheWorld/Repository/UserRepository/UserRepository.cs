using Data;
using Data.ReceivePostData;
using Raven.Client;
using Repository.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataHelpers;

namespace Repository.UserRepository
{
    public class UserRepository : GenericRepository.GenericRepository, IUserRepository
    {
        public UserRegistrationModel AddUser(UserRegistrationPost userPost)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                User newUser = new User();
                newUser.PasswordHash = userPost.PasswordHash;
                newUser.UserName = userPost.UserName;
                newUser.countryCode = userPost.CountryCode;
                newUser.lastDailyTime = DateTime.Now;
                Random rnd = new Random();
                newUser.colors = new Color();
                newUser.colors.blue = rnd.Next(1, 255);
                newUser.colors.red = rnd.Next(1, 255);
                newUser.colors.green = rnd.Next(1, 255);
                try
                {                    
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = false;
                    check.message = message2.UserNameTaken;
                    check.Token = System.Guid.NewGuid();
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(newUser.UserName));
                    if (oneObject == null)
                    {
                        newUser.Token = check.Token;
                        session.Store(newUser);
                        session.SaveChanges();
                        check.success = true;
                        check.message = message2.Success;
                        return check;
                    }
                    check.success = false;
                    check.message = message2.UserNameTaken;
                    return check;
                }
                catch
                {
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = true;
                    check.Token = System.Guid.NewGuid();
                    newUser.Token = check.Token;
                    session.Store(newUser);
                    session.SaveChanges();
                    check.message = message2.Success;
                    return check;
                }
            }
        }

        public bool SetColors(string userName, Color colors)
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

        public bool GetUsername(string userName)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>()
                        .First(x => x.UserName.Equals(userName));
                    if (user != null)
                        return false;
                    else return true;
                }
                catch
                {
                    return true;
                }
            }
        }

        public Color GetColors(string userName)
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

        public List<UserEvent> GetUserEvents(string userName)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>().First(x => x.UserName.Equals(userName));
                    return user.eventsHistory;
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
                return timePassed;
            }
        }

        public List<TopUser> GetTopUsers()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                List<TopUser> users = new List<TopUser>();
                try
                {
                    User[] oneObject = session
                         .Query<UsersByMostDots.Result, UsersByMostDots>()
                         .OrderByDescending(x => x.numberOfDots)
                         .Take(10)
                         .As<User>()
                         .ToArray();
                    for (int i = 0; i < oneObject.Length; i++)
                    {
                        TopUser user = new TopUser();
                        user.name = oneObject[i].UserName;
                        if (oneObject[i].dotsId == null)
                            user.numberOfMarks = 0;
                        else
                            user.numberOfMarks = oneObject[i].dotsId.Count;
                        users.Add(user);
                    }
                }
                catch
                {
                    return users;
                }
                return users;
            }
        }

        public UserRegistrationModel GetOneUser(UserRegistrationPost user)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = false;
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(user.UserName));
                    if (oneObject == null)
                    {
                        check.success = false;
                        check.message = message2.NoUserName;
                        return check;
                    }

                    if (!oneObject.PasswordHash.Equals(user.PasswordHash))
                    {
                        check.success = false;
                        check.message = message2.PassMissmatch;
                        return check;
                    }

                    if (oneObject != null)
                    {
                        check.success = true;
                        check.message = message2.Success;
                        check.Token = oneObject.Token;
                        return check;
                    }
                    return check;
                }
                catch
                {
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = false;
                    check.message = message2.NoUserName;
                    return check;
                }
            }
        }

        public User GetOneByName(string name)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(name));
                    return oneObject;
                }
                catch
                {
                    return null;
                }
            }
        }

        public User GetOneByToken(Guid token)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User oneObject = session.Query<User>().First(x => x.Token.Equals(token));
                    return oneObject;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Guid GetTokenByName(string name)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(name));
                    return oneObject.Token;
                }
                catch
                {
                    return new Guid();
                }
            }
        }
    }
}