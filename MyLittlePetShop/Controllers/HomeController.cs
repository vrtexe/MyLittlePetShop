using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLittlePetShop.Models;
using PagedList;

namespace MyLittlePetShop.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;
        public HomeController()
        {
            ViewBag.page = 1;
            ViewBag.page1 = 1;
            db = new ApplicationDbContext();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult SearchByCategory(string search,int? id)
        {
            return RedirectToAction("Index", "Items",new { search, id });
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
            Response.Redirect("/Items/Index/?search="+model.Search);
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult GetData(int? page)
        {
            return Json(db.ShoppingItems.ToList().ToPagedList(page.Value,5),JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataNew(int? page)
        {
            return Json(db.ShoppingItems.OrderByDescending(d => d.DateAdded).ToList().ToPagedList(page.Value, 5), JsonRequestBehavior.AllowGet);
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
    }
}