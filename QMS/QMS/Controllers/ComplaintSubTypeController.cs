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
    public class ComplaintSubTypeController : Controller
    {
        ComplaintSubTypeRepository _repoComplaintSubType = new ComplaintSubTypeRepository();
        DepartmentRepository _repoDepartment = new DepartmentRepository();
        ComplaintMainTypeRepository _repoComplaintMainType = new ComplaintMainTypeRepository();
        // GET: Dempartment
        public ActionResult Index()
        {
            return View();
        }

        [RoleBasedAccessFilter(subModule = "ComplaintSubTypes", accessLevel = 1)]
        public ActionResult List()
        {
           
            ViewBag.ComplaintSubTypes = _repoComplaintSubType.GetComplaintSubTypeList();
            return View();
        }
        [RoleBasedAccessFilter(subModule = "ComplaintSubTypes", accessLevel = 2)]
        public ActionResult Add()
        {
            ViewBag.Departments = _repoDepartment.GetDepartmentList();
            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(ComplaintSubType complaintSubType)
        {
            
            int employeeId = (int)Session["employeeId"];

            var Departments = _repoDepartment.GetDepartmentList();
            complaintSubType.Department = Departments.Where(x => x.Id.Equals(complaintSubType.Dept_Id)).FirstOrDefault().Dept_Name;
            var ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList();
            complaintSubType.MainType = ComplaintMainTypes.Where(x => x.Id.Equals(complaintSubType.MainType_Id)).FirstOrDefault().MainType;

            complaintSubType.CreatedOn = DateTime.Now;
            complaintSubType.CreatedBy = employeeId;
            complaintSubType.LastUpdatedOn = DateTime.Now;
            complaintSubType.LastUpdatedBy = employeeId;
            int deptid = _repoComplaintSubType.AddComplaintSubType(complaintSubType);
            if (deptid > 0)
            {
                return RedirectToAction("List", "ComplaintSubType", new { });
            }
            else
            {
                return RedirectToAction("List", "ComplaintSubType", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "ComplaintSubTypes", accessLevel = 3)]
        public ActionResult Edit(int id)
        {
            ViewBag.ComplaintSubType = _repoComplaintSubType.GetComplaintSubTypeById(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ComplaintSubType complaintSubType)
        {
           
            int employeeId = (int)Session["employeeId"];

            var Departments = _repoDepartment.GetDepartmentList();
            complaintSubType.Department = Departments.Where(x => x.Id.Equals(complaintSubType.Dept_Id)).FirstOrDefault().Dept_Name;
            var ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList();
            complaintSubType.MainType = ComplaintMainTypes.Where(x => x.Id.Equals(complaintSubType.MainType_Id)).FirstOrDefault().MainType;

            complaintSubType.LastUpdatedOn = DateTime.Now;
            complaintSubType.LastUpdatedBy = employeeId;
            int deptid = _repoComplaintSubType.UpdateComplaintSubType(complaintSubType);
            if (deptid > 0)
            {

                return RedirectToAction("List", "ComplaintSubType", new { });
            }
            else
            {
                return RedirectToAction("List", "ComplaintSubType", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "ComplaintSubTypes", accessLevel = 4)]
        public ActionResult Delete(int id)
        {
            bool deletedDept = _repoComplaintSubType.DeleteComplaintSubType(id);
            return RedirectToAction("List", "ComplaintSubType", new { });
        }

        public ActionResult ComplaintSubTypeListByMainTypeId(int id)
        {
            ViewBag.ComplaintSubTypes = _repoComplaintSubType.GetComplaintSubTypeListByMainTypeId(id);
            return PartialView("PartialComplaintSubTypesByMainType");
        }

    }
}