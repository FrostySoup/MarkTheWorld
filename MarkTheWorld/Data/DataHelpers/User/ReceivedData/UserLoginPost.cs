using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers.User.ReceivedData
{
    public class UserLoginPost
    {
        [Required]
        [StringLength(25, MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string PasswordHash { get; set; }
    }
}
