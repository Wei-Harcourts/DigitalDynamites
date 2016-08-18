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
            ViewBag.Title = "Home Page";

            return View();
        }

        [Route("AddNewVisitor", Name = "AddNewVisitor")]
        [HttpGet]
        public ActionResult AddVisitor(string from)
        {
            FacebookUserModel fbUser = null;
            if (!string.IsNullOrEmpty(from))
            {
                fbUser = (FacebookUserModel) Session[from];
            }
            ViewBag.FbUser = fbUser;
            return View();
        }
    }
}
