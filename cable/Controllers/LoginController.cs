using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using cable.Models;
using cable.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cable.Controllers
{
    public class LoginController : Controller
    {
       
        private readonly IAuthenTicationService _bLservice;
        public LoginController( IAuthenTicationService bLservice)
        {
            _bLservice = bLservice;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.err = "";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LoginVIewModel _user, string returnUrl)
        {

            var usr = _bLservice.AuthenticateUser(_user.uname, _user.password);
            if (usr != null)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, usr.uname));
                claims.Add(new Claim(ClaimTypes.Surname, usr.Name));
                claims.Add(new Claim(ClaimTypes.Role, usr.role));
                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.
        AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();
                props.IsPersistent = _user.RememberMe;

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.
        AuthenticationScheme,
                    principal, props).Wait();

                return RedirectToAction("", "Home");
            }
            else
            {
                ViewBag.err = "User Name / Password Error";
                return View();
            }
        }
        public IActionResult signout()
        {
            HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("", "Login");
        }
    }
}
