using Data;
using Data.ReceivePostData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataHelpers;
using Data.Database;

namespace Repository.UserRepository
{
    public partial class UserRepository
    {
        public async Task<UserRegistrationModel> AddUser(UserRegistrationPost userPost)
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
                User oneObject = await DocumentDBRepository<User>.GetItemAsync(x => x.UserName.Equals(newUser.UserName));
                if (oneObject == null)
                {
                    newUser.Token = check.Token.ToString();
                    await DocumentDBRepository<User>.CreateItemAsync(newUser);
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
                await DocumentDBRepository<User>.CreateItemAsync(newUser);
                check.message = message2.Success;
                return check;
            }
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

        public async Task<bool> GetUsername(string userName)
        {
            try
            {
                User user = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.UserName.Equals(userName));
                if (user != null)
                    return false;
                else return true;
            }
            catch
            {
                return true;
            }
        }

     

        /*public List<UserEvent> GetUserEvents(string userName)
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
        }*/

        public async Task<List<TopUser>> GetTopUsers(string countryCode, int number, int startingPage)
        {
            List<TopUser> users = new List<TopUser>();
            try
            {
                if (string.IsNullOrEmpty(countryCode))
                {
                    users = (await DocumentDBRepository<User>.GetItemAsyncPages(x => x != null, x => x.points, number, startingPage * number)).Select(x => new TopUser
                    {
                        photoPath = x.profilePicture,
                        points = x.points,
                        username = x.UserName
                    }).ToList();
                }
                else
                {
                    users = (await DocumentDBRepository<User>.GetItemAsyncPages(x => x.countryCode.Equals(countryCode), x => x.points, number, startingPage * number)).Select(x => new TopUser
                    {
                        photoPath = x.profilePicture,
                        points = x.points,
                        username = x.UserName
                    }).ToList();                   
                }                 
            }
            catch
            {
                return users;
            }
            foreach (TopUser user in users)
            {
                if (!user.photoPath.Contains("facebook"))
                    user.photoPath = "/Content/img/avatars/" + user.photoPath;
            }
            if (users.Count > 0)
                TopUser.lowestNumber = startingPage * number;
            return users;
        }

        public async Task<List<User>> GetAll()
        {
            return await DocumentDBRepository<User>
                .GetItemsAsync(x => x != null);
                                
        }

        public async Task<UserRegistrationModel> GetOneUser(UserRegistrationPost user)
        {
            try
            {
                UserRegistrationModel check = new UserRegistrationModel();
                check.success = false;
                User oneObject = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.UserName.Equals(user.UserName));
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
                    check.countryCode = oneObject.countryCode;
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

        public async Task<User> GetOneByName(string name)
        {
            try
            {
                User oneObject = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.UserName.Equals(name));
                return oneObject;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> GetOneByToken(string token)
        {
            try
            {
                User oneObject = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.Token.Equals(token));
                return oneObject;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> EditUser(User user)
        {
            try
            {
                await DocumentDBRepository<User>
                                .UpdateItemAsync(user.Id, user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetTokenByName(string name)
        {
            try
            {
                User oneObject = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.UserName.Equals(name));
                return oneObject.Token;
            }
            catch
            {
                return new Guid().ToString();
            }
        }

    }
}