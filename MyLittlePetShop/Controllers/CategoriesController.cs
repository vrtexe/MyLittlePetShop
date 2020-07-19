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
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.ShoppingCategories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCategory shoppingCategory = db.ShoppingCategories.Find(id);
            if (shoppingCategory == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCategory);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Name,Image")] ShoppingCategory shoppingCategory)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCategories.Add(shoppingCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoppingCategory);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCategory shoppingCategory = db.ShoppingCategories.Find(id);
            if (shoppingCategory == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCategory);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Name,Image")] ShoppingCategory shoppingCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingCategory);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCategory shoppingCategory = db.ShoppingCategories.Find(id);
            if (shoppingCategory == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCategory);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingCategory shoppingCategory = db.ShoppingCategories.Find(id);
            db.ShoppingCategories.Remove(shoppingCategory);
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
