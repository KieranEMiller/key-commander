using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyCdr.UI.Web.Controllers
{
    public class NavController : Controller
    {
        public ActionResult Index()
        {
            return View("IndexView");
        }
    }
}