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
    public class FaultTypeController : Controller
    {
        FaultTypeRepository _repoFaultType = new FaultTypeRepository();
        DepartmentRepository _repoDepartment = new DepartmentRepository();
        ComplaintMainTypeRepository _repoComplaintMainType = new ComplaintMainTypeRepository();
        ComplaintSubTypeRepository _repoComplaintSubType = new ComplaintSubTypeRepository();
        // GET: Dempartment
        public ActionResult Index()
        {
            return View();
        }

        [RoleBasedAccessFilter(subModule = "FaultTypes", accessLevel = 1)]
        public ActionResult List()
        {
           
            ViewBag.FaultTypes = _repoFaultType.GetFaultTypeList();
            return View();
        }
        [RoleBasedAccessFilter(subModule = "FaultTypes", accessLevel = 2)]
        public ActionResult Add()
        {
            ViewBag.Departments = _repoDepartment.GetDepartmentList();
            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList();
            ViewBag.ComplaintSubTypes = _repoComplaintSubType.GetComplaintSubTypeList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(FaultType faultType)
        {
            
            int employeeId = (int)Session["employeeId"];

            var Departments = _repoDepartment.GetDepartmentList();
            faultType.Department = Departments.Where(x => x.Id.Equals(faultType.Dept_Id)).FirstOrDefault().Dept_Name;

            var ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList();
            faultType.MainType = ComplaintMainTypes.Where(x => x.Id.Equals(faultType.MainType_Id)).FirstOrDefault().MainType;

            var ComplaintSubTypes = _repoComplaintSubType.GetComplaintSubTypeList();
            faultType.SubType = ComplaintSubTypes.Where(x => x.Id.Equals(faultType.SubType_Id)).FirstOrDefault().SubType;

            faultType.CreatedOn = DateTime.Now;
            faultType.CreatedBy = employeeId;
            faultType.LastUpdatedOn = DateTime.Now;
            faultType.LastUpdatedBy = employeeId;
            int deptid = _repoFaultType.AddFaultType(faultType);
            if (deptid > 0)
            {
                return RedirectToAction("List", "FaultType", new { });
            }
            else
            {
                return RedirectToAction("List", "FaultType", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "FaultTypes", accessLevel = 3)]
        public ActionResult Edit(int id)
        {
            ViewBag.FaultType = _repoFaultType.GetFaultTypeById(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FaultType faultType)
        {
           
            int employeeId = (int)Session["employeeId"];

            var Departments = _repoDepartment.GetDepartmentList();
            faultType.Department = Departments.Where(x => x.Id.Equals(faultType.Dept_Id)).FirstOrDefault().Dept_Name;
            var ComplaintSubTypes = _repoComplaintSubType.GetComplaintSubTypeList();
            faultType.SubType = ComplaintSubTypes.Where(x => x.Id.Equals(faultType.SubType_Id)).FirstOrDefault().SubType;

            faultType.LastUpdatedOn = DateTime.Now;
            faultType.LastUpdatedBy = employeeId;
            int deptid = _repoFaultType.UpdateFaultType(faultType);
            if (deptid > 0)
            {

                return RedirectToAction("List", "FaultType", new { });
            }
            else
            {
                return RedirectToAction("List", "FaultType", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "FaultTypes", accessLevel = 4)]
        public ActionResult Delete(int id)
        {
            bool deletedDept = _repoFaultType.DeleteFaultType(id);
            return RedirectToAction("List", "FaultType", new { });
        }

        public ActionResult FaultTypeListBySubTypeId(int id)
        {
            ViewBag.FaultTypes = _repoFaultType.GetFaultTypeListBySubTypeId(id);
            return PartialView("PartialFaultTypesBySubType");
        }

    }
}