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
    public partial class UserRepository : GenericRepository.GenericRepository, IUserRepository
    {
        public UserRegistrationModel AddUser(UserRegistrationPost userPost)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                User newUser = generateUser();
                newUser.PasswordHash = userPost.PasswordHash;
                newUser.UserName = userPost.UserName;
                if (userPost == null)
                    newUser.state = userPost.State;
                newUser.countryCode = userPost.CountryCode;         
                Random rnd = new Random();
                newUser.profilePicture = "defaultAvatar" + rnd.Next(1, 16) + ".png";
                UserRegistrationModel check = new UserRegistrationModel();
                check.photo = "/Content/img/avatars/" + newUser.profilePicture;             
                check.username = userPost.UserName;
                try
                {                    
                    
                    check.success = false;
                    check.message = message2.UserNameTaken;
                    check.Token = System.Guid.NewGuid().ToString();
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(newUser.UserName));
                    if (oneObject == null)
                    {
                        newUser.Token = check.Token.ToString();
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
                    check.success = true;
                    check.Token = System.Guid.NewGuid().ToString();
                    newUser.Token = check.Token.ToString();
                    session.Store(newUser);
                    session.SaveChanges();
                    check.message = message2.Success;
                    return check;
                }
            }
        }

        public Colors GetColorsById(string dotId)
        {
            throw new NotImplementedException();
        }

        public User generateUser()
        {
            User newUser = new User();
            newUser.lastDailyTime = DateTime.Now;
            Random rnd = new Random();
            newUser.colors = new Colors();
            newUser.colors.blue = rnd.Next(1, 255);
            newUser.colors.red = rnd.Next(1, 255);
            newUser.colors.green = rnd.Next(1, 255);
            return newUser;
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
                        check.photo = "/Content/img/avatars/" + oneObject.profilePicture;
                        check.username = oneObject.UserName;
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

        public User GetOneByToken(string token)
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

        public string GetTokenByName(string name)
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
                    return new Guid().ToString();
                }
            }
        }

    }
}