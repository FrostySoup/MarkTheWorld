using Data;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Index
{
    public class DotByKey : AbstractIndexCreationTask<Dot, DotByKey.Result>
    {

        public class Result
        {
            public string Key { get; set; }
            public string Id { get; set; }
        }

        public DotByKey()
        {
            Map = dots => dots.Select(dot => new
            {
                Key = dot.lat.ToString() + dot.lon.ToString()
            });
        }
    }
}
