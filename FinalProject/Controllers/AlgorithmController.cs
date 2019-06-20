using FinalProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
            //Grabbed the current user as an object
            AspNetUser user = ORM.AspNetUsers.Find(User.Identity.GetUserId());



            List<Crime> crimes = ORM.Crimes.ToList();


            //Create a local list of names for locations in our Crimes table
            List<string> locations = new List<string>();

            foreach (Crime c in crimes)
            {
                locations.Add(c.State);
            }

            //Create a list of the types of crimes 
            List<string> crimeTypes = new List<string>();
            foreach (var type in ORM.Crimes.GetType().GetProperties())
            {
                crimeTypes.Add(type.Name);
            }

            List<Crime> SortedList = crimes.OrderBy(o => o.Arson).ToList();


            List<Ability> abilities = ORM.Abilities.ToList();


            //Found either good or bad 
            //Ability ability = new Ability();
            //List<string> GoodAt = new List<string>();
            //List<string> BadAt = new List<string>();

            //foreach (var crime in ability.GetType().GetProperties())
            //{
            //    if (crime.GetValue(ability) is bool)
            //    {
            //        bool b = (bool)crime.GetValue(ability);

            //        if (b)
            //        {
            //            GoodAt.Add(crime.Name);
            //        }
            //        else
            //        {
            //            BadAt.Add(crime.Name);
            //        }
            //    }
            //}

            //Check user's Alliance 

            if (user.C_Hero_Villain_ == true)
            {
                #region After user chooses ability, what crimes they are good at stopping
                Ability ability = new Ability();
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

                #region Given a list of crimes that they are good at, what locations have the highest count of those crimes

                List<string> GoodLocations = new List<string>();

                foreach(var property in ability.GetType().GetProperties())
                {
                    if(GoodAt.Contains(property.Name))
                    {
                        property.GetValue(ability);
                    }
                }

                #endregion

                #region User chooses personality, what mentors they are good with

                List<Mentor> mentors = ORM.Mentors.ToList();
                List<string> GoodWith = new List<string>();

                foreach(Mentor m in mentors)
                {
                    if(user.Personality == m.Personality)
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
    }
}