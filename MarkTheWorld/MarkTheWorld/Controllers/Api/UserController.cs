using BusinessLayer.DotService;
using BusinessLayer.TestGenerator;
using BusinessLayer.UserService;
using Data;
using Data.DataHelpers;
using Data.ReceivePostData;
using MarkTheWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace MarkTheWorld.Controllers.Api
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        private readonly UserService userService;        

        public UserController()
        {
            userService = new UserService();
        }

        /// <summary>
        /// Registracija, prideda naują vartotoją į duomenų bazę
        /// </summary>
        [ResponseType(typeof(UserRegistrationModel))]
        [Route("User")]
        [HttpPost]
        public IHttpActionResult PostUser(UserRegistrationPost User)
        {
            UserRegistrationModel userCopy = new UserRegistrationModel();
            if (User == null)
                return Content(HttpStatusCode.BadRequest, "Invalid object");
            if (!User.checkIfValiable())
                return Content(HttpStatusCode.BadRequest, "Wrong length of username or password");
            if (!CountriesList.checkCode(User.CountryCode))
                return Content(HttpStatusCode.BadRequest, "Invalid country code");
            try
            {
                userCopy = userService.addUser(User);
                if (userCopy.message != message2.Success)
                {
                    ErrorStatus errorCheck = new ErrorStatus(userCopy);
                    return Content(errorCheck.status, errorCheck.message);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError){
                    Content = new StringContent("An error occured, please try again or contact the administrator"),
                    ReasonPhrase = "Critical Exception"
                });
            }

            return Ok(userCopy);
        }

        /// <summary>
        /// Generuoja nustatytą kiekį duomenų
        /// </summary>
        [ResponseType(typeof(bool))]
        [Route("generate/{user}/{dots}")]
        [HttpGet]
        public IHttpActionResult GenerateUsers(int user, int dots)
        {    
            try
            {
                GenerateObjects generate = new GenerateObjects();
                generate.GenerateXUsersWithYDots(user, dots);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(true);
        }

        /// <summary>
        /// Login, gražina vartotojo token
        /// </summary>
        [ResponseType(typeof(UserRegistrationModel))]
        [Route("User/Login")]
        [HttpPost]
        public IHttpActionResult GetUsers(UserRegistrationPost user)
        {
            if (User == null)
                return Content(HttpStatusCode.BadRequest, "Invalid object");
            UserRegistrationModel userCopy = new UserRegistrationModel();
            if (!user.checkIfValiable())
                return Content(HttpStatusCode.BadRequest, "Wrong length of username or password");
            try
            {
                userCopy = userService.getOne(user);
                if (userCopy.message != message2.Success)
                {
                    ErrorStatus errorCheck = new ErrorStatus(userCopy);
                    return Content(errorCheck.status, errorCheck.message);
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(userCopy);
        }

        /// <summary>
        /// Patikrina ar vartotojas gali pasiimti taškų
        /// </summary>
        [ResponseType(typeof(bool))]
        [Route("Daily/{userName}")]
        [HttpGet]
        public IHttpActionResult CheckPoints(string userName)
        {
            if (userName.Length < 3 || userName.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            bool canTake = false;
            try
            {
                 canTake = userService.checkUserDaily(userName);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(canTake);
        }

        /// <summary>
        /// Paima paskutinius 10 vartotojo įvykių
        /// </summary>
        [ResponseType(typeof(List<UserEvent>))]
        [Route("Events/{userName}")]
        [HttpGet]
        public IHttpActionResult GetUerEvents(string userName)
        {
            if (userName.Length < 3 || userName.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");

            List<UserEvent> events = new List<UserEvent>();
            try
            {
                events = userService.getUserEvents(userName);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(events);
        }

        /// <summary>
        /// Patalpina vartotojo spalvą
        /// </summary>
        [ResponseType(typeof(bool))]
        [Route("Color/{userName}")]
        [HttpPost]
        public IHttpActionResult PostUserColor(string userName, Colors colors)
        {
            if (userName.Length < 3 || userName.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            if (colors == null)
                return Content(HttpStatusCode.BadRequest, "Object missing");
            try
            {
                return Ok(userService.postUserColors(userName, colors));
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
        [Route("username/{userName}")]
        [HttpGet]
        public IHttpActionResult GetUsername(string userName)
        {
            try
            {
                return Ok(userService.checkUsername(userName));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Gražina vartotojo spalvas
        /// </summary>
        [ResponseType(typeof(bool))]
        [Route("Color/{userName}")]
        [HttpGet]
        public IHttpActionResult GetUserColor(string userName)
        {
            if (userName.Length < 3 || userName.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            try
            {
                return Ok(userService.getUserColors(userName));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Gražina iki 10 vartotojų turinčių daugiausia pažymėtų taškų
        /// </summary>
        [ResponseType(typeof(List<TopUser>))]
        [Route("topList")]
        [HttpGet]
        public IHttpActionResult GetTopUsers()
        {
            List<TopUser> users = new List<TopUser>();
            try
            {
                users = userService.getTopUsers();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(users);
        }

    }
}