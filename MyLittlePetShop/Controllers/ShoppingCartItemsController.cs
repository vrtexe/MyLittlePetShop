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
    public class ShoppingCartItemsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ShoppingCartItems
        public IQueryable<ShoppingCartItems> GetShoppingCartItems()
        {
            return db.ShoppingCartItems;
        }

        // GET: api/ShoppingCartItems/5
        [ResponseType(typeof(ShoppingCartItems))]
        public IHttpActionResult GetShoppingCartItems(string id)
        {
            ShoppingCartItems shoppingCartItems = db.ShoppingCartItems.Find(id);
            if (shoppingCartItems == null)
            {
                return NotFound();
            }

            return Ok(shoppingCartItems);
        }

        // PUT: api/ShoppingCartItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingCartItems(string id, ShoppingCartItems shoppingCartItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingCartItems.UserID)
            {
                return BadRequest();
            }

            db.Entry(shoppingCartItems).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartItemsExists(id))
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

        // POST: api/ShoppingCartItems
        [ResponseType(typeof(ShoppingCartItems))]
        public IHttpActionResult PostShoppingCartItems(ShoppingCartItems shoppingCartItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingCartItems.Add(shoppingCartItems);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ShoppingCartItemsExists(shoppingCartItems.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shoppingCartItems.UserID }, shoppingCartItems);
        }

        // DELETE: api/ShoppingCartItems/5
        [ResponseType(typeof(ShoppingCartItems))]
        public IHttpActionResult DeleteShoppingCartItems(int id)
        {
            ShoppingCartItems shoppingCartItems = db.ShoppingCartItems.Find(User.Identity.GetUserId());
            if (shoppingCartItems == null)
            {
                return NotFound();
            }
            shoppingCartItems.items.Remove(db.ShoppingItems.Find(id));
            db.Entry(shoppingCartItems).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(shoppingCartItems);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingCartItemsExists(string id)
        {
            return db.ShoppingCartItems.Count(e => e.UserID == id) > 0;
        }
    }
}