using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class FbClientLogin
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
