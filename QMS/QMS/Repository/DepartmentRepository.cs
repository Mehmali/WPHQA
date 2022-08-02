using QMS.Data;
using QMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class DepartmentRepository
    {
        public int AddDepartment(Department department)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Departments.Add(department);
                    db.SaveChanges();
                    return department.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }

        }

        public int UpdateDepartment(Department department)
        {
            try
            {
                using(var db=new QAData())
                {
                    db.Entry(department).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return department.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public Department GetDepartmentById(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var department = new Department();
                    department = db.Departments.Find(id);
                    return department;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<Department> GetDepartmentList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var departments = db.Departments.ToList();
                    return departments;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool DeleteDepartment(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var department = db.Departments.Find(id);
                    db.Departments.Remove(department);
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}