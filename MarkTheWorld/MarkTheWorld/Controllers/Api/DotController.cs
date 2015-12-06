﻿using BusinessLayer.DotService;
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
            List<CornersCorrds> corner = new List<CornersCorrds>();
            try
            {
                dots = dotService.getAllDots(corners);
                foreach (Dot dot in dots)
                {
                    corner.Add(dotService.coordsToSquare(dot.lat, dot.lon));
                }

            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(corner);
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