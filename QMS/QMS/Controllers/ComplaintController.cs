using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using HRMS.Models;
using QMS.CrystalReports;
using QMS.Data;
using QMS.Filters;
using QMS.Models;
using QMS.Repository;
using QMS.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static QMS.Utilities.Enums;

namespace QMS.Controllers
{
    [CustomAuthorizeFilter]
    public class ComplaintController : Controller
    {
        ComplaintRepository _repoComplaint = new ComplaintRepository();
        DepartmentRepository _repoDepartment = new DepartmentRepository();
        ComplaintMainTypeRepository _repoComplaintMainType = new ComplaintMainTypeRepository();
        ComplaintSubTypeRepository _repoComplaintSubType = new ComplaintSubTypeRepository();
        FaultTypeRepository _repoFaultType = new FaultTypeRepository();
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

        [RoleBasedAccessFilter(subModule = "QualityAssurance", accessLevel = 1)]
        //public ActionResult List(int? id)
        public ActionResult List()
        {
            FilterList(new ComplaintsListParams());
            //var complaintsList = _repoComplaint.GetComplaintListBySP();
            //ViewBag.Complaints = complaintsList;
            ////if (id != null)
            ////{
            ////    ViewBag.Complaints = complaintsList.Where(c => c.Status == id).ToList();
            ////    ViewBag.Status = id;
            ////}
            //ViewBag.Departments = _repoDepartment.GetDepartmentList().ToList();
            //ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();

            //TempData["Type"] = "All";
            return View();
        }

        [HttpPost]
        [RoleBasedAccessFilter(subModule = "QualityAssurance", accessLevel = 1)]
        public ActionResult List(ComplaintsListParams filtervalues)
        {
            FilterList(filtervalues);
            return View();
        }

        //public ActionResult PendingComplaintsList()
        //{

        //    ViewBag.Complaints = _repoComplaint.GetComplaintListBySP();
        //    ViewBag.Departments = _repoDepartment.GetDepartmentList().ToList();
        //    ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();

        //    TempData["Type"] = "All";
        //    return View();
        //}

        public ActionResult ComplaintReceivedList()
        {
            int employeeId = (int)Session["employeeId"];
            var Employee = _repoEmployee.GetEmployeeById(employeeId);
            ViewBag.Employee = Employee;
            var Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.Departments = Departments;
            //ViewBag.EmployeeDepartment = Departments.Where(x => x.Id.Equals(Employee.Department_Id)).FirstOrDefault().Dept_Name;
            var Complaints = _repoComplaint.GetComplaintListBySP();
            ViewBag.Complaints = Complaints.Where(x => x.ResponsibleDept_Id.Equals(Employee.Department_Id)).ToList();

            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();

            TempData["Type"] = "Received";
            return View();
        }

        public ActionResult ComplaintIssuedList()
        {
            int employeeId = (int)Session["employeeId"];
            var Employee = _repoEmployee.GetEmployeeById(employeeId);
            ViewBag.Employee = Employee;
            var Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.Departments = Departments;
            //ViewBag.EmployeeDepartment = Departments.Where(x => x.Id.Equals(Employee.Department_Id)).FirstOrDefault().Dept_Name;
            var Complaints = _repoComplaint.GetComplaintListBySP();
            ViewBag.Complaints = Complaints.Where(x => x.IssuerDept_Id.Equals(Employee.Department_Id)).ToList();

            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();
            TempData["Type"] = "Issued";

            return View();
        }

        public ActionResult ExternalComplaintsList()
        {
            //int employeeId = (int)Session["employeeId"];
            //var Employee = _repoEmployee.GetEmployeeById(employeeId);
            //ViewBag.Employee = Employee;
            var Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.Departments = Departments;
            //ViewBag.EmployeeDepartment = Departments.Where(x => x.Id.Equals(Employee.Department_Id)).FirstOrDefault().Dept_Name;
            var Complaints = _repoComplaint.GetComplaintListBySP();
            ViewBag.Complaints = Complaints.Where(x => x.ResponsibleDept_Id.Equals(17)).ToList();

            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();
            TempData["Type"] = "External";

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
            var Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.Departments = Departments.Where(x => x.IsExternal.Equals(false));
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
            complaint.ActionDueDate = DateTime.Now.AddDays(10).Date;
            complaint.CreatedOn = DateTime.Now;
            complaint.CreatedBy = employeeId;
            complaint.UpdatedOn = DateTime.Now;
            complaint.UpdatedBy = employeeId;
            int deptid = _repoComplaint.AddComplaint(complaint);
            if (deptid > 0)
            {
                var Employees = _repoEmployee.GetEmployeeList().Where(x => x.Department_Id.Equals(complaint.IssuerDept_Id) || x.Department_Id.Equals(complaint.ResponsibleDept_Id)).ToList();
                string[] EmailAddresses = Employees.Select(x => x.Email).ToArray();
                if (EmailAddresses.Count() < 1)
                {
                    Employees = _repoEmployee.GetEmployeeList().Where(x => x.Id.Equals(employeeId)).ToList();
                    EmailAddresses = Employees.Select(x => x.Email).ToArray();
                }
                EmailUtility emailUtility = new EmailUtility();
                emailUtility.SendEmail(EmailAddresses);
                return RedirectToAction("ComplaintIssuedList", "Complaint", new { });
            }
            else
            {
                return RedirectToAction("ComplaintIssuedList", "Complaint", new { });
            }
        }
        [RoleBasedAccessFilter(subModule = "Complaints", accessLevel = 3)]
        public ActionResult Edit(int id)
        {
            int employeeId = (int)Session["employeeId"];
            ViewBag.RoleId = (int)Session["Role_Id"];
            var Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.Departments = Departments;
            //ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();

            var Complaint = _repoComplaint.GetComplaintByIdSP(id);
            //var ComplaitSubTypeIds = Complaint.ComplaintSubTypeIds.Split(',');
            ViewBag.Complaint = Complaint;
            //ViewBag.ComplaintSubTypes = _repoComplaintSubType.GetComplaintSubTypeListByMainTypeId(Complaint.ComplaintMianType_Id);
            ViewBag.ComplaintFaults = _repoFaultType.GetFaultTypeListBySubTypeId(Complaint.ComplaintSubType_Id);
            TempData.Keep("Type");

            return View();
        }

        [HttpPost]
        public ActionResult Edit(ComplaintById complaintById)
        {

            int employeeId = (int)Session["employeeId"];

            var Departments = _repoDepartment.GetDepartmentList();

            string[] existingcomplaintFaultIds = complaintById.ExistingComplaintFaultIds != null ? complaintById.ExistingComplaintFaultIds.Split(',').Select(x => x.Trim()).ToArray() : new string[0];
            string[] complaintFaultIds = complaintById.NewComplaintFaultIds != null ? complaintById.NewComplaintFaultIds : new string[0];
            var removeableComplaintDefects = existingcomplaintFaultIds != null ? existingcomplaintFaultIds.Except(complaintFaultIds).ToArray() : null;
            var addableComplaintDefects = complaintFaultIds != null ? complaintFaultIds.Except(existingcomplaintFaultIds).ToArray() : null;


            Complaint complaint = new Complaint();

            complaint.Id = complaintById.Id;
            complaint.IsClosed = Convert.ToBoolean(complaintById.IsClosed);

            complaint.IssuerDepartment = complaintById.IssuerDepartment;
            complaint.IssuerDept_Id = complaintById.IssuerDept_Id;
            complaint.ComplaintNature = complaintById.ComplaintNature;
            complaint.ComplaintType = complaintById.ComplaintType;
            complaint.Issuer_Name = complaintById.Issuer_Name;
            complaint.CustomerName = complaintById.CustomerName;
            complaint.ProblemArea = complaintById.ProblemArea;
            complaint.ProblemDescription = complaintById.ProblemDescription;
            complaint.ProblemReason = complaintById.ProblemReason;
            complaint.ProblemDetectedOn = complaintById.ProblemDetectedOn;
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
            complaint.IsValid = Convert.ToBoolean(complaintById.IsValid);
            complaint.ClosedBy = complaintById.ClosedBy;
            complaint.ClosedOn = complaintById.ClosedOn;
            complaint.ComplaintMainType_Id = complaintById.ComplaintMainType_Id;
            complaint.ComplaintMainType = complaintById.ComplaintMainType;
            complaint.ComplaintSubType_Id = complaintById.ComplaintSubType_Id;
            complaint.ComplaintSubType = complaintById.ComplaintSubType;
            complaint.CreatedOn = complaintById.CreatedOn;
            complaint.CreatedBy = complaintById.CreatedBy;
            complaint.CurrentState = complaintById.CurrentState;
            complaint.UpdatedOn = DateTime.Now;
            complaint.UpdatedBy = complaintById.UpdatedBy;


            int deptid = _repoComplaint.UpdateComplaint(complaint);
            if (deptid > 0)
            {
                //var MaintTypes = _repoComplaintMainType.GetComplaintMainTypeList();
                //var SubTypes = _repoComplaintSubType.GetComplaintSubTypeList();
                var Faults = _repoFaultType.GetFaultTypeList();
                if (addableComplaintDefects != null)
                {


                    List<ComplaintDefect> defects = new List<ComplaintDefect>();


                    foreach (string complaintFaultId in addableComplaintDefects)
                    {
                        ComplaintDefect defect = new ComplaintDefect();
                        defect.ComplaintMainType_Id = complaint.ComplaintMainType_Id;
                        defect.ComplaintSubType_Id = complaint.ComplaintSubType_Id;
                        defect.ComplaintFault_Id = Int32.Parse(complaintFaultId);
                        defect.Complaint_Id = complaint.Id;
                        defect.ComplaintMainType = complaint.ComplaintMainType;
                        defect.ComplaintSubType = complaint.ComplaintSubType;
                        defect.ComplaintFault = Faults.Where(x => x.Id.Equals(defect.ComplaintFault_Id)).FirstOrDefault().Fault;
                        defect.CreatedOn = DateTime.Now;
                        defect.CreatedBy = complaint.CreatedBy;
                        defect.UpdatedOn = DateTime.Now;
                        defect.UpdatedBy = complaint.CreatedBy;
                        defects.Add(defect);
                    }
                    _repoComplaint.AddComplaintDefects(defects);
                }

                if (removeableComplaintDefects != null)
                {
                    List<ComplaintDefect> removeableDefects = new List<ComplaintDefect>();
                    foreach (string complaintFaultId in removeableComplaintDefects)
                    {
                        ComplaintDefect defect = new ComplaintDefect();
                        defect.ComplaintMainType_Id = complaint.ComplaintMainType_Id;
                        defect.ComplaintSubType_Id = complaint.ComplaintSubType_Id;
                        defect.ComplaintFault_Id = Int32.Parse(complaintFaultId);
                        defect.Complaint_Id = complaint.Id;

                        removeableDefects.Add(defect);
                    }
                    _repoComplaint.RemoveComplaintDefects(removeableDefects);
                }
                if (TempData["Type"] == "All")
                    return RedirectToAction("List", "Complaint", new { });
                else if (TempData["Type"] == "Issued")
                    return RedirectToAction("ComplaintIssuedList", "Complaint", new { });
                else if (TempData["Type"] == "External")
                    return RedirectToAction("ExternalComplaintsList", "Complaint", new { });
                else
                    return RedirectToAction("ComplaintReceivedList", "Complaint", new { });
            }
            else
            {
                if (TempData["Type"] == "All")
                    return RedirectToAction("List", "Complaint", new { });
                else if (TempData["Type"] == "Issued")
                    return RedirectToAction("ComplaintIssuedList", "Complaint", new { });
                else if (TempData["Type"] == "External")
                    return RedirectToAction("ExternalComplaintsList", "Complaint", new { });
                else
                    return RedirectToAction("ComplaintReceivedList", "Complaint", new { });
            }
        }
        //[RoleBasedAccessFilter(subModule = "Complaints", accessLevel = 4)]
        //public ActionResult Delete(int id)
        //{
        //    bool deletedDept = _repoComplaint.DeleteComplaint(id);
        // return RedirectToAction("List", "Complaint", new { });
        //}

        [RoleBasedAccessFilter(subModule = "Complaints", accessLevel = 1)]
        public ActionResult ViewDetail(int id)
        {
            int employeeId = (int)Session["employeeId"];
            var Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.Departments = Departments;
            //ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();

            var Complaint = _repoComplaint.GetComplaintByIdSP(id);
            //var ComplaitSubTypeIds = Complaint.ComplaintSubTypeIds.Split(',');
            ViewBag.Complaint = Complaint;
            ViewBag.ComplaintSubTypes = _repoComplaintSubType.GetComplaintSubTypeListByMainTypeId(Complaint.ComplaintMainType_Id);
            ViewBag.ComplaintDocuments = _repoComplaintDocument.GetComplaintDocumentList(id);

            TempData.Keep("Type");

            return View();
        }

        [HttpPost]
        public Boolean ChangeStatus(int id, ComplaintStatus status)
        {
            //var status = ComplaintStatus.Pending;
            bool result = _repoComplaint.ChangeStatus(id, status);
            return result;
        }

        public void FilterList(ComplaintsListParams complaintFilters)
        {
            var complaintsList = _repoComplaint.GetComplaintListBySP();
            ViewBag.Complaints = complaintsList;
            if (complaintFilters.CurrentState != null)
            {
                ViewBag.Complaints = complaintsList.Where(c => c.Status == complaintFilters.CurrentState).ToList();
                ViewBag.Status = complaintFilters.CurrentState;
            }
            ViewBag.Departments = _repoDepartment.GetDepartmentList().ToList();
            ViewBag.ComplaintMainTypes = _repoComplaintMainType.GetComplaintMainTypeList().ToList();

            TempData["Type"] = "All";
        }

        public Boolean test()
        {
            ServiceActions sa = new ServiceActions();
            sa.ComplaintDueAlerts();
            return true;
        }




    }
}