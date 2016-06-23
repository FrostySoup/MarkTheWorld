using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class Color
    {
        [Required]
        [Range(0, 255)]
        public int red { get; set; }

        [Required]
        [Range(0, 255)]
        public int green { get; set; }

        [Required]
        [Range(0, 255)]
        public int blue { get; set; }
    }
}
