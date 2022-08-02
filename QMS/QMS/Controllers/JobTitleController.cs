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
    public class JobTitleController : Controller
    {
        JobTitleRepository _repoJobTitle = new JobTitleRepository();

        [RoleBasedAccessFilter(subModule = "Job Titles", accessLevel = 1)]
        public ActionResult List()
        {
          
            ViewBag.JobTitles = _repoJobTitle.GetJobTitleList();
            return View();
        }
        [RoleBasedAccessFilter(subModule = "Job Titles", accessLevel = 2)]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [RoleBasedAccessFilter(subModule = "Job Titles", accessLevel = 3)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.JobTitle = _repoJobTitle.GetJobTitleById(id);
            return View();
        }

        [HttpPost]
        public ActionResult Add(JobTitle jobTitle)
        {
           
            int employeeId = (int)Session["employeeId"];
            
            jobTitle.CreatedOn = DateTime.Now;
            jobTitle.CreatedBy = employeeId;
            jobTitle.LastUpdatedOn = DateTime.Now;
            jobTitle.LastUpdated = employeeId;
            ViewBag.JobTitles = _repoJobTitle.AddJobTitle(jobTitle);
            return RedirectToAction("List", "JobTitle", new { });
        }

        [HttpPost]
        public ActionResult Edit(JobTitle jobTitle)
        {
            
            int employeeId = (int)Session["employeeId"];
            
            jobTitle.LastUpdatedOn = DateTime.Now;
            jobTitle.LastUpdated = employeeId;
            ViewBag.JobTitles = _repoJobTitle.UpdateJobTitle(jobTitle);
            return RedirectToAction("List", "JobTitle", new { });
        }
        [RoleBasedAccessFilter(subModule = "Job Titles", accessLevel = 4)]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _repoJobTitle.DeleteobTitleById(id);
            return RedirectToAction("List", "JobTitle", new { });
        }
    }
}