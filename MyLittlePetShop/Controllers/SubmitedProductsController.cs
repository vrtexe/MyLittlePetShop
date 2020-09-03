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
    [Authorize(Roles = "Administrator,Seller")]
    public class SubmitedProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubmitedProducts
        public ActionResult Index()
        {
            if(db.SubmitedProducts.Find(User.Identity.GetUserId()) == null)
            {
                SubmitedProducts submitedProducts = new SubmitedProducts();
                submitedProducts.UserId = User.Identity.GetUserId();
                submitedProducts.Products = new System.Collections.Generic.List<ShoppingItem>();
                db.SubmitedProducts.Add(submitedProducts);
                db.Entry(submitedProducts).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
            return View(db.SubmitedProducts.Find(User.Identity.GetUserId()).Products);
        }

        // GET: SubmitedProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitedProducts submitedProducts = db.SubmitedProducts.Find(id);
            if (submitedProducts == null)
            {
                return HttpNotFound();
            }
            return View(submitedProducts);
        }

        // GET: SubmitedProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubmitedProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId")] SubmitedProducts submitedProducts)
        {
            if (ModelState.IsValid)
            {
                db.SubmitedProducts.Add(submitedProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(submitedProducts);
        }

        // GET: SubmitedProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitedProducts submitedProducts = db.SubmitedProducts.Find(id);
            if (submitedProducts == null)
            {
                return HttpNotFound();
            }
            return View(submitedProducts);
        }

        // POST: SubmitedProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId")] SubmitedProducts submitedProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submitedProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(submitedProducts);
        }

        // GET: SubmitedProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitedProducts submitedProducts = db.SubmitedProducts.Find(id);
            if (submitedProducts == null)
            {
                return HttpNotFound();
            }
            return View(submitedProducts);
        }

        // POST: SubmitedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubmitedProducts submitedProducts = db.SubmitedProducts.Find(id);
            db.SubmitedProducts.Remove(submitedProducts);
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
