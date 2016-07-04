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
