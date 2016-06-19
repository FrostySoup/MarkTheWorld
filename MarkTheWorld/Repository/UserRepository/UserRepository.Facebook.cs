using Data;
using Data.DataHelpers.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public partial class UserRepository : GenericRepository.GenericRepository, IUserRepository
    {
        public bool CheckFbUser(string id, string token)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>().First(x => x.fbID.Equals(id));
                    user.Token = token;
                    session.Store(user);
                    session.SaveChanges();
                    return false;
                }
                catch
                {
                    return true;
                }
            }
        }

        public string CheckNameUnique(string usName)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>()
                        .First(x => x.UserName.Equals(usName));
                    if (user != null)
                    {
                        Random rnd = new Random();
                        int randomNum = rnd.Next(0, 999);
                        return usName + randomNum;
                    }
                    else return usName;
                }
                catch
                {
                    return usName;
                }
            }
        }

        public string RegisterFbUser(FbRegisterClient fb, string photo)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>().First(x => x.fbID.Equals(fb.userID));                    
                    return null;
                }
                catch
                {
                    User newUser = generateUser();
                    newUser.fbID = fb.userID;
                    newUser.countryCode = fb.countryCode;
                    newUser.UserName = fb.userName;
                    newUser.Token = fb.token;
                    newUser.profilePicture = photo;
                    session.Store(newUser);
                    session.SaveChanges();
                    return fb.token;
                }
            }
        }

    }
}
