using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class MapController : Controller
    {
        private franchiseDbEntities ORM = new franchiseDbEntities();

        public ActionResult Index()
        {
            List<Crime> list = ORM.Crimes.ToList();
            List<string> stateNames = new List<string>();

            foreach(Crime state in list)
            {
                stateNames.Add(state.State);
            }

            ViewBag.StateNames = stateNames;
            return View(list);
        }

        public ActionResult GetMapInfo(string state)
        {
            ViewBag.State = ORM.Crimes.Find(state);
            return View("../Map/Index"); 
        }
    }
}