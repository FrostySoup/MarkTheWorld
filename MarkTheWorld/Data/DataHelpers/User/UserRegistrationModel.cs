using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserRegistrationModel
    {
        public Boolean success { get; set; }
        /// <summary>
        /// Enum for 4 different responses
        /// </summary>
        public message2 message { get; set; }
        public string Token { get; set; }
        public string username { get; set; }
    }

    public enum message2
    {

        [Display(Name = "No account found with that username")]
        [StringValue("No account found with that username")]
        NoUserName = 0,

        [Display(Name = "The password you entered is incorrect")]
        [StringValue("The password you entered is incorrect")]
        PassMissmatch = 1,

        [Display(Name = "This username is already taken")]
        [StringValue("This username is already taken")]
        UserNameTaken = 2,

        [Display(Name = "Success")]
        [StringValue("Success")]
        Success = 3,

        [Display(Name = "You have already marked this spot")]
        [StringValue("You have already marked this spot")]
        AlreadyMarked = 4
    }
}
