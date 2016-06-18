using MarkTheWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MarkTheWorld.Controllers
{
    public class DummyController : Controller
    {
        // GET: Dummy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FbLogin(Dummy dum)
        {
            int a = 5;
            a = a + 5;
            return View();
        }
    }
}