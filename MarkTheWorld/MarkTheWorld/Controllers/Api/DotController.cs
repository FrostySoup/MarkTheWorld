using BusinessLayer.DotService;
using Data;
using Data.DataHelpers;
using MarkTheWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

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

        [Route("User")]
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
                return InternalServerError();
            }



            return Ok(dotCopy);
        }


        [Route("squaresInArea")]
        [HttpPost]
        public IHttpActionResult GetSquares(CornersCorrds corners)
        {
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

        [Route("getUserSquares/{token}")]
        [HttpPost]
        public IHttpActionResult GetUserSquares(CornersCorrds corners, Guid token)
        {
            List<Dot> gameDots = new List<Dot>();
            List<Square> squares = new List<Square>();
            try
            {
                gameDots = dotService.getUserDots(corners, token);
                foreach (Dot dot in gameDots)
                {
                    squares.Add(new Square(dot.message, dot.date, dotService.coordsToSquare(dot.lat, dot.lon), dot.username));
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(squares);
        }

        [Route("getUserDots/{token}")]
        [HttpPost]
        public IHttpActionResult GetUserDots(CornersCorrds corners, Guid token)
        {
            List<Dot> gameDots = new List<Dot>();
            try
            {
                gameDots = dotService.getUserDots(corners, token);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(gameDots);
        }

        [Route("dotsInArea")]
        [HttpPost]
        public IHttpActionResult GetDots(CornersCorrds corners)
        {
            List<Dot> gameDots = new List<Dot>();
            try
            {
                gameDots = dotService.getAllDots(corners);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(gameDots);
        }

    }
}