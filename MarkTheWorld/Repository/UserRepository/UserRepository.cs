using Data;
using Data.ReceivePostData;
using Raven.Client;
using Repository.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                newUser.UserName = userPost.PasswordHash;
                try
                {                    
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = false;
                    check.message = "Unknown error";
                    check.Token = System.Guid.NewGuid();
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(newUser.UserName));
                    if (oneObject == null)
                    {
                        newUser.Token = check.Token;
                        session.Store(newUser);
                        session.SaveChanges();
                        check.success = true;
                        check.message = "User added";
                        return check;
                    }
                    check.success = false;
                    check.message = "Username already taken";
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
                    check.message = "User added";
                    return check;
                }
            }
        }

        public bool SetDailies()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    List<User> users = session.Query<User>()
                        .Take(5000)
                        .ToList();
                    foreach(User user in users)
                    {
                        user.pointsAvailable = true;
                        session.Store(user);
                    }
                    session.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
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

        public bool GetUserDaily(string userName)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>().First(x => x.UserName.Equals(userName));
                    if (user.pointsAvailable != false)
                    {
                        user.pointsAvailable = false;
                        session.Store(user);
                        session.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
                return false;
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
                        check.message = "Wrong username";
                        return check;
                    }

                    if (!oneObject.PasswordHash.Equals(user.PasswordHash))
                    {
                        check.success = false;
                        check.message = "Wrong password";
                        return check;
                    }

                    if (oneObject != null)
                    {
                        check.success = true;
                        check.message = "User found";
                        check.Token = oneObject.Token;
                        return check;
                    }
                    return check;
                }
                catch
                {
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = false;
                    check.message = "Wrong username";
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