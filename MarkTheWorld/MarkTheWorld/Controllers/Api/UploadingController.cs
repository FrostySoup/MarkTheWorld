using BusinessLayer.DotService;
using BusinessLayer.PhotoUpload;
using BusinessLayer.UserService;
using Data;
using Data.Database;
using Data.DataHelpers.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace MarkTheWorld.Controllers.Api
{
    [EnableCors(origins: "http://localhost:5555", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class UploadingController : ApiController
    {

        private readonly DotServices dotService;

        public UploadingController()
        {
            dotService = new DotServices();
        }

        [Route("uploading")]
        [HttpPost]
        [ResponseType(typeof(string))]
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
                User user = await userServ.getOneByToken(userToken);
                if (user == null)
                {
                    System.IO.File.Delete(path);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "User not found!");
                }
                else {
                    string userOldPhotoPath = user.profilePicture;                    
                    string imageName = ImageThumb.toThumb(path);
                    user.profilePicture = imageName;
                    await userServ.editApplicationUser(user);
                    if (!userOldPhotoPath.Contains("facebook") && !userOldPhotoPath.Contains("default"))
                    {
                        try { System.IO.File.Delete(PATH + "\\" + userOldPhotoPath); }
                        catch { /*failed to remove image*/}
                    }
                }            
                return Request.CreateResponse(HttpStatusCode.OK, "/Content/img/avatars/" + user.profilePicture);
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }

        /// <summary>
        /// Dot with photo, prarams:
        /// token - string
        /// message (optional) - string
        /// lon - double
        /// lat - double
        /// file
        /// </summary>
        [Route("dotPhoto")]
        [HttpPost]
        public async Task<HttpResponseMessage> PhotoPost()
        {
            var folderName = "Content/img/dotLocation";
            var PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
                var result = await Request.Content.ReadAsMultipartAsync(streamProvider);
                var path = result.FileData[0].LocalFileName;

                UserService userServ = new UserService();
                string userToken = result.FormData["token"];
                string message = result.FormData["message"];
                var lng = result.FormData["lng"];
                var lat2 = result.FormData["lat"];
                double lon = 0;
                double lat = 0;
                try {
                    lon = double.Parse(result.FormData["lng"], CultureInfo.InvariantCulture);
                    lat = double.Parse(result.FormData["lat"], CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "Lon and lat is not properly formatted"));
                }
                string imageName = ImageThumb.toFixedThumb(path);

                DotFromViewModel dot = new DotFromViewModel(lat, lon, message, userToken);

                dotService.storeDot(dot, image: imageName);
                return Request.CreateResponse(HttpStatusCode.OK, "success!");
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }

    }


}