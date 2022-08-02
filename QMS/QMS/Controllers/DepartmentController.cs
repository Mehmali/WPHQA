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


        public ActionResult ReportCall()
        {
            
            List<Department> Departments = _repoDepartment.GetDepartmentList();

            ReportDocument rd1 = new ReportDocument();

            List<CDepartment> cd = new List<CDepartment>();
            foreach (var d in Departments)
            {
                CDepartment cdept = new CDepartment();
                cdept.Code = d.Dept_Code;
                cdept.Name = d.Dept_Name;
                cd.Add(cdept);
            }

            rd1.Load(Server.MapPath("~/CrystalReports/DepartmentRptEF.rpt"));
            rd1.SetDataSource(cd);



            string filename1 = "Departments - " + DateTime.Now.Millisecond;
            string exportpath = Server.MapPath("~/DownloadedReports/");

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            System.IO.Stream stream = rd1.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return File(stream, "application/pdf", filename1 + ".pdf");
            //rd1.ExportToDisk(ExportFormatType.PortableDocFormat, exportpath + filename1 + ".pdf");
            //return View();
        }
    }
}