﻿using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieAngularJsApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = (ClaimsIdentity)User.Identity;
            ViewBag.Name = user.Name;
            ViewBag.CanEdit = user.FindFirst("CanEdit") != null ? "true" : "false";

            return View();
        }
    }
}
