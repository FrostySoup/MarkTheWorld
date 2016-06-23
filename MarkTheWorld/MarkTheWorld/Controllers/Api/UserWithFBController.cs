using BusinessLayer.UserService;
using Data.DataHelpers;
using Data.DataHelpers.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace MarkTheWorld.Controllers.Api
{
    [RoutePrefix("api")]

    public class UserWithFBController : ApiController
    {
        private readonly FbUserService userService;

        public UserWithFBController()
        {
            userService = new FbUserService();
        }
        /// <summary>
        /// Nurodo ar duotas username yra jau užimtas/ true-neužimtas, false-užimtas
        /// </summary>
        [ResponseType(typeof(FbServerLogin))]
        [Route("fblogin")]
        [HttpPost]
        public IHttpActionResult PostLogin(FbClientLogin fb)
        {
            if (fb == null)
                return Content(HttpStatusCode.NoContent, "Wrong obejct sent");
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
        /// Nurodo ar duotas username yra jau užimtas/ true-neužimtas, false-užimtas
        /// </summary>
        [ResponseType(typeof(string))]
        [Route("fbRegister")]
        [HttpPost]
        public IHttpActionResult PostRegister(FbRegisterClient fb)
        {
            try
            {
                if (fb != null)
                {
                    string token = userService.register(fb);
                    if (token == null)
                        return Content(HttpStatusCode.BadRequest, "User already registered");
                    if (token.Equals("Invalid token"))
                        return Content(HttpStatusCode.BadRequest, "Invalid token");
                    return Ok(token);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Invalid object sent");
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown error");
            }
        }

        /// <summary>
        /// Nurodo ar duotas username yra jau užimtas/ true-neužimtas, false-užimtas
        /// </summary>
        [ResponseType(typeof(bool))]
        [Route("check")]
        [HttpGet]
        public IHttpActionResult GetLogin()
        {
            try
            {
                return Ok(true);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}