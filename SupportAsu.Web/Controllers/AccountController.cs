using System.Web.Mvc;
using System.Web.Security;
using UserManagment.Managers;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;
using SupportAsu.DTO.Auth;
using SupportAsu.DTO;
using System;
using SupportAsu.Helpers;
using SupportAsu.DTO.Roles;

namespace SupportAsu.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #region LogOn/Off
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LogOnModel model, string ReturnUrl)
        {           
            if (ModelState.IsValid)
            {
                string message = string.Empty;
                if (_userManager.ValidateUser(model.UserName, model.Password, out message))
                {
                    var user = _userManager.GetUser(model.UserName);
                    ClaimsIdentity claim = user.GenerateUserIdentity();
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false,
                        //ExpiresUtc=DateTime.UtcNow.AddSeconds(10)
                    }, claim);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", message);
                }
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View(model);
        }
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
        #endregion


        public JsonResult SaveUsersByRole()
        {
            _userManager.SaveUsersByRole(Role.Administrator);
            _userManager.SaveUsersByRole(Role.Intern);
            _userManager.SaveUsersByRole("Domain Users");
            return null;
        }

    }

}