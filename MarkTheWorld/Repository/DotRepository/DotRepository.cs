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

namespace Repository.DotRepository
{
    public class DotRepository : GenericRepository.GenericRepository, IDotRepository
    {
        public UserRegistrationModel AddOne(DotFromViewModel dot)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                UserRegistrationModel reg = new UserRegistrationModel();
                reg.success = false;
                reg.message = "Unknown error";
                try
                {                 
                    User user = session.Query<User>().First(x => x.Token.Equals(dot.username));
                    if (user.Id == null)
                        return reg;
                    Dot dotCopy = new Dot();
                    DotForIndex[] dots = session
                        .Query<UserDotsIndex.Result, UserDotsIndex>()
                        .Where(x => x.UserId.Equals(user.Id))
                        .As<DotForIndex>()
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
                                reg.message = "You already marked this dot";
                                return reg;
                            }
                        }
                    }
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
                    reg.message = "Success";
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
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
               Dot[] dots = session
                        .Query<Dot>()
                        .Take(5000)
                        .ToArray();
               List<Dot> dotsToSend = new List<Dot>();
               for (int i = 0; i < dots.Length; i++)
               {
                    if (corners.neX > dots[i].lon && corners.neY > dots[i].lat &&
                        corners.swX < dots[i].lon && corners.swY < dots[i].lat)
                    {
                        if (dots[i].username == null)
                            dots[i].username = "Unknown user";
                        dotsToSend.Add(dots[i]);
                    }
               }
               return dotsToSend;
            }
        }

        public List<Dot> GetAllUser(CornersCorrds corners, Guid token)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                List<Dot> reg = new List<Dot>();
                try
                {
                    User user = session.Query<User>().First(x => x.Token.Equals(token));
                    if (user.Id == null)
                        return reg;                   
                    DotForIndex[] dots = session
                        .Query<UserDotsIndex.Result, UserDotsIndex>()
                        .Where(x => x.UserId.Equals(user.Id))
                        .As<DotForIndex>()
                        .Take(100000)
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
