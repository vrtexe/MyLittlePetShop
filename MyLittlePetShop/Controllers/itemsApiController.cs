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
    public class itemsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/itemsApi
        public IQueryable<ShoppingItem> GetShoppingItems()
        {
            return db.ShoppingItems;
        }

        // GET: api/itemsApi/5
        [ResponseType(typeof(ShoppingItem))]
        public IHttpActionResult GetShoppingItem(int id)
        {
            ShoppingItem shoppingItem = db.ShoppingItems.Find(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            return Ok(shoppingItem);
        }

        // PUT: api/itemsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingItem(int id, ShoppingItem shoppingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingItem.Id)
            {
                return BadRequest();
            }

            db.Entry(shoppingItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingItemExists(id))
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

        // POST: api/itemsApi
        [ResponseType(typeof(ShoppingItem))]
        public IHttpActionResult PostShoppingItem(ShoppingItem shoppingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingItems.Add(shoppingItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shoppingItem.Id }, shoppingItem);
        }

        // DELETE: api/itemsApi/5
        [ResponseType(typeof(ShoppingItem))]
        [Authorize(Roles = "Administrator,Seller")]
        public IHttpActionResult DeleteShoppingItem(int id)
        {
            ShoppingItem shoppingItem = db.ShoppingItems.Find(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            db.ShoppingItems.Remove(shoppingItem);
            db.SaveChanges();

            return Ok(shoppingItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingItemExists(int id)
        {
            return db.ShoppingItems.Count(e => e.Id == id) > 0;
        }
    }
}