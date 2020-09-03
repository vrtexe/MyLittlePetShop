using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyLittlePetShop.Models;

namespace MyLittlePetShop.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index(string search,int? id)
        {
            List<ShoppingItem> items = db.ShoppingItems.ToList();
            if(!string.IsNullOrEmpty(search))
            {
                items = items.Where(item => item.Category.Name.Contains(search) || item.Name.Contains(search) || item.Description.Contains(search)).ToList();
            }
            if(id != null)
            {
                items = items.Where(item => item.CategoryId == id.Value).ToList();
            }
            return View(items);
        }
        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingItem shoppingItem = db.ShoppingItems.Find(id);
            shoppingItem.Category = db.ShoppingCategories.Find(shoppingItem.CategoryId);
            if (shoppingItem == null)
            {
                return HttpNotFound();
            }
            return View(shoppingItem);
        }

        // GET: Items/Create
        [Authorize(Roles = "Administrator,Seller")]
        public ActionResult Create()
        {
            ViewBag.Categories = db.ShoppingCategories.ToList();
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Seller")]
        public ActionResult Create([Bind(Include = "Id,Image,Name,CategoryId,Description,Price,Quantity")] ShoppingItem shoppingItem)
        {
            if (ModelState.IsValid)
            {
                shoppingItem.Category = db.ShoppingCategories.Find(shoppingItem.CategoryId);
                shoppingItem.DateAdded = DateTime.Now;
                db.ShoppingItems.Add(shoppingItem);
                SubmitedProducts submitedProducts = db.SubmitedProducts.Find(User.Identity.GetUserId());
                submitedProducts.Products.Add(shoppingItem);
                db.Entry(submitedProducts).State = EntityState.Modified;
                db.Entry(shoppingItem).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoppingItem);
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Administrator,Seller")]
        public ActionResult Edit(int? id)
        {
            ViewBag.Categories = db.ShoppingCategories.ToList();
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

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Image,Name,CategoryId,Description,Price,Quantity")] ShoppingItem shoppingItem)
        {
            if (ModelState.IsValid)
            {
                shoppingItem.DateAdded = DateTime.Now;
                db.Entry(shoppingItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingItem);
        }

        // GET: Items/Delete/5
        [Authorize(Roles = "Administrator,Seller")]
        public ActionResult Delete(int? id)
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

        // POST: Items/Delete/5
        [Authorize(Roles = "Administrator,Seller")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingItem shoppingItem = db.ShoppingItems.Find(id);
            db.ShoppingItems.Remove(shoppingItem);
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
