using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DotFromViewModel
    {
        [Range(-300, 300)]
        [Required]
        public double lat { get; set; }

        [Range(-300, 300)]
        [Required]
        public double lng { get; set; }

        public string message { get; set; }

        public DateTime date { get; set; }

        [StringLength(25, MinimumLength = 4)]
        [Required]
        public string username { get; set; }
    }
}
