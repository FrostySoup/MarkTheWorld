﻿using System;
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
        public static string toThumb(string path)
        {
            string root = HttpRuntime.AppDomainAppPath;

            if (root == null)
                return "";
            path = path.Replace("\\", "/");
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
            thumb.Dispose();
            image.Dispose();
            System.IO.File.Delete(photoUrl);
            string imageName = thumbUrl.Substring(thumbUrl.LastIndexOf('/') + 1);
            return imageName;
        }

        public static string toFixedThumb(string path)
        {
            string root = HttpRuntime.AppDomainAppPath;

            if (root == null)
                return "";
            path = path.Replace("\\", "/");
            root = root.Replace("\\", "/");
            int end = 0;

            var photoUrl = path;
            if (path.Contains("Content"))
                end = path.IndexOf("Content", 0);
            photoUrl = root + path.Substring(end);
            Image image = Image.FromFile(photoUrl);

            int[] sides = countSidesLength(700, image);
            Image thumb = image.GetThumbnailImage(sides[0], sides[1], () => false, IntPtr.Zero);
            var thumbUrl = photoUrl.Replace("OriginalPhoto", "thumb");
            thumb.Save(thumbUrl);
            thumb.Dispose();
            image.Dispose();
            System.IO.File.Delete(photoUrl);
            return thumbUrl;
        }

        private static int[] countSidesLength(int maxSide, Image image)
        {
            int[] sides = { 100, 100 };
            int x = image.Width;
            int y = image.Height;
            if (x > y && x > 700)
            {
                int ratio = x / 700;
                x = 700;
                y = y / ratio;
            }
            else if (y > 700)
            {
                int ratio = y / 700;
                y = 700;
                x = x / ratio;
            }
            sides[0] = x;
            sides[1] = y;
            return sides;
        }

    }
}
