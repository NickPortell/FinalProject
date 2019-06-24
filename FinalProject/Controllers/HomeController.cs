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

            AspNetUser user = ORM.AspNetUsers.Find(userId);
            if (user.StateId != null)
            {
                ViewBag.Img = "..\\Pictures\\StateImages\\" + user.StateId + ".jpg";
            }

            return View(user);
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

        //////////////////////
        ///How we got states//
        //////////////////////
        //string[] states = { "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA","HI",
        //                    "ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI",
        //                    "MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC",
        //                    "ND","OH","OK","OR","PA","RI","SC","SD","TN","TX","UT",
        //                    "VT","VA","WA","WV","WI","WY" };

        //for(int i = 0; i < states.Length; i++)
        //{
        //    Crime crime = new Crime(states[i]);
        //    ORM.Crimes.Add(crime);
        
        /* public Crime(string state)
         {
             JObject allData = JObject.Parse(GetStateData(state));
             JObject data = (JObject)(allData["results"].Where(s => (int)s["year"] == 2017).First());


             State = (string)(data)["state_abbr"];
             Population = (double?)(data)["population"] ?? 0;
             Violent_Crime = (double?)(data)["violent_crime"] ?? 0;
             Homicide = (double?)(data)["homicide"] ?? 0;

             Rape_Revised = (double?)(data)["rape_revised"] ?? 0;
             Robbery = (double?)(data)["robbery"] ?? 0;
             Aggravated_Assault = (double?)(data)["aggravated_assault"] ?? 0;
             Property_Crime = (double?)(data)["property_crime"] ?? 0;

             Burglary = (double?)(data)["burglary"] ?? 0;
             Larceny = (double?)(data)["larceny"] ?? 0;
             Motor_Vehicle_Theft = (double?)(data)["motor_vehicle_theft"] ?? 0;
             Arson = (double?)(data)["arson"] ?? 0;


         }
         public Crime()
         {

         }*/

        /*private static string apiKey = ConfigurationManager.AppSettings["apiKey"];

        public static string GetStateData(string state)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.usa.gov/crime/fbi/sapi/api/estimates/states/{state}/2000/2017?api_key={apiKey}");

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader data = new StreamReader(response.GetResponseStream());
                return data.ReadToEnd();
            }
            return null;
        }*/

    }
}






