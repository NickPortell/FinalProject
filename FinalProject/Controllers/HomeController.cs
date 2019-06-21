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
            #region Drop-down for superpowers
            List<Ability> list = ORM.Abilities.ToList();
            List<string> powerNames = new List<string>();

            foreach (Ability power in list)
            {
                powerNames.Add(power.Ability1);
            }

            ViewBag.PowerNames = powerNames;
            #endregion

            #region Drop-down for personalities
            List<Mentor> mentors = ORM.Mentors.ToList();
            List<string> personalities = new List<string>();
            List<string> personalitiesDistinct = new List<string>();


            foreach (Mentor m in mentors)
            {
                personalities.Add(m.Personality);
                personalitiesDistinct = personalities.Distinct().ToList();
            }

            ViewBag.Personalities = personalitiesDistinct;

            #endregion

            AspNetUser user = ORM.AspNetUsers.Find(User.Identity.GetUserId());

            ViewBag.User = user;


            return View();
        }

        public ActionResult GetMentors(string SuperPower, string Personality, string SuperName, string HeroVillain)
        {
            #region Drop-down for superpowers
            List<Ability> list = ORM.Abilities.ToList();
            List<string> powerNames = new List<string>();

            foreach (Ability power in list)
            {
                powerNames.Add(power.Ability1);
            }

            ViewBag.PowerNames = powerNames;
            #endregion

            #region Drop-down for personalities
            List<Mentor> mentors = ORM.Mentors.ToList();
            List<string> personalities = new List<string>();
            List<string> personalitiesDistinct = new List<string>();


            foreach (Mentor men in mentors)
            {
                personalities.Add(men.Personality);
                personalitiesDistinct = personalities.Distinct().ToList();
            }

            ViewBag.Personalities = personalitiesDistinct;

            #endregion


            AspNetUser user = ORM.AspNetUsers.Find(User.Identity.GetUserId());
            ORM.AspNetUsers.Attach(user);
            user.SuperPower = SuperPower;
            user.SuperName = SuperName;
            user.Personality = Personality;
            user.C_Hero_Villain_ = bool.Parse(HeroVillain);
            ORM.SaveChanges();

            ViewBag.User = user;


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

        public ActionResult SaveMentor(int Id)
        {

            AspNetUser user = ORM.AspNetUsers.Find(User.Identity.GetUserId());
            ORM.AspNetUsers.Attach(user);
            user.MentorId = Id;
            ORM.SaveChanges();
            return RedirectToAction("UserInfo");




        }

        public ActionResult Test()
        {
            return View();
        }
    }
}






