using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    public class CornersCorrds
    {
        [Required]
        [Range(-400, 400)]
        public double neX { get; set; }

        [Required]
        [Range(-400, 400)]
        public double neY { get; set; }

        [Required]
        [Range(-400, 400)]
        public double swX { get; set; }

        [Required]
        [Range(-400, 400)]
        public double swY { get; set; }


        public bool checkCorners()
        {
            if (neX > 1000 || neX < -1000)
                return false;
            if (neY > 1000 || neY < -1000)
                return false;
            if (swX > 1000 || swX < -1000)
                return false;
            if (swY > 1000 || swY < -1000)
                return false;
            return true;
        }
    }
}
