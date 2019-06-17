using FinalProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class ShopController : Controller
    {
        franchiseDbEntities ORM = new franchiseDbEntities();

        public ActionResult Index(string message)
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Message = message;
            return View(ORM.Items.ToList());
        }

        public bool CanPurchase(Item item, int quantity)
        {
            AspNetUser user = ORM.AspNetUsers.Find(User.Identity.GetUserId());
            if (item.Cost * quantity > user.Bitcoin)
            {
                return false;
            }

            return true;
        }

        public ActionResult Purchase(int id, int quantity)
        {
            if(User == null)
            {
                return RedirectToAction("Login", "Account");
            }

            AspNetUser user = ORM.AspNetUsers.Find(User.Identity.GetUserId());

            Item item = ORM.Items.Find(id);

            if (CanPurchase(item, quantity))
            {
                UserItem newItem = new UserItem
                {
                    Item = item,
                    AspNetUser = user,
                    ItemId = item.Id,
                    UserId = user.Id
                };
                user.Bitcoin -= item.Cost * quantity;
                ORM.SaveChanges();

                return RedirectToAction("Index", new { message = $"{item.ItemName} purchased successfully." });
            }

            return RedirectToAction("Index", new { message = $"You don't have the funds for {item.ItemName}!" });
        }
    }
}