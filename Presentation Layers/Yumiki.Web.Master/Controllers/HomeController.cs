﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Common.Dictionary;

namespace Yumiki.Web.Master.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();//Redirect(HttpConstants.Pages.Login);
        }
    }
}