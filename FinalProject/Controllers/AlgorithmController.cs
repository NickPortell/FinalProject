﻿using FinalProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class AlgorithmController : Controller
    {
        private franchiseDbEntities ORM = new franchiseDbEntities();

        // GET: Algorithm
        public ActionResult Index()
        {
            AspNetUser user = ORM.AspNetUsers.Find(User.Identity.GetUserId());

            if (user.C_Hero_Villain_ == true)
            {
                Ability ability = user.Ability;

                List<string> GoodAt = new List<string>();
                List<Item> SuggestedItems = new List<Item>();
                int maxValue = 0;


                foreach (var crime in ability.GetType().GetProperties())
                {
                    if (crime.GetValue(ability) is bool)
                    {
                        bool b = (bool)crime.GetValue(ability);

                        if (b)
                        {
                            GoodAt.Add(crime.Name);
                        }
                    }
                }


                foreach (Crime crime in ORM.Crimes)
                {
                    foreach (var property in crime.GetType().GetProperties())
                    {

                        if (GoodAt.Contains(property.Name))
                        {
                            int current = Convert.ToInt32(property.GetValue(crime));
                            if (current > maxValue)
                            {
                                ViewBag.Name = (string)property.Name;
                                ViewBag.Img = "..\\Pictures\\StateImages\\" + crime.State + ".jpg";
                                ViewBag.State = crime;
                                maxValue = current;
                                SuggestedItems.Add(ORM.Items.Where(i => i.Crime == (string)property.Name && (i.Availability == "good" || i.Availability == "both")).FirstOrDefault());
                            }
                        }
                    }
                }

                ViewBag.Max = maxValue;
                ViewBag.Ability = ability.Ability1;
                ViewBag.SuggestedItems = SuggestedItems.Distinct();

                #region User chooses personality, what mentors they are good with

                List<Mentor> mentors = ORM.Mentors.ToList();
                List<string> GoodWith = new List<string>();

                foreach (Mentor m in mentors)
                {
                    if (user.Personality == m.Personality)
                    {
                        GoodWith.Add(m.Name);
                    }
                }


                #endregion


            }
            else
            {

            }

            return View();
        }

        public ActionResult TestAlgorithm()
        {
            Ability ability = ORM.Abilities.Find("Fire Breath");
            List<string> GoodAt = new List<string>();
            List<Item> SuggestedItems = new List<Item>();
            int maxValue = 0;


            foreach (var crime in ability.GetType().GetProperties())
            {
                if (crime.GetValue(ability) is bool)
                {
                    bool b = (bool)crime.GetValue(ability);

                    if (b)
                    {
                        GoodAt.Add(crime.Name);
                    }
                }
            }


            foreach (Crime crime in ORM.Crimes)
            {
                foreach (var property in crime.GetType().GetProperties())
                {

                    if (GoodAt.Contains(property.Name))
                    {
                        int current = Convert.ToInt32(property.GetValue(crime));
                        if (current > maxValue)
                        {
                            ViewBag.Name = (string)property.Name;
                            ViewBag.Img = "..\\Pictures\\StateImages\\" + crime.State + ".jpg";
                            ViewBag.State = crime;
                            maxValue = current;
                            SuggestedItems.Add(ORM.Items.Where(i => i.Crime == (string)property.Name && (i.Availability == "good" || i.Availability == "both")).FirstOrDefault());
                        }
                    }
                }
            }

            ViewBag.Max = maxValue;
            ViewBag.Ability = ability.Ability1;
            ViewBag.SuggestedItems = SuggestedItems.Distinct();

            return View();
        }

    }
}