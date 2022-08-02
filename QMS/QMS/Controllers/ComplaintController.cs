using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using QMS.CrystalReports;
using QMS.Data;
using QMS.Filters;
using QMS.Models;
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
    public class ComplaintController : Controller
    {
        ComplaintRepository _repoComplaint = new ComplaintRepository();
        DepartmentRepository _repoDepartment = new DepartmentRepository();
        ComplaintMainTypeRepository _repoComplaintMainType = new ComplaintMainTypeRepository();
        ComplaintSubTypeRepository _repoComplaintSubType = new ComplaintSubTypeRepository();
        EmployeeRepository _repoEmployee = new EmployeeRepository();
        ComplaintDocumentRepository _repoComplaintDocument = new ComplaintDocumentRepository();
        // GET: Dempartment
        public ActionResult Index()
        {
            return View();
        }

        [RoleBasedAccessFilter(subModule = "Complaints", accessLevel = 1)]
        public ActionResult ListMain()
        {
           
            ViewBag.Complaints = _repoComplaint.GetComplaintList();
            ViewBag.Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();
            return View();
        }

        public ActionResult List()
        {

            ViewBag.Complaints = _repoComplaint.GetComplaintListBySP();
            ViewBag.Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();
            return View();
        }

        //public ActionResult ComplaintListByDepartment(int id)
        //{
        //    ViewBag.Complaints = _repoComplaint.GetComplaintListByDepartmentId(id);
        //    return PartialView("PartialComplaintsByDepartment");
        //}

        [RoleBasedAccessFilter(subModule = "Complaints", accessLevel = 2)]
        public ActionResult Add()
        {
            int employeeId = (int)Session["employeeId"];
            var Departments= _repoDepartment.GetDepartmentList().ToList();
            ViewBag.Departments = Departments;
            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();
            var Employee = _repoEmployee.GetEmployeeById(employeeId);
            ViewBag.Employee = Employee;
            ViewBag.EmployeeDepartment = Departments.Where(x => x.Id.Equals(Employee.Department_Id)).FirstOrDefault().Dept_Name;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Complaint complaint)
        {
            
            int employeeId = (int)Session["employeeId"];
            var Departments = _repoDepartment.GetDepartmentList();
            complaint.IssuerDepartment = Departments.Where(x => x.Id.Equals(complaint.IssuerDept_Id)).FirstOrDefault().Dept_Name;
            complaint.ResponsibleDepartment = Departments.Where(x => x.Id.Equals(complaint.ResponsibleDept_Id)).FirstOrDefault().Dept_Name;
            complaint.CreatedOn = DateTime.Now;
            complaint.CreatedBy = employeeId;
            complaint.UpdatedOn = DateTime.Now;
            complaint.UpdatedBy = employeeId;
            int deptid = _repoComplaint.AddComplaint(complaint);
            if (deptid > 0)
            {
                return RedirectToAction("List", "Complaint", new { });
            }
            else
            {
                return RedirectToAction("List", "Complaint", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "Complaints", accessLevel = 3)]
        public ActionResult Edit(int id)
        {
            int employeeId = (int)Session["employeeId"];
            var Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.Departments = Departments;
            //ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();
            
            var Complaint = _repoComplaint.GetComplaintByIdSP(id);
           //var ComplaitSubTypeIds = Complaint.ComplaintSubTypeIds.Split(',');
            ViewBag.Complaint = Complaint;
            ViewBag.ComplaintSubTypes = _repoComplaintSubType.GetComplaintSubTypeListByMainTypeId(Complaint.ComplaintMianType_Id);

            return View();
        }

        [HttpPost]
        public ActionResult Edit(ComplaintById complaintById)
        {
           
            int employeeId = (int)Session["employeeId"];

            var Departments = _repoDepartment.GetDepartmentList();
            string[] existingcomplaintSubTypeIds = complaintById.ExistingComplaintSubTypeIds.Split(',').Select(x=>x.Trim()).ToArray();
            string[] complaintSubTypeIds = complaintById.NewComplaintSubTypeIds;
            var removeableComplaintDefects = existingcomplaintSubTypeIds.Except(complaintSubTypeIds).ToArray();
            var addableComplaintDefects = complaintSubTypeIds.Except(existingcomplaintSubTypeIds).ToArray();
          

            Complaint complaint = new Complaint();

            complaint.Id = complaintById.Id;
            complaint.IsClosed = Convert.ToBoolean(complaintById.IsClosed);
            complaint.IssuerDepartment = complaintById.IssuerDepartment;
            complaint.IssuerDept_Id = complaintById.IssuerDept_Id;
            complaint.Issuer_Name = complaintById.Issuer_Name;
            complaint.ProblemArea = complaintById.ProblemArea;
            complaint.ProblemDescription = complaintById.ProblemDescription;
            complaint.ProblemReason = complaintById.ProblemReason;
            complaint.QARemarks = complaintById.QARemarks;
            complaint.ResponsibleDepartment = complaintById.ResponsibleDepartment;
            complaint.ResponsibleDeptRemarks = complaintById.ResponsibleDeptRemarks;
            complaint.ResponsibleDept_Id = complaintById.ResponsibleDept_Id;
            complaint.ResponsiblePersonName = complaintById.ResponsiblePersonName;
            complaint.RiskAssessment = complaintById.RiskAssessment;
            complaint.ActionDueDate = complaintById.ActionDueDate;
            complaint.ActionsTaken = complaintById.ActionsTaken;
            complaint.ActionTakenOn = complaintById.ActionTakenOn;
            complaint.CARCAPA = complaintById.CARCAPA;
            complaint.ClosedBy = complaintById.ClosedBy;
            complaint.ClosedOn = complaintById.ClosedOn;
            complaint.ComplaintMianType_Id = complaintById.ComplaintMianType_Id;
            
            complaint.CreatedOn = complaintById.CreatedOn;
            complaint.CreatedBy = complaintById.CreatedBy;
            complaint.CurrentState = complaintById.CurrentState;
            complaint.UpdatedOn = DateTime.Now;
            complaint.UpdatedBy = complaintById.UpdatedBy;

      
            int deptid = _repoComplaint.UpdateComplaint(complaint);
            if (deptid > 0)
            {
                var MaintTypes = _repoComplaintMainType.GetComplaintMainTypeList();
                var SubTypes = _repoComplaintSubType.GetComplaintSubTypeList();
                List<ComplaintDefect> defects = new List<ComplaintDefect>();
                foreach (string complaintSubTypeId in addableComplaintDefects)
                {
                    ComplaintDefect defect = new ComplaintDefect();
                    defect.ComplaintMainType_Id = complaint.ComplaintMianType_Id;
                    defect.ComplaintSubType_Id = Int32.Parse(complaintSubTypeId);
                    defect.Complaint_Id = complaint.Id;
                    defect.ComplaintMainType = MaintTypes.Where(x => x.Id.Equals(defect.ComplaintMainType_Id)).FirstOrDefault().MainType;
                    defect.ComplaintSubType = SubTypes.Where(x => x.Id.Equals(defect.ComplaintSubType_Id)).FirstOrDefault().SubType;
                    defect.CreatedOn = DateTime.Now;
                    defect.CreatedBy = 0;
                    defects.Add(defect);
                }
                _repoComplaint.AddComplaintDefects(defects);

                List<ComplaintDefect> removeableDefects = new List<ComplaintDefect>();
                foreach (string complaintSubTypeId in removeableComplaintDefects)
                {
                    ComplaintDefect defect = new ComplaintDefect();
                    defect.ComplaintMainType_Id = complaint.ComplaintMianType_Id;
                    defect.ComplaintSubType_Id = Int32.Parse(complaintSubTypeId);
                    defect.Complaint_Id = complaint.Id;

                    removeableDefects.Add(defect);
                }
                _repoComplaint.RemoveComplaintDefects(removeableDefects);
                return RedirectToAction("List", "Complaint", new { });
            }
            else
            {
                return RedirectToAction("List", "Complaint", new { });
            }
        }
        //[RoleBasedAccessFilter(subModule = "Complaints", accessLevel = 4)]
        //public ActionResult Delete(int id)
        //{
        //    bool deletedDept = _repoComplaint.DeleteComplaint(id);
        // return RedirectToAction("List", "Complaint", new { });
        //}


        public ActionResult ViewDetail(int id)
        {
            int employeeId = (int)Session["employeeId"];
            var Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.Departments = Departments;
            //ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();

            var Complaint = _repoComplaint.GetComplaintByIdSP(id);
            //var ComplaitSubTypeIds = Complaint.ComplaintSubTypeIds.Split(',');
            ViewBag.Complaint = Complaint;
            ViewBag.ComplaintSubTypes = _repoComplaintSubType.GetComplaintSubTypeListByMainTypeId(Complaint.ComplaintMianType_Id);
            ViewBag.ComplaintDocuments = _repoComplaintDocument.GetComplaintDocumentList(id);

            return View();
        }

    }
}