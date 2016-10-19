using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.ReceivePostData
{
    public class UserDailyReward
    {
        public int totalPoints { get; set; }
        public int received { get; set; }
        public int timeLeft { get; set; }
        public UserDailyReward()
        {
            totalPoints = -1;
            received = -1;
        }
    }
}
