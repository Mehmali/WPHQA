
using QMS.Data;
using QMS.Filters;
using QMS.Models;
using QMS.Repository;
using iTextSharp.tool.xml.html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Controllers
{
    //[CustomAuthorizeFilter]
    public class UserController : Controller
    {
        UserRepository _repoUser = new UserRepository();
        ModuleRepository _repoModule = new ModuleRepository();
        // GET: User
        //[AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.path = AppDomain.CurrentDomain.BaseDirectory + "Templates\\admin\\dist\\images\\Appraisal6.png";
            TempData.Keep("Message");
            return View();
        }

        public ActionResult InvalidURLRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (_repoUser.Validate(user))
            {
                var loggedInUser = _repoUser.GetSystemUser(user);
                var permissions = _repoUser.GetPermissions((Int32)loggedInUser.Role_Id).ToList();
                Session["user"] = loggedInUser;
                Session["Role_Id"] = loggedInUser.Role_Id;
                Session["employeeId"] = loggedInUser.Employee_Id;
                Session["permissions"] = permissions;
                Session["subModules"] = _repoModule.GetSubModulesList();
               
                return RedirectToAction("ComplaintReceivedList", "Complaint", new { });
            }
            else
            {
                TempData["Message"] = "You have entered an invalid username/password";
                return RedirectToAction("Login", "User", new { });
            }
        }


     
        [HttpPost]
        public JsonResult ChangePasswordAjax(string oldPass, string newPass)
        {
            bool result=false;
            var loggedInUser = (SystemUser)Session["user"];
            if (oldPass == loggedInUser.Password)
            {
                _repoUser.ChangePassword(loggedInUser.Id, loggedInUser.Id, newPass);
                result = true;
                ((SystemUser)Session["user"]).Password = newPass;
            }
            else
            {
                result = false;

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [CustomAuthorizeFilter]
        public ActionResult Logout()
        {
            Session.Remove("user");
            Session.Remove("permissions");
           
            Session.Abandon();
            return RedirectToAction("Login", "User", new { });
        }
    }
}