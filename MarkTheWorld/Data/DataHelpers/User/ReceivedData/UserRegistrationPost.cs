using Data.DataHelpers.User.ReceivedData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ReceivePostData
{
    public class UserRegistrationPost
    {
        [Required]
        [StringLength(25, MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 1)]
        public string CountryCode { get; set; }

        [StringLength(4, MinimumLength = 1)]
        public string State { get; set; }

        public UserRegistrationPost(){ }

        public UserRegistrationPost(UserLoginPost user)
        {
            UserName = user.UserName;
            PasswordHash = user.PasswordHash;
        }
    }
}
