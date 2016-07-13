using BusinessLayer.Filters;
using BusinessLayer.UserService;
using Data.DataHelpers;
using Data.DataHelpers.Facebook;
using Data.DataHelpers.User.SendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace MarkTheWorld.Controllers.Api
{
    [RoutePrefix("api")]
    [ValidateViewModel]
    public class UserWithFBController : ApiController
    {
        private readonly FbUserService userService;

        public UserWithFBController()
        {
            userService = new FbUserService();
        }
        /// <summary>
        /// Facebook prisijungimas, newUser -> true - naujas vartotojas, false - senas vartotojas
        /// </summary>
        [ResponseType(typeof(FbServerLogin))]
        [Route("fblogin")]
        [HttpPost]
        public IHttpActionResult PostLogin(FbClientLogin fb)
        {
            try
            {
                bool newUser = userService.checkUserById(fb.Id);
                FbServerLogin user = new FbServerLogin();
                user.newUser = newUser;
                user.longToken = "";
                if (!newUser)
                {
                    FbNameToken tokenAndName = userService.getLongLiveToken(fb);
                    user.username = tokenAndName.username;
                    user.longToken = tokenAndName.token;
                    user.photo = tokenAndName.photoPath;
                    if (user.longToken == null)
                        return Content(HttpStatusCode.NoContent, "Couldn't receive user token");
                }
                else
                {
                    FbServerLogin userTemp = userService.getUserParams(fb);
                    user.username = userTemp.username;
                    user.country = userTemp.country;
                }
                
                return Ok(user);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unkown error");
            }
        }

        /// <summary>
        /// Užregistruoja naują facebook vartotoją sistemoje
        /// </summary>
        [ResponseType(typeof(Registration))]
        [Route("fbRegister")]
        [HttpPost]
        public IHttpActionResult PostRegister(FbRegisterClient fb)
        {
            try
            {
                Registration token = userService.register(fb);
                if (token == null)
                    return Content(HttpStatusCode.BadRequest, "User already registered");
                if (token.Equals("Invalid token") || token.Equals(""))
                    return Content(HttpStatusCode.BadRequest, "Invalid token");
                return Ok(token);

            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown error");
            }
        }

    }
}