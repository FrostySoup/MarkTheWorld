using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.DataHelpers;

namespace Repository.DotRepository
{
    public class DotRepository : GenericRepository.GenericRepository, IDotRepository
    {
        public Dot AddOne(DotFromViewModel dot)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User user = session.Query<User>().First(x => x.Token.Equals(dot.username));
                    if (user.Id == null)
                        return new Dot();
                    Dot dotCopy = new Dot();
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
                    return dotCopy;
                }
                catch
                {
                    return new Dot();
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
                    if (corners.nwX > dots[i].lon && corners.nwY > dots[i].lat &&
                        corners.seX < dots[i].lon && corners.seY < dots[i].lat)
                        dotsToSend.Add(dots[i]);
               }
               return dotsToSend;
            }
        }
    }
}
