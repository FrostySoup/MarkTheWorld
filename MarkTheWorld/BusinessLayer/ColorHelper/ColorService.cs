using Data.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BusinessLayer.ColorHelper
{
    public static class ColorService
    {
        public static string Darken(Colors color)
        {
            double darkenAmount = 1-((double)((double)color.red / 255 + (double)color.green / 255 + (double)color.blue / 255) / 4);
            Color getColor = Color.FromArgb(color.red, color.green, color.blue);
            HSLColor hslColor = new HSLColor(getColor);
            hslColor.Luminosity *= darkenAmount; // 0 to 1
            return hslColor.ToRGBString();
        }
    }
}
