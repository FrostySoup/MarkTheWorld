using BusinessLayer.PhotoUpload;
using BusinessLayer.UserService;
using Data;
using Data.DataHelpers.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
        public async Task<HttpResponseMessage> Post()
        {
            var folderName = "Content/img/avatars";
            var PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
                var result = await Request.Content.ReadAsMultipartAsync(streamProvider);               
                var path = result.FileData[0].LocalFileName;

                UserService userServ = new UserService();
                string userToken = result.FormData["token"];
                User user = userServ.getOneByToken(userToken);
                if (user == null)
                {
                    System.IO.File.Delete(path);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "User not found!");
                }
                else {
                    string userOldPhotoPath = user.profilePicture;                    
                    string imageName = ImageThumb.toThumb(path);
                    user.profilePicture = imageName;
                    userServ.editApplicationUser(user);
                    if (!userOldPhotoPath.Contains("facebook") && !userOldPhotoPath.Contains("default"))
                    {
                        try { System.IO.File.Delete(PATH + "\\" + userOldPhotoPath); }
                        catch { /*failed to remove image*/}
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, "success!");
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }
    }
}