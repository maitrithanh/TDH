using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;
namespace VTel.Controllers
{
    public class HomeController : Controller
    {
        VTELEntities database = new VTELEntities();
        public ActionResult Index()
        {
            var productlist = database.SANPHAMs.OrderBy(x => x.TENSP);
            return View(productlist.Take(3).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}