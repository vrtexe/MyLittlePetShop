using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyLittlePetShop.Models;

namespace MyLittlePetShop.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index()
        {
            return View(db.ShoppingItems.ToList());
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
        public ActionResult Create([Bind(Include = "Id,Image,Name,CategoryId,Description,Price,Quantity")] ShoppingItem shoppingItem)
        {
            if (ModelState.IsValid)
            {
                shoppingItem.Category = db.ShoppingCategories.Find(shoppingItem.CategoryId);
                db.ShoppingItems.Add(shoppingItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoppingItem);
        }

        // GET: Items/Edit/5
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

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Image,Name,CategoryId,Description,Price,Quantity")] ShoppingItem shoppingItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingItem);
        }

        // GET: Items/Delete/5
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
