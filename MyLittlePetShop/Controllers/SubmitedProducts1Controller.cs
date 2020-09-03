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
using Microsoft.AspNet.Identity;
using MyLittlePetShop.Models;

namespace MyLittlePetShop.Controllers
{
    public class SubmitedProducts1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SubmitedProducts1
        public IQueryable<SubmitedProducts> GetSubmitedProducts()
        {
            return db.SubmitedProducts;
        }

        // GET: api/SubmitedProducts1/5
        [ResponseType(typeof(SubmitedProducts))]
        public IHttpActionResult GetSubmitedProducts(string id)
        {
            SubmitedProducts submitedProducts = db.SubmitedProducts.Find(id);
            if (submitedProducts == null)
            {
                return NotFound();
            }

            return Ok(submitedProducts);
        }

        // PUT: api/SubmitedProducts1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubmitedProducts(string id, SubmitedProducts submitedProducts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != submitedProducts.UserId)
            {
                return BadRequest();
            }

            db.Entry(submitedProducts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubmitedProductsExists(id))
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

        // POST: api/SubmitedProducts1
        [ResponseType(typeof(SubmitedProducts))]
        public IHttpActionResult PostSubmitedProducts(SubmitedProducts submitedProducts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubmitedProducts.Add(submitedProducts);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SubmitedProductsExists(submitedProducts.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = submitedProducts.UserId }, submitedProducts);
        }

        // DELETE: api/SubmitedProducts1/5
        [ResponseType(typeof(SubmitedProducts))]
        [Authorize(Roles ="Administrator,Seller")]
        public IHttpActionResult DeleteSubmitedProducts(int id)
        {
            SubmitedProducts submitedProducts = db.SubmitedProducts.Find(User.Identity.GetUserId());
            if (submitedProducts == null)
            {
                return NotFound();
            }
            ShoppingCartItems shoppingCartItems = db.ShoppingCartItems.Find(User.Identity.GetUserId());
            if (shoppingCartItems == null)
            {
                return NotFound();
            }
            shoppingCartItems.items.Remove(db.ShoppingItems.Find(id));
            db.Entry(shoppingCartItems).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(submitedProducts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubmitedProductsExists(string id)
        {
            return db.SubmitedProducts.Count(e => e.UserId == id) > 0;
        }
    }
}