using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class Colors
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

        public string colorsToString()
        {
            return string.Format("rgb({0}, {1}, {2})", red, green, blue);
        }
    }
}
