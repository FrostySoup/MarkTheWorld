using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Drawing;

namespace BusinessLayer.PhotoUpload
{
    public static class ImageThumb
    {
        public static void toThumb(string path)
        {
            string root = WebConfigurationManager.AppSettings["Photo_root"];

            if (root == null)
                return;

            var photoUrl = path;
            photoUrl = photoUrl.Replace("http://localhost:64789/", root);
            Image image = Image.FromFile(photoUrl);
            //Jeigu čia gaunate klaidą vadinas neteisingai nurodėte kelią iki Content folderio Web.config folderyje
            Image thumb = image.GetThumbnailImage(100, 100, () => false, IntPtr.Zero);
            photoUrl = photoUrl.Replace("OriginalPhoto", "thumb");
            thumb.Save(photoUrl);
            thumb.Dispose();
            image.Dispose();
        }
    }
}
