using QMS.Data;
using QMS.Filters;
using QMS.Models;
using QMS.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static QMS.Utilities.Enums;

namespace QMS.Controllers
{
    [CustomAuthorizeFilter]

    public class EmployeeController : Controller
    {
        
        EmployeeRepository _repoEmployee = new EmployeeRepository();
        
        
        JobTitleRepository _repoJobTitle = new JobTitleRepository();
        DepartmentRepository _repoDepartment = new DepartmentRepository();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult test()
        {
            return View();
        }

        [RoleBasedAccessFilter(subModule = "Employees", accessLevel = 1)]
        public ActionResult List(string sortOrder, string sortBy, int pageNumber = 1)
        {
            
            ViewBag.sortOrder = sortOrder;

            // ViewBag.Employees = _repoEmployee.GetEmployeeList(orgId);
            var Employees = _repoEmployee.GetEmployeeList();


            ViewBag.totalPages = Math.Ceiling(Employees.Count() / 5.0);
            ViewBag.pageNumber = pageNumber;
            Employees = Employees.Skip((pageNumber - 1) * 5).Take(5).ToList();



            switch (sortBy)
            {
                case "Emp_Code":
                    {
                        switch (sortOrder)
                        {
                            case "Asc":
                                {
                                    ViewBag.Employees = Employees.OrderBy(x => x.Emp_Code).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    ViewBag.Employees = Employees.OrderByDescending(x => x.Emp_Code).ToList();
                                    break;
                                }
                            default:
                                {
                                    ViewBag.Employees = Employees.OrderBy(x => x.Emp_Code).ToList();
                                    break;
                                }

                        }
                        break;
                    }
                case "Emp_Name":
                    {
                        switch (sortOrder)
                        {
                            case "Asc":
                                {
                                    ViewBag.Employees = Employees.OrderBy(x => x.Emp_Name).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    ViewBag.Employees = Employees.OrderByDescending(x => x.Emp_Name).ToList();
                                    break;
                                }
                            default:
                                {
                                    ViewBag.Employees = Employees.OrderBy(x => x.Emp_Name).ToList();
                                    break;
                                }

                        }
                        break;
                    }
                default:
                    {
                        ViewBag.Employees = Employees.OrderBy(x => x.Emp_Code).ToList();
                        break;
                    }

            }



            return View();
        }

        [RoleBasedAccessFilter(subModule = "Employees", accessLevel = 1)]
        [HttpPost]
        public ActionResult List(string search)
        {
           

            search = search.ToLower();
            ViewBag.Employees = _repoEmployee.GetEmployeeList();
            if (search != null)
            {
                ViewBag.Employees = _repoEmployee.GetEmployeeList().Where(e => e.Emp_Name.ToLower().Contains(search) || e.Emp_Code.Contains(search)).ToList();
            }
            var Employees = ViewBag.Employees;
            ViewBag.totalPages = 1.0;
            return View();
        }

        [RoleBasedAccessFilter(subModule = "Employees", accessLevel = 2)]
        public ActionResult Add()
        {
         
            ViewBag.Departments = _repoDepartment.GetDepartmentList();
           
            ViewBag.JobTitles = _repoJobTitle.GetJobTitleList();
         
           
            ViewBag.Managers = _repoEmployee.GetEmployeeList();

            return View();
        }

        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            
            int employeeId = (int)Session["employeeId"];
           
            employee.CreatedOn = DateTime.Now;
            employee.CreatedBy = employeeId;
            employee.LastUpdatedOn = DateTime.Now;
            employee.LastUpdatedBy = employeeId;


           
            int employeeid = _repoEmployee.AddEmployee(employee);
            if (employeeid > 0)
            {
                TempData["addSuccessMessage"] = "Record Saved Successfully";

                return RedirectToAction("List", "Employee", new { });
            }
            else
            {
                TempData["addErrorMessage"] = "Record not Saved";
                return RedirectToAction("List", "Employee", new { });
            }
        }

        [RoleBasedAccessFilter(subModule = "Employees", accessLevel = 3)]
        public ActionResult Edit(int id)
        {
           
           
           
            ViewBag.Employee = _repoEmployee.GetEmployeeById(id);
           
            ViewBag.JobTitles = _repoJobTitle.GetJobTitleList();
            ViewBag.Departments = _repoDepartment.GetDepartmentList();
            
            ViewBag.Managers = (_repoEmployee.GetEmployeeList()).OrderBy(x=>x.Emp_Name).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
          
            int employeeId = (int)Session["employeeId"];
           
            employee.LastUpdatedOn = DateTime.Now;
            employee.LastUpdatedBy = employeeId;

            int employeeid = _repoEmployee.UpdateEmployee(employee);
            if (employeeid > 0)
            {

                return RedirectToAction("List", "Employee", new { });
            }
            else
            {
                return RedirectToAction("List", "Employee", new { });
            }
        }

        [RoleBasedAccessFilter(subModule = "Employees", accessLevel = 4)]
        public ActionResult Delete(int id)
        {
            bool deletedEmployee = _repoEmployee.DeleteEmployee(id);
            return RedirectToAction("List", "Employee", new { });
        }


        public ActionResult Details(int id)
        {
           
            
            var Employee = _repoEmployee.GetEmployeeById(id);
           

            ViewBag.Employee = _repoEmployee.GetEmployeeById(id);
           
            ViewBag.jobTitle = _repoJobTitle.GetJobTitleList().Where(x => x.Id.Equals(Employee.JobTitle_Id)).FirstOrDefault().Title;
            ViewBag.Department = _repoDepartment.GetDepartmentList().Where(x => x.Id.Equals(Employee.Department_Id)).FirstOrDefault().Dept_Name;

            //ViewBag.JobTitle = jobTitles.Where(x => x.Id.Equals(Employee.JobTitle_Id)).FirstOrDefault().Title;
            //ViewBag.Countries = Countries.Where(x => x.Id.Equals(Employee.Country_Id)).FirstOrDefault().Name;
            //ViewBag.Department = Departments.Where(x => x.Id.Equals(Employee.Department_Id)).FirstOrDefault().Name;


            return View("Detail");
        }

        [HttpGet]
        public string Detail(string code)
        {

            var employee = _repoEmployee.GetEmployeeByCode(code);
            CustomEmployee cemployee = new CustomEmployee();
            cemployee.FirstName = employee.Emp_Name;

            cemployee.JobTitle = employee.JobTitle_Id.ToString();
            cemployee.Department = employee.Department_Id.ToString();

            return JsonConvert.SerializeObject(cemployee);

        }

    }
}