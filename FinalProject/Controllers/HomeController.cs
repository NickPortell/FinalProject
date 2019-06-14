using FinalProject.Models;
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

            #region Code used to generate Crimes-Table initially

            //string[] states = { "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA","HI",
            //                    "ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI",
            //                    "MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC",
            //                    "ND","OH","OK","OR","PA","RI","SC","SD","TN","TX","UT",
            //                    "VT","VA","WA","WV","WI","WY" };

            //for(int i = 0; i < states.Length; i++)
            //{
            //    Crime crime = new Crime(states[i]);
            //    ORM.Crimes.Add(crime);
            //}

            //ORM.SaveChanges();
            #endregion

            List<Crime> crimeList = ORM.Crimes.ToList();
            return View(crimeList);
        }

    }
}