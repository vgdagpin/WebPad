﻿using System.Web.Mvc;

namespace OnlineNotepad.Controllers
{
        public class HomeController : Controller
        {
            public ActionResult Index(string id)
            {
                return View();
            }
        }
}
