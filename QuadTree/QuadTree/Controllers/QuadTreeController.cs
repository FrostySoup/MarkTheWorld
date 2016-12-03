using QuadTree.Models;
using QuadTree.QuadServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace QuadTree.Controllers
{
    [RoutePrefix("api")]
    public class QuadTreeController : ApiController
    {
        private readonly IQuadService quadService;

        public QuadTreeController(IQuadService quadService)
        {
            this.quadService = quadService;
        }

        [Route("QuadTree")]
        [HttpPost]
        public IHttpActionResult GetGroupedDots(GetGroupDotsDataReceived dotsToGroup)
        {
            if (dotsToGroup.corners == null || dotsToGroup.dots == null)
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            var groupedDots = quadService.groupDots(dotsToGroup.dots, dotsToGroup.corners, dotsToGroup.zoomLevel);

            return Ok(groupedDots);
        }

        [Route("TestPost")]
        [HttpPost]
        public HttpResponseMessage TestPost(Test test)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            return Request.CreateResponse<Dot>(HttpStatusCode.OK, new Dot()
            {
                Id = test.id
            });
        }

    }
}