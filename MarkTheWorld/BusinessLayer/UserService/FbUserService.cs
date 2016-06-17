using Data.Database;
using Data.DataHelpers;
using Data.DataHelpers.Facebook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BusinessLayer.UserService
{
    public class FbUserService
    {
        private Repository.UserRepository.UserRepository repository = new Repository.UserRepository.UserRepository();

        public FbServerLogin checkAuth(FbClientLogin fb)
        {
            Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,locale&access_token=" + fb.Token);
            HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

            // Read the returned JSON object response
            StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
            string jsonResponse = string.Empty;
            jsonResponse = userInfo.ReadToEnd();

            // Deserialize and convert the JSON object to the Facebook.User object type
            JavaScriptSerializer sr = new JavaScriptSerializer();
            string jsondata = jsonResponse;
            Facebook.User converted = sr.Deserialize<Facebook.User>(jsondata);

            //Country
            Country country = new Country();
            string countryCode = converted.locale;
            if (countryCode.Length > 4)
            {
                countryCode = countryCode.Substring(3, 2);
                country = CountriesList.getCountry(countryCode);
            }
            //Username
            string usName = "";
            usName = converted.first_name + converted.last_name;
            if (usName.Length > 22)
                usName.Substring(0, 22);
            usName = repository.CheckNameUnique(usName);
            return new FbServerLogin
            {
                country = country,
                username = usName
            };
        }

        public string register(FbRegisterClient fb)
        {
            return repository.RegisterFbUser(fb);
        }

        public FbServerLogin getLongLiveToken(FbClientLogin fb)
        {

            FbServerLogin fbLoginParam = checkAuth(fb);
            string url = string.Format("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id={0}&client_secret={1}&fb_exchange_token={2}",
                "211779402536909", "25bd7558cf8e380a176243ef5a96128e", fb.Token);
            Uri targetUserUri = new Uri(url);
            HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

            // Read the returned JSON object response
            StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
            string jsonResponse = string.Empty;
            jsonResponse = userInfo.ReadToEnd();

            string strStart = "=";
            string strEnd = "&expires";
            string token = getBetween(jsonResponse, strStart, strEnd);

            fbLoginParam.longToken = token;

            fbLoginParam.newUser = checkUserById(fb.Id, fb.Token);

            return fbLoginParam;
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public bool checkUserById(string id, string token)
        {
            return repository.CheckFbUser(id, token);
        }
    }
}
