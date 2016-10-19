using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers.Facebook
{
    public class FbRegisterClient
    {
        [Required]
        public string token { get; set; }
        [Required]
        public string userID { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 4)]
        public string userName { get; set; }
        [Required]
        [StringLength(4, MinimumLength = 1)]
        public string countryCode { get; set; }

        [StringLength(4, MinimumLength = 1)]
        public string state { get; set; }
    }
}
