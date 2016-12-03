using BusinessLayer.DotService;
using BusinessLayer.Filters;
using BusinessLayer.TestGenerator;
using BusinessLayer.UserService;
using Data;
using Data.DataHelpers;
using Data.DataHelpers.User.ReceivedData;
using Data.ReceivePostData;
using MarkTheWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace MarkTheWorld.Controllers.Api
{
    [EnableCors(origins: "http://localhost:5555", headers: "*", methods: "*")]
    [ValidateViewModel]
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
        [Route("user")]
        [HttpPost]
        public async Task<IHttpActionResult> PostUser(UserRegistrationPost User)
        {
            UserRegistrationModel userCopy = new UserRegistrationModel();
            if (!CountriesList.checkCode(User.CountryCode))
                return Content(HttpStatusCode.BadRequest, "Invalid country code");
            try
            {
                userCopy = await userService.addUser(User);
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
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(true);
        }

        /// <summary>
        /// Randa nurodytą kiekį vartotojų vardų pagal pateiktą string 
        /// </summary>
        [ResponseType(typeof(List<string>))]
        [Route("autocomplete/{username}/{number}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUsersAutoComplete(string username, int number)
        {
            List<string> usernames;
            try
            {
                usernames = await userService.GetUsersAutoComplete(username, number);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "No users found");
            }

            return Ok(usernames);
        }

        /// <summary>
        /// Paima duomenis reikalingus profilio langui
        /// </summary>
        [ResponseType(typeof(UserProfile))]
        [Route("profile/{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProfile(string userName)
        {
            if (userName.Length < 3 || userName.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            UserProfile user = new UserProfile();
            try
            {
                if (await userService.checkUsername(userName))
                    return Content(HttpStatusCode.BadRequest, "User doesn't exist");
                user = await userService.GetProfile(userName);                
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(user);
        }

        /// <summary>
        /// Login, gražina vartotojo token
        /// </summary>
        [ResponseType(typeof(UserRegistrationModel))]
        [Route("user/login")]
        [HttpPost]
        public async Task<IHttpActionResult> GetUsers(UserLoginPost user)
        {
            UserRegistrationPost userLogin = new UserRegistrationPost(user);
            UserRegistrationModel userCopy = new UserRegistrationModel();
            try
            {
                userCopy = await userService.getOne(userLogin);
                if (userCopy.message != message2.Success)
                {
                    ErrorStatus errorCheck = new ErrorStatus(userCopy);
                    return Content(errorCheck.status, errorCheck.message);
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(userCopy);
        }

        /// <summary>
        /// Paima vartotojo daily reward
        /// </summary>
        [ResponseType(typeof(UserDailyReward))]
        [Route("daily/{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckPoints(string userName)
        {
            if (userName.Length < 3 || userName.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            UserDailyReward canTake = new UserDailyReward();
            try
            {
                canTake = await userService.takeUserDaily(userName);
                if (canTake.received < 0)
                    return Content(HttpStatusCode.BadRequest, "Can't get daily reward yet");
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(canTake);
        }
        /*
        /// <summary>
        /// Paima paskutinius 10 vartotojo įvykių
        /// </summary>
        [ResponseType(typeof(List<UserEvent>))]
        [Route("events/{userName}")]
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
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(events);
        }*/

        /// <summary>
        /// Patalpina vartotojo spalvą
        /// </summary>
        [ResponseType(typeof(bool))]
        [Route("color/{userName}")]
        [HttpPost]
        public async Task<IHttpActionResult> PostUserColor(string userName, Colors colors)
        {
            if (userName.Length < 3 || userName.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            try
            {
                return Ok(await userService.postUserColors(userName, colors));
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }
        }

        /// <summary>
        /// Nurodo ar duotas username yra jau užimtas/ true-neužimtas, false-užimtas
        /// </summary>
        [ResponseType(typeof(bool))]
        [Route("username/{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUsername(string userName)
        {
            if (userName.Length < 3 || userName.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            try
            {
                return Ok(await userService.checkUsername(userName));
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }
        }

        /// <summary>
        /// Gražina vartotojo spalvas
        /// </summary>
        [ResponseType(typeof(bool))]
        [Route("color/{userName}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserColor(string userName)
        {
            if (userName.Length < 3 || userName.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            try
            {
                return Ok(await userService.getUserColors(userName));
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }
        }

        /// <summary>
        /// Gražina nurodyta kiekį vartotojų pagal valstybės kodą, jeigu šis nurodytas
        /// Default: countryCode = "", number = 10
        /// Nenurodžius coutryCode bus gražinami iš visų vartotojų atrinkti top users
        /// </summary>
        [ResponseType(typeof(List<TopUser>))]
        [Route("topList")]
        [HttpPost]
        public async Task<IHttpActionResult> GetTopUsers(string countryCode = "", int number = 10, int startingPage = 0)
        {
            List<TopUser> users = new List<TopUser>();
            try
            {
                users = await userService.getTopUsers(countryCode, number, startingPage);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }
            if (users.Count < 1)
            {
                return Content(HttpStatusCode.BadRequest, "No users from this country found");
            }
            return Ok(users);
        }

    }
}