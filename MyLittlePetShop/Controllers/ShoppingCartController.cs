using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyLittlePetShop.Models;
using Microsoft.AspNet.Identity;

namespace MyLittlePetShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View(db.ShoppingCartItems.ToList());
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details()
        {
            List<ShoppingItem> shoppingItem = db.ShoppingCartItems.Find(User.Identity.GetUserId()).items;
            if (shoppingItem == null)
            {
                return HttpNotFound();
            }
            return View(shoppingItem);
        }

        // GET: ShoppingCart/Create
        public ActionResult AddItemToCart(int id, int quantity = 1)
        {
            string Userid = User.Identity.GetUserId();
            ShoppingCartItems shoppingCartItems = db.ShoppingCartItems.Find(Userid);//.Where(i => i.UserID.Equals(Userid)).First();
            if(shoppingCartItems != null)
            {
                ShoppingItem item = db.ShoppingItems.Find(id);
                shoppingCartItems.Quantity = item.Quantity >= quantity ? quantity : item.Quantity;
                shoppingCartItems.items.Add(item);
                db.Entry(shoppingCartItems).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: ShoppingCart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingItem shoppingItem = db.ShoppingItems.Find(id);
            if (shoppingItem == null)
            {
                return HttpNotFound();
            }
            return View(shoppingItem);
        }

        // POST: ShoppingCart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID")] ShoppingItem shoppingItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingItem);
        }

        // GET: ShoppingCart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItems shoppingCartItems = db.ShoppingCartItems.Find(User.Identity.GetUserId());
            if (shoppingCartItems == null)
            {
                return HttpNotFound();
            }
            shoppingCartItems.items.Remove(db.ShoppingItems.Find(id));
            db.Entry(shoppingCartItems).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ShoppingCartItems shoppingCartItems = db.ShoppingCartItems.Find(id);
            db.ShoppingCartItems.Remove(shoppingCartItems);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
