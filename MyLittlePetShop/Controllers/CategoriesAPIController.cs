using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MyLittlePetShop.Models;

namespace MyLittlePetShop.Controllers
{
    public class CategoriesAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CategoriesAPI
        public IQueryable<ShoppingCategory> GetShoppingCategories()
        {
            return db.ShoppingCategories;
        }

        // GET: api/CategoriesAPI/5
        [ResponseType(typeof(ShoppingCategory))]
        public IHttpActionResult GetShoppingCategory(int id)
        {
            ShoppingCategory shoppingCategory = db.ShoppingCategories.Find(id);
            if (shoppingCategory == null)
            {
                return NotFound();
            }

            return Ok(shoppingCategory);
        }

        // PUT: api/CategoriesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingCategory(int id, ShoppingCategory shoppingCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(shoppingCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CategoriesAPI
        [ResponseType(typeof(ShoppingCategory))]
        public IHttpActionResult PostShoppingCategory(ShoppingCategory shoppingCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingCategories.Add(shoppingCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shoppingCategory.Id }, shoppingCategory);
        }

        // DELETE: api/CategoriesAPI/5
        [ResponseType(typeof(ShoppingCategory))]
        public IHttpActionResult DeleteShoppingCategory(int id)
        {
            ShoppingCategory shoppingCategory = db.ShoppingCategories.Find(id);
            if (shoppingCategory == null)
            {
                return NotFound();
            }

            db.ShoppingCategories.Remove(shoppingCategory);
            db.SaveChanges();

            return Ok(shoppingCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingCategoryExists(int id)
        {
            return db.ShoppingCategories.Count(e => e.Id == id) > 0;
        }
    }
}