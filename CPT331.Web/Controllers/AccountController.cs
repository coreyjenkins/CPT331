﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPT331.Web.Models.Account;
using CPT331.Web.Attributes;

namespace CPT331.Web.Controllers
{
    /// <summary>
    ///    A controller that provides methods that respond to HTTP requests that are related 
    ///    to the user access and authentication.
    /// </summary>
    /// <permission cref="CPT331.Web.Attributes.AdminAuthorize">Only authorised personnel have access to restricted methods.</permission>
    [AdminAuthorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// View the Login page for the Administration portal.
        /// </summary>
        /// <returns>The 'Account/Login' view.</returns>
        /// <permission cref="System.Web.Mvc.AllowAnonymousAttribute">Everyone has access to this method.</permission>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Title = "Admin";
            return View();
        }

        /// <summary>
        /// Authenticates a persons session using the credentials provided.
        /// </summary>
        /// <param name="loginName">The name associated to the persons account.</param>
        /// <param name="password">The passed for the persons account.</param>
        /// <returns>Admin 'Account/Home' view if authentication passes; otherwise the 'Account/Login' view.</returns>
        /// <permission cref="System.Web.Mvc.AllowAnonymousAttribute">Everyone has access to this method.</permission>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string loginName, string password)
        {
            if ("administrator".Equals(loginName) && "Rgx53r$t5r".Equals(password))
            {
                Session["user"] = new UserModel() { LoginName = loginName, Password = password };
                return RedirectToAction("Home", "Admin");
            }
            return View();
        }

        /// <summary>
        /// <para>Provides a means to securely log out of the Administration portal.</para>
        /// </summary>
        /// <returns>The 'Account/Login' view.</returns>
        /// <permission cref="CPT331.Web.Attributes.AdminAuthorize">Only authorised personnel have access to this method.</permission>
        [HttpGet]
        public ActionResult SignOut()
        {
            ViewBag.Message = "Logging off...";
            ModelState.Clear();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}