using BusinessLayer.ColorHelper;
using BusinessLayer.DotService;
using BusinessLayer.Filters;
using BusinessLayer.UserService;
using Data;
using Data.Database;
using Data.DataHelpers;
using Data.DataHelpers.Map;
using MarkTheWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace MarkTheWorld.Controllers.Api
{
    [EnableCors(origins: "http://localhost:5555", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class DotController : ApiController
    {
        private readonly DotServices dotService;

        public DotController()
        {
            dotService = new DotServices();
        }
        /// <summary>
        /// Leidžia patalpinti tašką duomenų bazėje
        /// </summary>
        [ResponseType(typeof(UserRegistrationModel))]
        [Route("dot")]
        [HttpPost]
        [ValidateViewModel]
        public async Task<IHttpActionResult> PostDot(DotFromViewModel dot)
        {          
            UserRegistrationModel dotCopy = new UserRegistrationModel();
            try
            {
                dotCopy = await dotService.storeDot(dot);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            if (dotCopy.message != message2.Success)
            {
                return Content(HttpStatusCode.BadRequest, dotCopy.messageToString());
            }

            return Ok(dotCopy);
        }

        [Route("test")]
        [ResponseType(typeof(string))]
        [HttpGet]
        [AuthorizeAttribute]
        public IHttpActionResult TestDot()
        {
            
            return Ok(new Dot()
            {
                Id = "Authenticated user called API"
            });
        }

        /// <summary>
        /// Gražina visus kvadratėlius tam tikroje teritorijoje
        /// </summary>
        [ResponseType(typeof(Square))]
        [Route("squares")]
        [HttpPost]
        [ValidateViewModel]
        public async Task<IHttpActionResult> GetSquares(CornersCorrds corners, string name = "")
        {
            List<Dot> dots = new List<Dot>();
            List<Square> squares = new List<Square>();
            UserService userService = new UserService();
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    dots = await dotService.getAllDots(corners);
                    foreach (Dot dot in dots)
                    {
                        Colors squareColor = await userService.getUserColors(dot.username);
                        squares.Add(new Square(dotService.coordsToSquare(dot.lat, dot.lon), squareColor, dot.Id, ColorService.Darken(squareColor)));
                    }
                }
                else
                {
                    Colors squareColor = await userService.getUserColors(name);
                    dots = await dotService.getUserDotsName(corners, name);
                    foreach (Dot dot in dots)
                    {
                        squares.Add(new Square(dotService.coordsToSquare(dot.lat, dot.lon), squareColor, dot.Id, ColorService.Darken(squareColor)));
                    }
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(squares);
        }

        /// <summary>
        /// Gražina vieną kvadratėlį pagal Id
        /// </summary>
        [ResponseType(typeof(DotClick))]
        [Route("square/{Id}")]
        [HttpGet]
        [ValidateViewModel]
        public async Task<IHttpActionResult> GetSquareInfo(string Id)
        {
            DotClick clicked;
            try
            {
                clicked = await dotService.GetDotWithInfo(Id);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }
            if (clicked == null)
                return Content(HttpStatusCode.BadRequest, "Dot not found");
            else if (!string.IsNullOrEmpty(clicked.photoPath))
                clicked.photoPath = "/Content/img/dotLocation/" + clicked.photoPath;
            return Ok(clicked);
        }

        /// <summary>
        /// Patikrina ar galima patalpinti tašką
        /// </summary>
        [ResponseType(typeof(CanMarkSpot))]
        [Route("dotCheck")]
        [HttpPost]
        //[ValidateViewModel]
        public async Task<IHttpActionResult> CheckDot(DotFromViewModel dot)
        {
            CanMarkSpot results = new CanMarkSpot();
            try
            {
                results = await dotService.CheckDot(dot);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(results);
        }

        /// <summary>
        /// (REMOVE LATER)
        /// </summary>
        [ResponseType(typeof(List<Square>))]
        [Route("squares/{name}")]
        [HttpPost]
        public async Task<IHttpActionResult> GetUserSquaresByName(CornersCorrds corners, string name)
        {
            if (name.Length < 3 || name.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            List<Dot> gameDots = new List<Dot>();
            List<Square> squares = new List<Square>();
            UserService userService = new UserService();
            try
            {
                Colors squareColor = await userService.getUserColors(name);
                gameDots = await dotService.getUserDotsName(corners, name);
                foreach (Dot dot in gameDots)
                {                  
                    squares.Add(new Square(dotService.coordsToSquare(dot.lat, dot.lon), squareColor, dot.Id, ColorService.Darken(squareColor)));
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(squares);
        }

        /// <summary>
        /// Gražina tam tikro žaidėjo taškų skaičių, kurį jis gali gauti kartą per parą
        /// </summary>
        [Route("points/{name}")]
        [HttpGet]
        [ValidateViewModel]
        public async Task<IHttpActionResult> GetPointsByName(string name)
        {
            if (name.Length < 3 || name.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            int points = 0;
            try
            {
                Dot[] dots = await dotService.getAlluserDots(name);
                points = dotService.getUserPointsName(dots);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(points);
        }

        /// <summary>
        /// Gražina tik tam tikro žaidėjo taškus, pagal prisijungimo vardą
        /// </summary>
        [ResponseType(typeof(List<GroupedDotsForApi>))]
        [Route("dot/{zoomLevel}")]
        [HttpPost]
        [ValidateViewModel]
        public async Task<IHttpActionResult> GetUserDotsByName(CornersCorrds corners, double zoomLevel, string name = "")
        {
            if (name.Length < 3 || name.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            if (zoomLevel > 15 && zoomLevel < 0)
                return Content(HttpStatusCode.BadRequest, "Wrong zoom level");
            List<Dot> gameDots = new List<Dot>();
            List<GroupedDotsForApi> groupedDots = new List<GroupedDotsForApi>();
            try
            {
                gameDots = await dotService.getUserDotsName(corners, name);
                groupedDots = await dotService.groupDots(gameDots, corners, zoomLevel);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(groupedDots);
        }



        /// <summary>
        /// REMOVE
        /// </summary>
        [ResponseType(typeof(List<GroupedDotsForApi>))]
        [Route("dots/{zoomLevel}")]
        [HttpPost]
        [ValidateViewModel]
        public async Task<IHttpActionResult> GetDots(CornersCorrds corners, double zoomLevel)
        {
            if (zoomLevel > 15 && zoomLevel < 0)
                return Content(HttpStatusCode.BadRequest, "Wrong zoom level");
            List<Dot> gameDots = new List<Dot>();
            List<GroupedDotsForApi> groupedDots = new List<GroupedDotsForApi>();
            try
            {
                gameDots = await dotService.getAllDots(corners);
                groupedDots = await dotService.groupDots(gameDots, corners, zoomLevel);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(groupedDots);
        }

    }
}