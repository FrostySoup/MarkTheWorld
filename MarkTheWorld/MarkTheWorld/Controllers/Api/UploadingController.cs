using BusinessLayer.PhotoUpload;
using Data.DataHelpers.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MarkTheWorld.Controllers.Api
{
    [RoutePrefix("api")]
    public class UploadingController : ApiController
    {
        [Route("uploading")]
        [HttpPost]
        public Task<IEnumerable<Photo>> Post()
        {
            var folderName = "Content/uploadedImages";
            var PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);

                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<Photo>>(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                    {
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }

                    var fileInfo = streamProvider.FileData.Select(i =>
                    {
                        var info = new FileInfo(i.LocalFileName);
                        var path = rootUrl + "/" + folderName + "/" + info.Name;
                        ImageThumb.toThumb(path);
                        return new Photo(info.Name, path, info.Length / 1024);
                    });
                    return fileInfo;
                });
                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }
    }
}