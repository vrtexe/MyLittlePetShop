﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLittlePetShop.Models;

namespace MyLittlePetShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
        public ActionResult Search(String model)
        {
            Response.Redirect("/Home/About");
            return PartialView(model);

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}