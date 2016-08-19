using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Harcourts.eOpen.Web.Models;

namespace Harcourts.eOpen.Web.Controllers
{
    public class DummyController : Controller
    {
        public ActionResult Index(string from)
        {
            ViewBag.Title = "Dummy Home Page";

            FacebookUserModel fbUser = null;
            if (!string.IsNullOrEmpty(from))
            {
                fbUser = (FacebookUserModel)Session[from];
            }
            ViewBag.FbUser = fbUser;

            return View();
        }
    }
}
