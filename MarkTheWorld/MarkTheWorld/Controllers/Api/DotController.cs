using BusinessLayer.DotService;
using Data;
using Data.DataHelpers;
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
        [Route("Dot")]
        [HttpPost]
        public IHttpActionResult PostDot(DotFromViewModel dot)
        {          
            UserRegistrationModel dotCopy = new UserRegistrationModel();
            if (dot.lat < -300 || dot.lng < -300 || dot.username == null)
                return Content(HttpStatusCode.BadRequest, "Wrong object sent");
            try
            {
                dotCopy = dotService.storeDot(dot);
            }
            catch (Exception)
            {
                return InternalServerError();
            }



            return Ok(dotCopy);
        }

        /// <summary>
        /// Gražina visus kvadratėlius tam tikroje teritorijoje
        /// </summary>
        [ResponseType(typeof(SquaresWithInfo))]
        [Route("squares")]
        [HttpPost]
        public IHttpActionResult GetSquares(CornersCorrds corners)
        {
            if (corners == null)
                return Content(HttpStatusCode.BadRequest, "Wrong object");
            List<Dot> dots = new List<Dot>();
            List<Square> squares = new List<Square>();
            List<SquaresWithInfo> squaresSend = new List<SquaresWithInfo>();
            try
            {
                dots = dotService.getAllDots(corners);
                foreach (Dot dot in dots)
                {
                    squares.Add(new Square(dot.message, dot.date, dotService.coordsToSquare(dot.lat, dot.lon), dot.username));                    
                }
                squaresSend = dotService.groupSquares(squares);

            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(squaresSend);
        }

        /// <summary>
        /// Gražina tik tam tikro žaidėjo kvadratėlius, pagal prisijungimo vardą
        /// </summary>
        [ResponseType(typeof(List<Square>))]
        [Route("squares/{name}")]
        [HttpPost]
        public IHttpActionResult GetUserSquaresByName(CornersCorrds corners, string name)
        {
            if (corners == null || corners.checkCorners())
                return Content(HttpStatusCode.BadRequest, "Wrong object");
            if (name.Length < 3 || name.Length > 25)
                return Content(HttpStatusCode.BadRequest, "Wrong username length");
            List<Dot> gameDots = new List<Dot>();
            List<Square> squares = new List<Square>();
            try
            {
                gameDots = dotService.getUserDotsName(corners, name);
                foreach (Dot dot in gameDots)
                {
                    squares.Add(new Square(dot.message, dot.date, dotService.coordsToSquare(dot.lat, dot.lon), name));
                }
            }
            catch (Exception)
            {
                return InternalServerError();
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
                return InternalServerError();
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
            if (corners == null || corners.checkCorners())
                return Content(HttpStatusCode.BadRequest, "Insuficient information about specific object");
            List<Dot> gameDots = new List<Dot>();
            List<GroupedDotsForApi> groupedDots = new List<GroupedDotsForApi>();
            try
            {
                gameDots = dotService.getUserDotsName(corners, name);
                groupedDots = dotService.groupDots(gameDots, corners, zoomLevel);
            }
            catch (Exception)
            {
                return InternalServerError();
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
            if (corners == null || !corners.checkCorners())
                return Content(HttpStatusCode.BadRequest, "Insuficient information about specific object");
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
                return InternalServerError();
            }

            return Ok(groupedDots);
        }

    }
}