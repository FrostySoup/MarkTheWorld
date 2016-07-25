using Data.Database;
using Data.DataHelpers.Facebook;
using Data.DataHelpers.User.SendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public partial class UserRepository
    {
        public async Task<bool> CheckFbUser(string id)
        {
            try
            {
                User user = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.fbID.Equals(id));
                if (user != null)
                    return false;
                else
                    return true;
            }
            catch
            {
                return true;
            }
        }

        public async Task<FbNameToken> SaveNewToken(string id, string token)
        {
            try
            {
                User user = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.fbID.Equals(id));
                if (user != null)
                {
                    user.Token = token;
                    await DocumentDBRepository<User>.UpdateItemAsync(user.Id, user);
                    string profilePath = user.profilePicture;
                    if (!profilePath.Contains("facebook"))
                        profilePath = "/Content/img/avatars/" + profilePath;
                    return new FbNameToken
                    {
                        countryCode = user.countryCode,
                        photoPath = profilePath,
                        token = user.Token,
                        username = user.UserName
                    };
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> CheckNameUnique(string usName)
        {
            try
            {
                User user = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.UserName.Equals(usName));
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

        public async Task<Registration> RegisterFbUser(FbRegisterClient fb, string photo)
        {

                User user = await DocumentDBRepository<User>
                                .GetItemAsync(x => x.fbID.Equals(fb.userID));   
                if (user != null)               
                    return null;
                User newUser = generateUser();
                newUser.fbID = fb.userID;
                newUser.countryCode = fb.countryCode;
                newUser.UserName = fb.userName;
                newUser.Token = fb.token;
                newUser.profilePicture = photo;
                if (fb.state != null)
                    newUser.state = fb.state;
                await DocumentDBRepository<User>
                    .CreateItemAsync(newUser);
                return new Registration
                {
                    token = fb.token,
                    photo = photo
                };
        }

    }
}
