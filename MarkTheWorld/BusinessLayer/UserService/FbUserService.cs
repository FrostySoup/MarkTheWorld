﻿using Data;
using Data.Database;
using Data.DataHelpers;
using Data.DataHelpers.Facebook;
using Data.DataHelpers.User.SendData;
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

        public async Task<FbServerLogin> getUserParams(FbClientLogin fb)
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
            usName = await repository.CheckNameUnique(usName);
            return new FbServerLogin
            {
                country = country,
                username = usName
            };
        }

        public async Task<string> getUserParamesDb(string token)
        {        
            return (await repository.GetOneByToken(token)).UserName;
        }

        public async Task<Registration> register(FbRegisterClient fb)
        {
            string photo = "https://graph.facebook.com/"+ fb.userID + "/picture?width=100&height=100";
            FbNameToken token = await getLongLiveToken(new FbClientLogin
            {
                Token = fb.token,
                Id = fb.userID
            });
            if (token.token == null)
                return null;
            fb.token = token.token;
            return await repository.RegisterFbUser(fb, photo);
        }

        private string getPhoto(string token)
        {
            Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=picture&access_token=" + token);
            HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

            // Read the returned JSON object response
            try {
                StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
                string jsonResponse = string.Empty;
                jsonResponse = userInfo.ReadToEnd();

                // Deserialize and convert the JSON object to the Facebook.User object type
                JavaScriptSerializer sr = new JavaScriptSerializer();
                string jsondata = jsonResponse;
                Facebook.User converted = sr.Deserialize<Facebook.User>(jsondata);

                if (converted.picture.data.url != null)
                {
                    converted.picture.data.url = converted.picture.data.url.Replace("p50x50", "p100x100");
                    return converted.picture.data.url;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<FbNameToken> getLongLiveToken(FbClientLogin fb)
        {
            string url = string.Format("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id={0}&client_secret={1}&fb_exchange_token={2}",
                "211779402536909", "25bd7558cf8e380a176243ef5a96128e", fb.Token);
            Uri targetUserUri = new Uri(url);
            try { 
                HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

                // Read the returned JSON object response
                StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
                string jsonResponse = string.Empty;
                jsonResponse = userInfo.ReadToEnd();

                string strStart = "=";
                string strEnd = "&expires";
                string token = getBetween(jsonResponse, strStart, strEnd);

                FbNameToken newToken = await repository.SaveNewToken(fb.Id, token);

                if (newToken == null)
                {
                    newToken = new FbNameToken();
                    newToken.token = token;
                }
                return newToken;
            }
            catch
            {
                return null;
            }
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

        public async Task<bool> checkUserById(string id)
        {
            return await repository.CheckFbUser(id);
        }
    }
}
