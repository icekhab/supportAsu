using SupportAsu.DTO.Roles;
using SupportAsu.DTO.User;
using SupportAsu.User.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserManagment.Managers;

namespace SupportAsu.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IUserManager _userManager;
        public UserController(IUserService userService, IUserManager userManager)
        {
            _userManager = userManager;
            _userService = userService;
        }
        [Authorize(Roles =Role.Administrator)]
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CheckLogin(string login)
        {
            return Json(_userService.CheckLogin(login), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEdit(UserModel model,bool isCreate,bool isProfile)
        {
            if(ModelState.IsValid)
            {
                if(isProfile)
                {
                    model.Login = User.Identity.Name;
                }
                _userService.InsertOrUpdate(model, isCreate);
            }
            return null;
        }

        [HttpPost]
        public ActionResult GetUsers(string filter)
        {
            var model = _userService.GetUsers(filter);
            return PartialView("Users", model);
        }

        [HttpPost]
        public JsonResult Edit(string login)
        {
            var model = _userService.GetUser(login);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(string id)
        {
            _userService.DeleteUser(id);
            return null;
        }

        [Route("Account")]
        public ActionResult Profile()
        {
            var model = _userService.GetUser(User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public JsonResult CheckPassword(string password)
        {
            string message;
            return Json(_userManager.ValidateUser(User.Identity.Name, password, out message), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            string message;
            if (ModelState.IsValid && _userManager.ValidateUser(User.Identity.Name, model.OldPassword, out message))
            {
                _userManager.ChangePassword(User.Identity.Name, model.NewPassword);
            }
            return null;
        }
    }
}