using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class UserProfile
    {
        public string name { get; set; }
        public string flagAddress { get; set; }
        public string countryName { get; set; }
        public int points { get; set; }
        public string colors { get; set; }
        public string pictureAddress { get; set; }
        public DailyReward dailies { get; set; }
    }
}
