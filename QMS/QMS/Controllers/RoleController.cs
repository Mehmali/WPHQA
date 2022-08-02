using QMS.Data;
using QMS.Filters;
using QMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Controllers
{
    [CustomAuthorizeFilter]
    public class RoleController : Controller
    {
        // GET: Role
        RolesRepository _repoRole = new RolesRepository();
        // GET: Dempartment
        public ActionResult Index()
        {
            return View();
        }

        [RoleBasedAccessFilter(subModule = "Roles", accessLevel = 1)]
        public ActionResult List()
        {
            
            ViewBag.Roles = _repoRole.GetRolesList();
            return View();
        }
        [RoleBasedAccessFilter(subModule = "Roles", accessLevel = 2)]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Role role)
        {
            
            int employeeId = (int)Session["employeeId"];
            
            role.CreatedOn = DateTime.Now;
            role.CreatedBy = employeeId;
            role.LastUpdatedOn = DateTime.Now;
            role.LastUpdatedBy = employeeId;
            int roleid = _repoRole.AddRole(role);
            if (roleid > 0)
            {
                return RedirectToAction("List", "Role", new { });
            }
            else
            {
                return RedirectToAction("List", "Role", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "Roles", accessLevel = 3)]
        public ActionResult Edit(int id)
        {
            ViewBag.role = _repoRole.GetRoleById(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            
            int employeeId = (int)Session["employeeId"];
           
            role.LastUpdatedOn = DateTime.Now;
            role.LastUpdatedBy = employeeId;
            int roleid = _repoRole.UpdateRole(role);
            if (roleid > 0)
            {

                return RedirectToAction("List", "Role", new { });
            }
            else
            {
                return RedirectToAction("List", "Role", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "Roles", accessLevel = 4)]
        public ActionResult Delete(int id)
        {
            bool deletedRole = _repoRole.DeleteRole(id);
            return RedirectToAction("List", "Role", new { });
        }
    }
}