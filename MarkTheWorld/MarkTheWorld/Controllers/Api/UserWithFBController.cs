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
            try
            {
                FbServerLogin fbUser = userService.getLongLiveToken(fb);
                return Ok(fbUser);
            }
            catch (Exception)
            {
                return InternalServerError();
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
                string token = userService.register(fb);
                if (token.Equals("Invalid token"))
                    return Content(HttpStatusCode.BadRequest, "Invalid token");
                return Ok(token);
            }
            catch (Exception)
            {
                return InternalServerError();
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