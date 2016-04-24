using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ReceivePostData
{
    public class UserDailyReward
    {
        public int totalPoints { get; set; }
        public int received { get; set; }
        public bool canGet { get; set; }
        public UserDailyReward()
        {
            totalPoints = 0;
            received = 0;
            canGet = false;
        }
    }
}
