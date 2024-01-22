using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHShows.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection col)
        {

            if (Convert.ToString(col["userid"]) == "cinema" && Convert.ToString(col["password"]) == "2017")
            {
                Session["Loggedin"] = "YES";
                return RedirectToAction("../Movies");
            }
            else
            {
                ViewBag.Message = "Invalid userid / password!";
            }

            return View();
        }
    }
}