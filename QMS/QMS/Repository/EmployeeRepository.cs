using QMS.Data;
using QMS.Models.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class EmployeeRepository
    {
        public int AddEmployee(Employee employee)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return employee.Id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int UpdateEmployee(Employee employee)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return employee.Id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var employee = new Employee();
                    employee = db.Employees.Find(id);
                    return employee;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public Employee GetEmployeeByCode(string code)
        {
            try
            {
                using (var db = new QAData())
                {
                    var employee = new Employee();
                    employee = db.Employees.Where(e => e.Emp_Code.Equals(code)).FirstOrDefault();
                    return employee;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Employee> GetEmployeeList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var employees = db.Employees.ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var employee = db.Employees.Find(id);
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}