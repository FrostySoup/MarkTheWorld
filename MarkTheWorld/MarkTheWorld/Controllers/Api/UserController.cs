using BusinessLayer.DotService;
using BusinessLayer.TestGenerator;
using BusinessLayer.UserService;
using Data;
using MarkTheWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Route("addUser")]
        [HttpPost]
        public IHttpActionResult PostUser(User User)
        {
            UserRegistrationModel userCopy = new UserRegistrationModel();
            if (User.UserName == null || User.PasswordHash == null)
                return Ok(userCopy);
            try
            {
                userCopy = userService.addUser(User);
            }
            catch (Exception)
            {
                return InternalServerError();
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
<<<<<<< HEAD
                generate.GenerateXUsersWithYDots(5, 100);
=======
                generate.GenerateXUsersWithYDots(user, dots);
>>>>>>> b478da8119c850b18b74dd5dc455d9a8c0bc1e2f
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
        [Route("getUser")]
        [HttpPost]
        public IHttpActionResult GetUsers(User user)
        {
            UserRegistrationModel userCopy = new UserRegistrationModel();
            try
            {
                userCopy = userService.getOne(user);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(userCopy);
        }

        /// <summary>
        /// Prideda taškų į vartotojo sąskaitą (NEBAIGTA)
        /// </summary>
        [ResponseType(typeof(UserRegistrationModel))]
        [Route("points/{userName}")]
        [HttpPost]
        public IHttpActionResult GetPoints(string userName)
        {
            UserRegistrationModel userCopy = new UserRegistrationModel();
            try
            {
               // userCopy = userService.getOne(user);
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
        [Route("checkDaily/{userName}")]
        [HttpGet]
        public IHttpActionResult CheckPoints(string userName)
        {
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
        [Route("getEvents/{userName}")]
        [HttpGet]
        public IHttpActionResult GetUerEvents(string userName)
        {
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