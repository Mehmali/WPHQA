using QMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Utilities
{
    public class ServiceActions
    {
        public void ComplaintDueAlerts()
        {
            EmployeeRepository _repoEmployee = new EmployeeRepository();
            ComplaintRepository _repoComplaint = new ComplaintRepository();
           var Complaints= _repoComplaint.GetOpenComplaintsListByDate(DateTime.Now.Date);
            if (Complaints !=null && Complaints.Count() > 0)
            {
                foreach(var complaint in Complaints)
                {
                    var Employees = _repoEmployee.GetEmployeeList().Where(x => x.Department_Id.Equals(complaint.IssuerDept_Id) || x.Department_Id.Equals(complaint.ResponsibleDept_Id)).ToList();
                    string[] EmailAddresses = Employees.Select(x => x.Email).ToArray();
                    if (EmailAddresses.Count() < 1)
                    {
                        Employees = _repoEmployee.GetEmployeeList().Where(x => x.Id.Equals(complaint.CreatedBy)).ToList();
                        EmailAddresses = Employees.Select(x => x.Email).ToArray();
                    }
                    EmailUtility emailUtility = new EmailUtility();
                    emailUtility.SendEmail(EmailAddresses);
                }
            }

          


        }

    }
}