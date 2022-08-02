using QMS.Data;
using QMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Controllers
{
    public class UserPermissionController : Controller
    {
        UserPermissionRepository _repoUserPermission = new UserPermissionRepository();
        ModuleRepository _repoModule = new ModuleRepository();
        RolesRepository _repoRoles = new RolesRepository();
        // GET: Permission
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
          
            ViewBag.UserPermissions = _repoUserPermission.GetUserPermissionList();
            ViewBag.SubModules = _repoModule.GetSubModulesList();
            var rolesList = _repoRoles.GetRolesList();
            if (rolesList != null)
            {
                rolesList = rolesList.ToList();
            }
            ViewBag.Roles = rolesList;
            return View();
        }

        [HttpGet]
        public JsonResult GetPermissionsByRoleId(int id)
        {
            Session["orgId"] = 2;
            int orgId = (int)Session["orgId"];
            var permissions = _repoUserPermission.GetUserPermissionList().Where(p => p.Role_Id.Equals(id)).ToList();

            var modules = _repoModule.GetSubModulesList();

            foreach (var module in modules)
            {
                if (!permissions.Any(p => p.SubModule_Id.Equals(module.Id)))
                {
                    var missingPermission = new UserPermission();
                    missingPermission.SubModule_Id = module.Id;
                    missingPermission.AccessLevel = 0;
                    permissions.Add(missingPermission);
                }
            }

            return Json(permissions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public int Add(UserPermission userpermission)
        {
            
            int employeeId = (int)Session["employeeId"];

            //if (userpermission.AccessLevel == 0)
            //{
            //    bool deleted = _repoUserPermission.DeleteUserPermission(userpermission.Role_Id, userpermission.SubModule_Id, orgId);
            //    if (deleted)
            //        return 1;
            //    else
            //        return 0;
            //}
            //else
            //{
           
            userpermission.CreatedOn = DateTime.Now;
            userpermission.CreatedBy = employeeId;
            userpermission.LastUpdatedOn = DateTime.Now;
            userpermission.LastUpdatedBy = employeeId;
            int deptid = _repoUserPermission.AddUserPermission(userpermission);
            return deptid;
            //}
        }

        public ActionResult Edit(int id)
        {
            ViewBag.permissions = _repoUserPermission.GetUserPermissionById(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserPermission userpermission)
        {
           
            int employeeId = (int)Session["employeeId"];
           
            userpermission.LastUpdatedOn = DateTime.Now;
            userpermission.LastUpdatedBy = employeeId;
            int deptid = _repoUserPermission.UpdateUserPermission(userpermission);
            if (deptid > 0)
            {

                return RedirectToAction("List", "UserPermission", new { });
            }
            else
            {
                return RedirectToAction("List", "UserPermission", new { });
            }
        }

        public ActionResult Delete(int id)
        {
            bool deleteduserpermission = _repoUserPermission.DeleteUserPermission(id);
            return RedirectToAction("List", "UserPermission", new { });
        }
    }
}