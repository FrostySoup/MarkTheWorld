using BusinessLayer.Delete;
using BusinessLayer.Filters;
using Data;
using Data.DeleteLater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace MarkTheWorld.Controllers.Api
{
    [RoutePrefix("api")]
    public class ItemController : ApiController
    {
        private readonly ItemService itemService;

        public ItemController()
        {
            itemService = new ItemService();
        }
        [ResponseType(typeof(List<Item>))]
        [Route("item")]
        [HttpGet]
        [ValidateViewModel]
        public async Task<IHttpActionResult> PostDot()
        {
            List<Item> items = new List<Item>();
            items = await itemService.getItems();
            
            return Ok(items);
        }

        [ResponseType(typeof(List<Item>))]
        [Route("item")]
        [HttpPost]
        [ValidateViewModel]
        public async Task<IHttpActionResult> PostItem(Item item)
        {            
            await itemService.addItem(item);

            return Ok(true);
        }

    }
}