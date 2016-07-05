using BusinessLayer.DotService;
using BusinessLayer.Filters;
using BusinessLayer.UserService;
using Data;
using Data.DataHelpers;
using Data.DataHelpers.Map;
using MarkTheWorld.Models;
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
    [ValidateViewModel]
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
        public IHttpActionResult PostDot(DotFromViewModel dot)
        {          
            UserRegistrationModel dotCopy = new UserRegistrationModel();
            try
            {
                dotCopy = dotService.storeDot(dot);
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

        /// <summary>
        /// Gražina visus kvadratėlius tam tikroje teritorijoje
        /// </summary>
        [ResponseType(typeof(Square))]
        [Route("squares")]
        [HttpPost]
        public IHttpActionResult GetSquares(CornersCorrds corners)
        {
            List<Dot> dots = new List<Dot>();
            List<Square> squares = new List<Square>();
            UserService userService = new UserService();
            try
            {
                dots = dotService.getAllDots(corners);
                foreach (Dot dot in dots)
                {
                    Color squareColor = userService.getUserColors(dot.username);
                    squares.Add(new Square(dot.message, dot.date, dotService.coordsToSquare(dot.lat, dot.lon), dot.username, squareColor));                    
                }
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(squares);
        }

        /// <summary>
        /// Patikrina ar galima patalpinti tašką
        /// </summary>
        [ResponseType(typeof(CanMarkSpot))]
        [Route("dotCheck")]
        [HttpPost]
        public IHttpActionResult CheckDot(DotFromViewModel dot)
        {
            CanMarkSpot results = new CanMarkSpot();
            try
            {
                results = dotService.CheckDot(dot);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(results);
        }

        /// <summary>
        /// Gražina tik tam tikro žaidėjo kvadratėlius, pagal prisijungimo vardą
        /// </summary>
        [ResponseType(typeof(List<Square>))]
        [Route("squares/{name}")]
        [HttpPost]
        public IHttpActionResult GetUserSquaresByName(CornersCorrds corners, string name)
        {
            if (name.Length < 3 || name.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            List<Dot> gameDots = new List<Dot>();
            List<Square> squares = new List<Square>();
            UserService userService = new UserService();
            try
            {
                Color squareColor = userService.getUserColors(name);
                gameDots = dotService.getUserDotsName(corners, name);
                foreach (Dot dot in gameDots)
                {                  
                    squares.Add(new Square(dot.message, dot.date, dotService.coordsToSquare(dot.lat, dot.lon), name, squareColor));
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
        public IHttpActionResult GetPointsByName(string name)
        {
            if (name.Length < 3 || name.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            int points = 0;
            try
            {
                Dot[] dots = dotService.getAlluserDots(name);
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
        [Route("dots/{name}/{zoomLevel}")]
        [HttpPost]
        public IHttpActionResult GetUserDotsByName(CornersCorrds corners, string name, double zoomLevel)
        {
            if (name.Length < 3 || name.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            if (zoomLevel > 15 && zoomLevel < 0)
                return Content(HttpStatusCode.BadRequest, "Wrong zoom level");
            List<Dot> gameDots = new List<Dot>();
            List<GroupedDotsForApi> groupedDots = new List<GroupedDotsForApi>();
            try
            {
                gameDots = dotService.getUserDotsName(corners, name);
                groupedDots = dotService.groupDots(gameDots, corners, zoomLevel);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(groupedDots);
        }



        /// <summary>
        /// Gražina visus apjungtus taškus tam tikroje teritorijoje
        /// </summary>
        [ResponseType(typeof(List<GroupedDotsForApi>))]
        [Route("dots/{zoomLevel}")]
        [HttpPost]
        public IHttpActionResult GetDots(CornersCorrds corners, double zoomLevel)
        {
            if (zoomLevel > 15 && zoomLevel < 0)
                return Content(HttpStatusCode.BadRequest, "Wrong zoom level");
            List<Dot> gameDots = new List<Dot>();
            List<GroupedDotsForApi> groupedDots = new List<GroupedDotsForApi>();
            try
            {
                gameDots = dotService.getAllDots(corners);
                groupedDots = dotService.groupDots(gameDots, corners, zoomLevel);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Unknown server error");
            }

            return Ok(groupedDots);
        }

    }
}