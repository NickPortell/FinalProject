using FinalProject.Models;
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
                #region Generate a List of crime names that the chosen Ability by the User is 'Good' at or has a 'True' relationship with
                Ability ability = ORM.Abilities.Find(user.SuperPower);

                List<string> GoodAt = new List<string>();

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
                #endregion

                #region Using that List, find the max crime rate out of the entire Crimes table for only our crime list and get the State Information, img, and suggested items
                List<Item> SuggestedItems = new List<Item>();
                int maxValue = 0;
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


                #endregion

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
                #region Generate a List of crime names that the chosen Ability by the User is 'Bad' at or has a 'false' relationship with
                Ability ability = ORM.Abilities.Find(user.SuperPower);

                List<string> BadAt = new List<string>();

                foreach (var crime in ability.GetType().GetProperties())
                {
                    if (crime.GetValue(ability) is bool)
                    {
                        bool b = (bool)crime.GetValue(ability);

                        if (!b)
                        {
                            BadAt.Add(crime.Name);
                        }
                    }
                }
                #endregion

                #region Using that List, find the max crime rate out of the entire Crimes table for only our crime list and get the State Information, img, and suggested items
                List<Item> SuggestedItems = new List<Item>();
                int maxValue = 0;
                foreach (Crime crime in ORM.Crimes)
                {
                    foreach (var property in crime.GetType().GetProperties())
                    {

                        if (BadAt.Contains(property.Name))
                        {
                            int current = Convert.ToInt32(property.GetValue(crime));
                            if (current > maxValue)
                            {
                                ViewBag.Name = (string)property.Name;
                                ViewBag.Img = "..\\Pictures\\StateImages\\" + crime.State + ".jpg";
                                ViewBag.State = crime;
                                maxValue = current;
                                SuggestedItems.Add(ORM.Items.Where(i => i.Crime == (string)property.Name && (i.Availability == "bad" || i.Availability == "both")).FirstOrDefault());
                            }
                        }
                    }
                }

                ViewBag.Max = maxValue;
                ViewBag.Ability = ability.Ability1;
                ViewBag.SuggestedItems = SuggestedItems.Distinct();


                #endregion

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

            return View();
        }

        public ActionResult TestAlgorithm()
        {
            AspNetUser user = new AspNetUser();
            user.SuperName = "Evil Dude";
            user.C_Hero_Villain_ = false;
            user.SuperPower = "Fire Breath";

            if (user.C_Hero_Villain_ == true)
            {
                #region Generate a List of crime names that the chosen Ability by the User is 'Good' at or has a 'True' relationship with
                Ability ability = ORM.Abilities.Find(user.SuperPower);

                List<string> GoodAt = new List<string>();

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
                #endregion

                #region Using that List, find the max crime rate out of the entire Crimes table for only our crime list and get the State Information, img, and suggested items
                List<Item> SuggestedItems = new List<Item>();
                int maxValue = 0;
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


                #endregion

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
                #region Generate a List of crime names that the chosen Ability by the User is 'Bad' at or has a 'false' relationship with
                Ability ability = ORM.Abilities.Find(user.SuperPower);

                List<string> BadAt = new List<string>();

                foreach (var crime in ability.GetType().GetProperties())
                {
                    if (crime.GetValue(ability) is bool)
                    {
                        bool b = (bool)crime.GetValue(ability);

                        if (!b)
                        {
                            BadAt.Add(crime.Name);
                        }
                    }
                }
                #endregion

                #region Using that List, find the max crime rate out of the entire Crimes table for only our crime list and get the State Information, img, and suggested items
                List<Item> SuggestedItems = new List<Item>();
                int maxValue = 0;
                foreach (Crime crime in ORM.Crimes)
                {
                    foreach (var property in crime.GetType().GetProperties())
                    {

                        if (BadAt.Contains(property.Name))
                        {
                            int current = Convert.ToInt32(property.GetValue(crime));
                            if (current > maxValue)
                            {
                                ViewBag.Name = (string)property.Name;
                                ViewBag.Img = "..\\Pictures\\StateImages\\" + crime.State + ".jpg";
                                ViewBag.State = crime;
                                maxValue = current;
                                SuggestedItems.Add(ORM.Items.Where(i => i.Crime == (string)property.Name && (i.Availability == "bad" || i.Availability == "both")).FirstOrDefault());
                            }
                        }
                    }
                }

                ViewBag.Max = maxValue;
                ViewBag.Ability = ability.Ability1;
                ViewBag.SuggestedItems = SuggestedItems.Distinct();


                #endregion

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

            return View();
        }

    }
}