using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using QMS.CrystalReports;
using QMS.Data;
using QMS.Filters;
using QMS.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Controllers
{
    [CustomAuthorizeFilter]
    public class DepartmentController : Controller
    {
        DepartmentRepository _repoDepartment = new DepartmentRepository();
        // GET: Dempartment
        public ActionResult Index()
        {
            return View();
        }

        [RoleBasedAccessFilter(subModule = "Departments", accessLevel = 1)]
        public ActionResult List()
        {

            ViewBag.Departments = _repoDepartment.GetDepartmentList();
            return View();
        }
        [RoleBasedAccessFilter(subModule = "Departments", accessLevel = 2)]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Department department)
        {

            int employeeId = (int)Session["employeeId"];

            department.CreatedOn = DateTime.Now;
            department.CreatedBy = employeeId;
            department.LastUpdateOn = DateTime.Now;
            department.LastUpdatedBy = employeeId;
            int deptid = _repoDepartment.AddDepartment(department);
            if (deptid > 0)
            {
                return RedirectToAction("List", "Department", new { });
            }
            else
            {
                return RedirectToAction("List", "Department", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "Departments", accessLevel = 3)]
        public ActionResult Edit(int id)
        {
            ViewBag.department = _repoDepartment.GetDepartmentById(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {

            int employeeId = (int)Session["employeeId"];

            department.LastUpdateOn = DateTime.Now;
            department.LastUpdatedBy = employeeId;
            int deptid = _repoDepartment.UpdateDepartment(department);
            if (deptid > 0)
            {

                return RedirectToAction("List", "Department", new { });
            }
            else
            {
                return RedirectToAction("List", "Department", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "Departments", accessLevel = 4)]
        public ActionResult Delete(int id)
        {
            bool deletedDept = _repoDepartment.DeleteDepartment(id);
            return RedirectToAction("List", "Department", new { });
        }

        public ActionResult GetDepartmentsByComplaintType(string param)
        {
            Boolean isExternal = param == "External" ? true : false;
            ViewBag.Departments = _repoDepartment.GetDepartmentList().Where(x => x.IsExternal.Equals(isExternal)).ToList();
            return PartialView("PartialDepartmentsByComplaintType");
        }

    }
}