using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Drawing;
using System.Web;
using Data.DataHelpers.User;
using System.IO;

namespace BusinessLayer.PhotoUpload
{
    public static class ImageThumb
    {
        public static Photo toThumb(string path)
        {
            string root = HttpRuntime.AppDomainAppPath;

            Photo photo = new Photo();

            if (root == null)
                return photo;

            root = root.Replace("\\", "/");
            int end = 0;

            var photoUrl = path;
            if (path.Contains("Content"))
                end = path.IndexOf("Content", 0);
            photoUrl = root + path.Substring(end);
            Image image = Image.FromFile(photoUrl);
            //Jeigu čia gaunate klaidą vadinas neteisingai nurodėte kelią iki Content folderio Web.config folderyje
            Image thumb = image.GetThumbnailImage(100, 100, () => false, IntPtr.Zero);
            var thumbUrl = photoUrl.Replace("OriginalPhoto", "thumb");
            thumb.Save(thumbUrl);
            photo.Name = "name";
            photo.Path = thumbUrl;
            photo.Size = new FileInfo(thumbUrl).Length;
            thumb.Dispose();
            image.Dispose();
            System.IO.File.Delete(photoUrl);
            return photo;
        }
    }
}
