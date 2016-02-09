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
            public double? lat { get; set; }
            public double? lon { get; set; }        
            public string message { get; set; }
            public string UserId { get; set; }
        }

        public UserDotsIndex()
        {
            AddMap<User>(users => from user in users
                                  from dotId in user.dotsId
                                  select new
                                  {
                                      Id = dotId,
                                      lat = (double?)null,
                                      lon = (double?)null,                                    
                                      message = (string)null,
                                      UserId = user.Id
                                  });

            AddMap<Dot>(dots => from dot in dots
                                  select new
                                  {
                                      Id = dot.Id,
                                      lat = dot.lat,
                                      lon = dot.lon,                                      
                                      message = dot.message,
                                      UserId = (string)null
                                  });

            Reduce = results => from result in results
                                group result by result.Id
                                    into g
                                    select new
                                    {
                                        Id = g.Key,
                                        lat = g.Select(x => x.lat).Where(x => x != null).First(),
                                        lon = g.Select(x => x.lon).Where(x => x != null).First(),                                        
                                        message = g.Select(x => x.message).Where(x => x != null).First(),
                                        UserId = g.Select(x => x.UserId).Where(x => x != null).First()
                                    };
        }     

    }
}