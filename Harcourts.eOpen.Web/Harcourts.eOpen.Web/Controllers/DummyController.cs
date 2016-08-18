using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Harcourts.eOpen.Web.Controllers
{
    public class DummyController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Dummy Home Page";

            
            return View();
        }
    }
}
