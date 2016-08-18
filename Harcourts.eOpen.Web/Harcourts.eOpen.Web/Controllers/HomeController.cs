using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Harcourts.eOpen.Web.Models;

namespace Harcourts.eOpen.Web.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Dummy");
        }

        [Route("listings/view/{listingNumber}")]
        public ActionResult ListingView(string listingNumber)
        {
            //ViewBag.ListingNumber = listingNumber;
            //return View();
            return Redirect("https://international.harcourts.net/");
            //return Redirect("https://public.nz.dev.harcourts.net/");
        }
    }
}
