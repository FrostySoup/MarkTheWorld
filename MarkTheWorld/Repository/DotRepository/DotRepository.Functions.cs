using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DotRepository
{
    public partial class DotRepository
    {
        private string cutImageName(string photoUrl)
        {
            if (photoUrl == null)
                return null;
            return photoUrl.Substring(photoUrl.LastIndexOf('/') + 1);           
        }

        private Dot changeDotValues(DateTime today, string message, double lng, double lat, string userName, string v, Dot oldDot = null)
        {
            Dot dot = new Dot();
            if (oldDot != null)
            {
                dot = oldDot;
            }
            dot.lat = lat;
            dot.lon = lng;
            dot.message = message;
            dot.photoPath = v;
            dot.username = userName;
            dot.date = today;
            return dot;
        }

        private void removeOld(string oldPhoto, string path)
        {
            if (oldPhoto == null)
                return;
            string pathRem = path.Replace("\\", "/") + "Content/img/dotLocation/" + oldPhoto;
            try
            {
                System.IO.File.Delete(pathRem);
            }
            catch {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(path + "Content/failedToRemovePhoto.txt", true))
                {
                    file.WriteLine(oldPhoto);
                }
            }
        }
    }
}
