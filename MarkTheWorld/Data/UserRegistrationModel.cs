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
        public Guid Token { get; set; }
    }

    public enum message2 : int
    {
        /// <summary>
        /// Unknown error
        /// </summary>
        [Display(Name = "Unknown")]
        Unknown,
        /// <summary>
        /// This person already marked this spot
        /// </summary>
        [Display(Name = "Already marked")]
        Fail,
        /// <summary>
        /// Succesfully made an action
        /// </summary>
        [Display(Name = "Success")]
        Success,
        /// <summary>
        /// Username or password didnt match
        /// </summary>
        [Display(Name = "MissMatch")]
        MissMatch
    }
}
