using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.DataHelpers;
using Data.DataHelpers.Map;
using Data.Database;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Repository.DotRepository
{
    public partial class DotRepository
    {
        public async Task<UserRegistrationModel> AddOne(DotFromViewModel dot, string imagePath, string path)
        {
            UserRegistrationModel reg = new UserRegistrationModel();
            reg.success = false;
            reg.message = 0;
            double[] coords = centerCoords(dot);
            string coordsKey = coords[0].ToString() + coords[1].ToString();
            User user = RavenDbRepository<User>.GetItemAsync(x => x.Token.Equals(dot.token));
            if (user.Id == null)
                return reg;

            List<Dot> dots =  RavenDbRepository<Dot>.GetItemsAsync(x => x != null).Where(x => (x.lon.ToString() + x.lat.ToString()).Equals(coordsKey)).ToList();

            for (int i = 0; i < dots.Count; i++)
            {                       
                if (user.UserName.Equals(dots[i].username))
                {
                    if (imagePath != null)
                        removeOld(imagePath, path);
                    reg.message = message2.AlreadyMarked;
                    return reg;
                }
                else if (checkTerritory(dots[i]))
                {
                    string name = dots[i].username;
                    User userChanged = RavenDbRepository<User>.GetItemAsync(x => x.UserName.Equals(name));
                    userChanged.dotsId.Remove(dots[i].Id);

                    //addEvent(userChanged, user, dots[i]);

                    removeOld(dots[i].photoPath, path);
                    dots[i] = changeDotValues(DateTime.Today, dot.message, coords[1], coords[0], user.UserName, cutImageName(imagePath), dots[i]);
                    if (user.dotsId == null)
                        user.dotsId = new List<string>();
                    user.dotsId.Add(dots[i].Id);
                     RavenDbRepository<Dot>.UpdateItemAsync(dots[i].Id, dots[i]);
                     RavenDbRepository<User>.UpdateItemAsync(userChanged.Id, userChanged);
                     RavenDbRepository<User>.UpdateItemAsync(user.Id, user);
                    reg.success = true;
                    reg.message = message2.Success;
                    return reg;
                }
            }

            if (dots.Count() == 0 && checkCenter(dot.lng, dot.lat))
            {
                //addEventNewDot(session, user, dot.lng, dot.lat);
                Dot dotCopy = new Dot();
                dotCopy = changeDotValues(DateTime.Today, dot.message, coords[1], coords[0], user.UserName, cutImageName(imagePath));
                dotCopy.Id = (RavenDbRepository<Dot>.CreateItemAsync(dotCopy)).Id;
                if (user.dotsId == null)
                    user.dotsId = new List<string>();
                user.dotsId.Add(dotCopy.Id);
                RavenDbRepository<User>.UpdateItemAsync(user.Id, user);

                reg.success = true;
                reg.message = message2.Success;
                return reg;
            }
            reg.message = message2.NotInTerritory;
            return reg;
        }       

        public async Task <List<Dot>> GetAll(CornersCorrds corners)
        {
            double squareX = corners.neX - corners.swX;
            double squareY = corners.neY - corners.swY;
            double neX = corners.neX + squareX / 3;
            double neY = corners.neY + squareY / 3;
            double swX = corners.swX - squareX / 3;
            double swY = corners.swY - squareY / 3;
            if (neX < swX)
                neX = 179;
            if (neX > 180 || neX < -180)
                neX = 179;
            if (swX > 180 || swX < -180)
                swX = -179;
            if (neY > 90 || neY < -90)
                neY = 89;
            if (swY > 90 || swY < -90)
                swY = -89;              
            List<Dot> dotsToSend = RavenDbRepository<Dot>.GetItemsAsync(x => x.lon < neX && x.lat < neY && swX < x.lon && swY < x.lat);
            return dotsToSend;
        }


        public async Task<List<Dot>> GetAllUserByName(CornersCorrds corners, string name)
        {
            List<Dot> reg = new List<Dot>();
            try
            {
                List<Dot> dotsToSend = RavenDbRepository<Dot>
                    .GetItemsAsync(x => x.username.Equals(name) && x.lon < corners.neX 
                            && x.lat < corners.neY && corners.swX < x.lon && corners.swY < x.lat);
                return reg;
            }
            catch
            {
                return reg;
            }
        }

        public async Task<Dot[]> GetAllDotsByName(string name)
        {
            List<Dot> dots;
            if (!string.IsNullOrEmpty(name))
            {
                 dots = RavenDbRepository<Dot>
                    .GetItemsAsync(x => x.username.Equals(name));
               
            }
            else {
                dots = RavenDbRepository<Dot>
                    .GetItemsAsync(x => x != null);
            }
            return dots.ToArray();
        }

        public async Task<CanMarkSpot> CheckDotResults(DotFromViewModel dot)
        {            
            CanMarkSpot reg = new CanMarkSpot();
            reg.CanMark = false;
            try
            {
                double[] coords = centerCoords(dot);
                string coordsKey = coords[0].ToString() + coords[1].ToString();
                User user = RavenDbRepository<User>.GetItemAsync(x => x.Token.Equals(dot.token));

                if (user.Id == null)
                    return reg;
                Dot dotCopy = new Dot();
                List<Dot> dots = (RavenDbRepository<Dot>.GetItemsAsync(x => x != null)).Where(x => (x.lon.ToString() + x.lat.ToString()).Equals(coordsKey)).ToList();
                if (dots.Count() > 0)
                {
                    if (user.UserName.Equals(dots[0].username))
                    {
                        return reg;
                    }
                    else
                    {
                        reg.Corners = coordsToSquare(dot.lat, dot.lng);
                        reg.Lat = dots[0].nextCapLat;
                        reg.Lon = dots[0].nextCapLon;
                        reg.SmallSquare = getSmallSquare(dots[0].nextCapLat, dots[0].nextCapLon);
                        reg.MarkedUsername = dots[0].username;
                        reg.CanMark = checkTerritory(dots[0]);
                        return reg;
                    }
                }
                reg.Corners = coordsToSquare(dot.lat, dot.lng);
                double[] holder = centreCapturePoint(dot.lat, dot.lng);
                reg.Lat = holder[1];
                reg.Lon = holder[0];
                reg.CanMark = checkCenter(dot.lat, dot.lng);
                reg.SmallSquare = getSmallSquare(dot.lat, dot.lng);
                return reg;
            }
            catch
            {
                return reg;
            }   
         }     

        public async Task<DotClick> GetDotById(string Id)
        {
            try
            {
                Dot myDot = RavenDbRepository<Dot>.GetItemAsyncByID(Id);
                User user = RavenDbRepository<User>.GetItemAsync(x => x.UserName.Equals(myDot.username));
                return new DotClick
                {
                    profilePic = user.profilePicture,
                    country = user.countryCode,
                    state = user.state,
                    points = user.points,
                    message = myDot.message,
                    date = myDot.date,
                    username = myDot.username,
                    photoPath = myDot.photoPath
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
