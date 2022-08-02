using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Models.StoredProcedures
{
    public class SP_Employees
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime HireDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public int Country_Id { get; set; }
        public int City_Id { get; set; }
        public Nullable<int> Manager_Id { get; set; }
    }
}