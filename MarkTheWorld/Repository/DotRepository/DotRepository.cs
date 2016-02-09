using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.DataHelpers;
using Raven.Client;
using Repository.Index;
using Repository.DataForIndex;
using System.Collections;
using Raven.Abstractions.Data;
using Raven.Client.Document;

namespace Repository.DotRepository
{
    public class DotRepository : GenericRepository.GenericRepository, IDotRepository
    {
        public UserRegistrationModel AddGroup(List<DotFromViewModel> dot)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                UserRegistrationModel reg = new UserRegistrationModel();
                foreach(DotFromViewModel d in dot)
                {
                    session.Store(d);
                    session.SaveChanges();
                }
                return reg;
            }
        }


        public UserRegistrationModel AddOne(DotFromViewModel dot)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                UserRegistrationModel reg = new UserRegistrationModel();
                reg.success = false;
                reg.message = 0;
                try
                {                 
                    User user = session.Query<User>().First(x => x.Token.Equals(dot.username));
                    if (user.Id == null)
                        return reg;
                    Dot dotCopy = new Dot();
                    Dot[] dots = session
                        .Query<Dot>()
                        .Take(5000)
                        .ToArray();
                    for (int i = 0; i < dots.Length; i++)
                    {
                        int lng = (int)(dots[i].lon * 100);
                        int lng2 = (int)(dot.lng * 100);
                        if (lng == lng2)
                        {
                            int lat = (int)(dots[i].lat * 100);
                            int lat2 = (int)(dot.lat * 100);
                            if (lat == lat2)
                            {
                                if (dot.username.Equals(dots[i].username))
                                {
                                    reg.message = message2.Fail;
                                    return reg;
                                }
                                else
                                {
                                    string name = dots[i].username;
                                    User userChanged = session.Query<User>().First(x => x.UserName.Equals(name));
                                    userChanged.dotsId.Remove(dots[i].Id);

                                    //add event here
                                    if (userChanged.eventsHistory == null)
                                        userChanged.eventsHistory = new List<UserEvent>();
                                    userChanged.eventsHistory.Add(new UserEvent("Player captured your point", dots[i].lon, dots[i].lat, user.UserName));
                                    if (userChanged.eventsHistory.Count > 10)
                                        userChanged.eventsHistory.RemoveRange(10, 1);

                                    if (user.eventsHistory == null)
                                        user.eventsHistory = new List<UserEvent>();
                                    user.eventsHistory.Add(new UserEvent("You captured point", dots[i].lon, dots[i].lat, userChanged.UserName));
                                    if (user.eventsHistory.Count > 10)
                                        user.eventsHistory.RemoveRange(10, 1);

                                    dots[i].date = DateTime.Today;
                                    dots[i].message = dot.message;
                                    dots[i].lat = dot.lat;
                                    dots[i].lon = dot.lng;
                                    dots[i].username = user.UserName;
                                    user.dotsId.Add(dots[i].Id);
                                    session.Store(dots[i]);
                                    session.Store(userChanged);
                                    session.Store(user);
                                    session.SaveChanges();
                                    reg.success = true;
                                    reg.message = message2.Success;
                                    return reg;
                                }
                            }
                        }
                    }

                    if (user.eventsHistory == null)
                        user.eventsHistory = new List<UserEvent>();
                    user.eventsHistory.Add(new UserEvent("You captured new point", dot.lng, dot.lat, null));
                    if (user.eventsHistory.Count > 10)
                        user.eventsHistory.RemoveRange(10, 1);

                    dotCopy.date = DateTime.Today;
                    dotCopy.message = dot.message;
                    dotCopy.lat = dot.lat;
                    dotCopy.lon = dot.lng;
                    dotCopy.username = user.UserName;
                    session.Store(dotCopy);
                    if (user.dotsId == null)
                        user.dotsId = new List<string>();
                    user.dotsId.Add(dotCopy.Id);
                    session.Store(user);
                    session.SaveChanges();
                    reg.success = true;
                    reg.message = message2.Success;
                    return reg;
                }
                catch
                {
                    return reg;
                }
            }
        }

        public List<Dot> GetAll(CornersCorrds corners)
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
            List<Dot> dotsToSend = new List<Dot>();
            IEnumerable<Dot> allDots = GetAll();
            Dot[] dots = allDots.ToArray();
            for (int i = 0; i < dots.Length; i++)
            {
                if (neX > dots[i].lon && neY > dots[i].lat &&
                    swX < dots[i].lon && swY < dots[i].lat)
                {
                    if (dots[i].username == null)
                        dots[i].username = "Unknown user";
                    dotsToSend.Add(dots[i]);
                }
            }
            return dotsToSend;
        }

        public IEnumerable<Dot> GetAll()
        {
            var results = new List<Dot>();

            var conventions = DocumentStoreHolder.Store.Conventions ?? new DocumentConvention();
            var defaultIndexStartsWith = conventions.GetTypeTagName(typeof(Dot));

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                using (var enumerator = session.Advanced.Stream<Dot>(defaultIndexStartsWith))
                {
                    while (enumerator.MoveNext())
                        results.Add(enumerator.Current.Document);
                }
            }

            return results;
        }

        public List<Dot> GetAllUserByName(CornersCorrds corners, string name)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                List<Dot> reg = new List<Dot>();
                try
                {
                    User user = session.Query<User>().First(x => x.UserName.Equals(name));
                    if (user.Id == null)
                        return reg;
                    DotForIndex[] dots = session
                        .Query<UserDotsIndex.Result, UserDotsIndex>()
                        .Where(x => x.UserId.Equals(user.Id))
                        .As<DotForIndex>()
                        .Take(2000)
                        .ToArray();
                    for (int i = 0; i < dots.Length; i++)
                    {
                        if (corners.neX > dots[i].lon && corners.neY > dots[i].lat &&
                        corners.swX < dots[i].lon && corners.swY < dots[i].lat)
                        {
                            Dot dotCopy = new Dot();
                            dotCopy.Id = dots[i].Id;
                            dotCopy.lat = (double)dots[i].lat;
                            dotCopy.lon = (double)dots[i].lon;
                            dotCopy.message = dots[i].message;
                            reg.Add(dotCopy);
                        }
                    }
                    return reg;
                }
                catch
                {
                    return reg;
                }
            }
        }

    }
}
