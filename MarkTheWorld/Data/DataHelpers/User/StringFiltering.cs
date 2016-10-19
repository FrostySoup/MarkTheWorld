using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers.User
{
    public class StringFiltering
    {
        public string username { get; set; }
        public int value { get; set; }

        public StringFiltering(string user, string filter)
        {
            value = user.ToLower().IndexOf(filter.ToLower());
            if (value == -1)
                value = 100;
            username = user;
        }
    }
}
