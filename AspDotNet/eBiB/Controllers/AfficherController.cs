﻿using eBiB.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace eBiB.Controllers
{
    public class AfficherController : Controller
    {
        // GET: Afficher
        public ActionResult Index()
        {
            Books books = new Books();
            ViewData["Books"] = books.GetBooks();
            return View();
        }
        

    }
}