using Data;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Index
{
    public class UserDotsIndex: AbstractMultiMapIndexCreationTask<UserDotsIndex.Result>
    {

        public class Result
        {
            public string Id { get; set; }
            public double coordX { get; set; }
            public double coordY { get; set; }
            public string UserId { get; set; }
        }

        public UserDotsIndex()
        {
            AddMap<User>(users => from user in users
                                  from dotId in user.dotsId
                                  select new
                                  {
                                      Id = dotId,
                                      coordX = -100,
                                      coordY = -100,
                                      UserId = user.Id
                                  });

            AddMap<Dot>(dots => from dot in dots
                                  select new
                                  {
                                      Id = dot.Id,
                                      coordX = dot.lon,
                                      coordY = dot.lat,
                                      UserId = (string)null
                                  });

            Reduce = results => from result in results
                                group result by result.Id
                                    into g
                                    select new
                                    {
                                        Id = g.Key,
                                        coordX = g.Select(x => x.coordX).Where(x => x != -100).First(),
                                        coordY = g.Select(x => x.coordY).Where(x => x != -100).First(),
                                        UserId = g.Select(x => x.UserId).Where(x => x != null).First()
                                    };
        }
    }
}