﻿using FinalProject.Models;
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
            List<Crime> list = ORM.Crimes.ToList();
            List<string> stateNames = new List<string>();

            foreach (Crime s in list)
            {
                stateNames.Add(s.State);
            }

            ViewBag.StateNames = stateNames;

            ViewBag.State = ORM.Crimes.Find(state);

            //ViewBag.StateName = ViewBag.
            //    ViewBag.StatePop = 
            //    ViewBag.StateViolent = 
            //    ViewBag.StateHomicide =
                //ViewBag.StateName =
                //ViewBag.StateName = 
                //ViewBag.StateName =
                //ViewBag.StateName =
                //ViewBag.StateName =

            return View("../Map/Index"); 
        }
    }
}