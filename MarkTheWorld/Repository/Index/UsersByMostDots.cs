using Data;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Index
{
    public class UsersByMostDots : AbstractIndexCreationTask<User, UsersByMostDots.Result>
    {

        public class Result
        {
            public string name { get; set; }
            public int numberOfDots { get; set; }
        }

        public UsersByMostDots()
        {
            Map = users => users.Select(user => new
                {
                    name = user.UserName,
                    numberOfDots = user.dotsId.Count                          
                });          
        }
    }
}