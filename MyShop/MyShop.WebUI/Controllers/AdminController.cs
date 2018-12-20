using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    //[Authorize(Users ="admin@mywebsite.com")]         //first authorize the admin controller, lecture 89, 01:06, first by users
    [Authorize(Roles ="Admin")]                         //second by roles & fill in the database directly, Lecture 89, 03:03
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}