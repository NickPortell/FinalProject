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
            if (userId == null)
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

        public ActionResult GetMentors(string HeroVillain)
        {

            List<Mentor> badMentors = ORM.Mentors.Where(u => u.Hero_Villain == false).ToList();
            List<Mentor> goodMentors = ORM.Mentors.Where(u => u.Hero_Villain == true).ToList();

            List<Mentor> m;

            if (HeroVillain == "true")
            {

                m = goodMentors;
            }
            else
            {

                m = badMentors;
            }

            return View("UserProfile", m);
        }

        //public ActionResult SaveMentor(int Id)
        //{

        //    List<Mentor> thisMentor = ORM.Mentors.Find(Mentor.Id);
        //    ORM.SaveChanges();
        //    return RedirectToAction("UserInfo");




        //}
    }
}






