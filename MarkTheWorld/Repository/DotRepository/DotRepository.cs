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
                        //.Where(x => x.lat != -300)
                        .As<DotForIndex>()
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
                    dotCopy.date = dot.date;
                    dotCopy.message = dot.message;
                    dotCopy.lat = dot.lat;
                    dotCopy.lon = dot.lng;
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
                        .ToArray();
                List<Dot> dotsToSend = new List<Dot>();
               for (int i = 0; i < dots.Length; i++)
               {
                    if (corners.neX > dots[i].lon && corners.neY > dots[i].lat &&
                        corners.swX < dots[i].lon && corners.swY < dots[i].lat)
                        dotsToSend.Add(dots[i]);
               }
               return dotsToSend;
            }
        }
    }
}
