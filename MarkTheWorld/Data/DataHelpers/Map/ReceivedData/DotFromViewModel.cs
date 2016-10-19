using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public string token { get; set; }

        public DotFromViewModel() { }

        public DotFromViewModel(double la, double ln, string messag, string toke)
        {
            lat = la;
            lng = ln;
            message = messag;
            date = DateTime.Now;
            token = toke;
        }
    }
}
