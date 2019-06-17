using FinalProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private franchiseDbEntities ORM = new franchiseDbEntities();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult UserInfo()
        {
            var userId = User.Identity.GetUserId();
            return View();
        }

        public ActionResult Inventory()
        {
            ViewBag.Title = "Inventory";
            string userId = User.Identity.GetUserId();
            if(userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<UserItem> items = ORM.UserItems.Where(u => u.UserId == userId).ToList();
            return View(items);
        }

        public ActionResult UserProfile()
        {


            return View();
        }
    }
}