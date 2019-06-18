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
            string userId = User.Identity.GetUserId();
            
            return View(ORM.AspNetUsers.Find(userId));
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


        public ActionResult UserProfile(string SuperPower, string HeroVillain)
        {

            //string userId = User.Identity.GetUserId();
            //AspNetUser user = ORM.AspNetUsers.Find(userId);

            List<Mentor> goodMentors = ORM.Mentors.Where(u => u.Hero_Villain == true).ToList();
            List<Mentor> badMentors = ORM.Mentors.Where(u => u.Hero_Villain == false).ToList();

            
                if (HeroVillain == "true")
                {
                   
                    ViewBag.Mentors = goodMentors;
                }
                else
                {
                    
                    ViewBag.Mentors = badMentors;
                }
            
            

            
            return View();
        }

    }
}