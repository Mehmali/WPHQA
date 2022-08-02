using QMS.Data;
using QMS.Filters;
using QMS.Models;
using QMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Controllers
{
    //[CustomAuthorizeFilter]
    public class SystemUserController : Controller
    {
        EmployeeRepository _repoEmployee = new EmployeeRepository();
        SystemUserRepository _repoSystemUser = new SystemUserRepository();
        RolesRepository _repoRole = new RolesRepository();
       

        // GET: User
        //[AllowAnonymous]
        [RoleBasedAccessFilter(subModule = "SystemUsers", accessLevel = 1)]
        public ActionResult List()
        {

            ViewBag.Roles = _repoRole.GetRolesList();
            ViewBag.SystemUsers = _repoSystemUser.GetSystemUserList();
            ViewBag.Roles = _repoRole.GetRolesList();
            return View();
        }

        [RoleBasedAccessFilter(subModule = "SystemUsers", accessLevel = 2)]
        public ActionResult Add()
        {
           
            ViewBag.Employees = _repoEmployee.GetEmployeeList().ToList();
            ViewBag.Roles = _repoRole.GetRolesList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(SystemUser sysUser)
        {
           
            int employeeId = (int)Session["employeeId"];

            var Employee = _repoEmployee.GetEmployeeById(sysUser.Employee_Id);
            sysUser.Id = 0;
            sysUser.Emp_Name = Employee.Emp_Name;
            sysUser.CreatedBy = employeeId;
            sysUser.CreatedOn = DateTime.Now;
            sysUser.LastUpdatedBy = employeeId;
            sysUser.LastUpdatedOn = DateTime.Now;

            int sysUserId = _repoSystemUser.AddSystemUser(sysUser);

            if (sysUserId > 0)
            {
                return RedirectToAction("List", "SystemUser", new { });
            }
            else
            {
                return RedirectToAction("Add", "SystemUser", new { });
            }
        }

        [RoleBasedAccessFilter(subModule = "SystemUsers", accessLevel = 3)]
        public ActionResult Edit(int id)
        {
            ViewBag.SystemUser = _repoSystemUser.GetSystemUserId(id);
            ViewBag.Employees = _repoEmployee.GetEmployeeList().ToList();
            ViewBag.Roles = _repoRole.GetRolesList();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(SystemUser sysUser)
        {

            int employeeId = (int)Session["employeeId"];                 
            
           
            sysUser.LastUpdatedBy = employeeId;
            sysUser.LastUpdatedOn = DateTime.Now;

            int sysUserId = _repoSystemUser.UpdateSystemUser(sysUser);

            if (sysUserId > 0)
            {
                return RedirectToAction("List", "SystemUser", new { });
            }
            else
            {
                return RedirectToAction("Edit", "SystemUser", new { });
            }
            
        }

        [RoleBasedAccessFilter(subModule = "SystemUsers", accessLevel = 4)]
        public ActionResult Delete(int id)
        {
            int employeeId = (int)Session["employeeId"];
            bool deletedEmployee = _repoSystemUser.DeleteSystemUser(id, employeeId);
            return RedirectToAction("List", "SystemUser", new { });
        }
    }
}