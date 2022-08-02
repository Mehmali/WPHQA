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
    public class ComplaintMainTypeController : Controller
    {
        ComplaintMainTypeRepository _repoComplaintMainType = new ComplaintMainTypeRepository();
        DepartmentRepository _repoDepartment = new DepartmentRepository();
        // GET: Dempartment
        public ActionResult Index()
        {
            return View();
        }

        [RoleBasedAccessFilter(subModule = "ComplaintMainTypes", accessLevel = 1)]
        public ActionResult List()
        {
           
            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList();
            ViewBag.Departments = _repoDepartment.GetDepartmentList().ToList();
            return View();
        }

        public ActionResult ComplaintMainTypeListByDepartment(int id)
        {
            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeListByDepartmentId(id);
            return PartialView("PartialComplaintMainTypesByDepartment");
        }

        [RoleBasedAccessFilter(subModule = "ComplaintMainTypes", accessLevel = 2)]
        public ActionResult Add()
        {
            ViewBag.Departments = _repoDepartment.GetDepartmentList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(ComplaintMainType complaintMainType)
        {
            
            int employeeId = (int)Session["employeeId"];
            var Departments = _repoDepartment.GetDepartmentList();
            complaintMainType.Department = Departments.Where(x => x.Id.Equals(complaintMainType.Dept_Id)).FirstOrDefault().Dept_Name;

            complaintMainType.CreatedOn = DateTime.Now;
            complaintMainType.CreatedBy = employeeId;
            complaintMainType.LastUpdatedOn = DateTime.Now;
            complaintMainType.LastUpdatedBy = employeeId;
            int deptid = _repoComplaintMainType.AddComplaintMainType(complaintMainType);
            if (deptid > 0)
            {
                return RedirectToAction("List", "ComplaintMainType", new { });
            }
            else
            {
                return RedirectToAction("List", "ComplaintMainType", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "ComplaintMainTypes", accessLevel = 3)]
        public ActionResult Edit(int id)
        {
            ViewBag.ComplaintMainType = _repoComplaintMainType.GetComplaintMainTypeById(id);
            ViewBag.Departments = _repoDepartment.GetDepartmentList().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ComplaintMainType complaintMainType)
        {
           
            int employeeId = (int)Session["employeeId"];

            var Departments = _repoDepartment.GetDepartmentList();
            complaintMainType.Department = Departments.Where(x => x.Id.Equals(complaintMainType.Dept_Id)).FirstOrDefault().Dept_Name;

            complaintMainType.LastUpdatedOn = DateTime.Now;
            complaintMainType.LastUpdatedBy = employeeId;
            int deptid = _repoComplaintMainType.UpdateComplaintMainType(complaintMainType);
            if (deptid > 0)
            {

                return RedirectToAction("List", "ComplaintMainType", new { });
            }
            else
            {
                return RedirectToAction("List", "ComplaintMainType", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "ComplaintMainTypes", accessLevel = 4)]
        public ActionResult Delete(int id)
        {
            bool deletedDept = _repoComplaintMainType.DeleteComplaintMainType(id);
            return RedirectToAction("List", "ComplaintMainType", new { });
        }


     
    }
}