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
        public string flagAdress { get; set; }
        public int points { get; set; }
        public Colors colors { get; set; }
        public string pictureAdress { get; set; }
        public DailyReward dailies { get; set; }
    }
}
