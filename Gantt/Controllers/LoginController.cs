﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gantt.Models;

namespace Gantt.Controllers
{
    public class LoginController : Controller
    {
        private Gantt_dbEntities db = new Gantt_dbEntities();

       #region Login

        // GET: Login
        public ActionResult Index()
        {
            Session["LoggedIn"] = null;
            return View();
        }

        public ActionResult Register()
        {
            Session["LoggedIn"] = 2;
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "username,password,email,firstname,lastname,gender,bday")] GC_User user)
        {
            if (ModelState.IsValid)
            {
                db.GC_User.Add(user);
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Validate()
        {
            string user = Request.Form["username"];
            string pass = Request.Form["password"];

            Console.WriteLine("FORM USER: " + user);
            Console.WriteLine("FORM PASS: " + pass);

            foreach (var acct in db.GC_User)
            {
                Console.WriteLine("FORM USER: " + user);
                Console.WriteLine("FORM PASS: " + pass);

                Console.WriteLine("ACCT VAR");
                Console.WriteLine("username: " + acct.username);
                Console.WriteLine("password: " + acct.password);
                if((user == acct.username) && (pass == acct.password))
                {
                    Session["LoggedIn"] = 1;
                    Session["Username"] = user;

                    return Redirect("/Dashboard/Index");
                }
            }
            return Redirect("/Login/Index");
        }

        public ActionResult Logout()
        {
            Session["LoggedIn"] = null;
            Session["Username"] = "User";
            return View("Index");
        }

#endregion

    }
}