using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MyLittlePetShop.Models;
using PagedList;

namespace MyLittlePetShop.Controllers
{
    
    public class HomeController : Controller
    {
        private List<ShoppingItem> cartitems;
        ApplicationDbContext db;
        public HomeController()
        {
            ViewBag.page = 1;
            db = new ApplicationDbContext();
            cartitems = new List<ShoppingItem>();
        }

        public ActionResult Index(int? page, int? page1)
        {
            if (page == null)
            {
                page = 1;
            }
            if(page1 == null)
            {
                page1 = 1;
            }
            ViewBag.page = page;
            ViewBag.page1 = page1;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Search()
        {
            SearchModel model = new SearchModel();
            model.Search = "";
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            ViewBag.search = model.Search;
            Response.Redirect("/Items/Index");
            return PartialView(model);

        }
        public ActionResult ShowItems(int? page)
        {
            ViewBag.page = page;
            List<ShoppingItem> items = db.ShoppingItems.ToList();
            return PartialView(items.ToPagedList(page.Value,5));
        }
        public ActionResult ShowByNew(int? page)
        {
            ViewBag.page1 = page;
            List<ShoppingItem> items = db.ShoppingItems.ToList();
            return PartialView(items.ToPagedList(page.Value, 5));
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public ActionResult AddToCart(string name)
        {
            
           if (Session["cart"] == null)
            {
                List<ShoppingItem> cart = new List<ShoppingItem>();
                var product=db.ShoppingItems.Find(0);
                foreach(var item in db.ShoppingItems)
                {
                    if (item.Name == name)
                    {
                           product = item;
                    }
                }
                
                cart.Add(new ShoppingItem()
                {
                    Name = product.Name,
                    Image = product.Image,
                    Price = product.Price,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<ShoppingItem> cart = (List<ShoppingItem>)Session["cart"];
                
                var product = db.ShoppingItems.Find(0);
                foreach (var item in db.ShoppingItems)
                {
                    if (item.Name == name)
                    {
                        product = item;
                    }
                }
                bool f = false;
                foreach (var item in cart.ToList())
                {
                    if (item.Name == name)
                    {
                        int prevQuantity = item.Quantity;
                        cart.Remove(item);
                        cart.Add(new ShoppingItem()
                        {
                            Name = product.Name,
                            Image=product.Image,
                            Price = product.Price,
                            Quantity = prevQuantity + 1
                        });
                        f = true;
                        break;
                    }
                    
                }
                if (f == false)
                {
                    cart.Add(new ShoppingItem()
                    {
                        Name = product.Name,
                        Image = product.Image,
                        Price = product.Price,
                        Quantity = 1
                    });
                    
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(string name)
        {
            List<ShoppingItem> cart = (List<ShoppingItem>)Session["cart"];
            foreach (var item in cart.ToList())
            {
                if (item.Name == name)
                {
                    cart.Remove(item);
                    break;
                }

            }
            Session["cart"] = cart;
            return RedirectToAction("Cart");
        }
        public ActionResult Cart()
        {
            cartitems=Session["cart"] as List<ShoppingItem>;
            return View(cartitems);
        }
        public ActionResult Checkout(decimal total)
        {
            ViewBag.Total = total;
            return View();
        }
    }
}