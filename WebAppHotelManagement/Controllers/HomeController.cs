using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppHotelManagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Service()
        {
            return View();
        }

    }
}