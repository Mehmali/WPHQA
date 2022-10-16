using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Models
{
    public class CustomComplaint
    {
        public int Id { get; set; }
        public string Issuer_Name { get; set; }

        public int IssuerDept_Id { get; set; }
        public string IssuerDepartment { get; set; }

        public int ResponsibleDept_Id { get; set; }
        public string ResponsibleDepartment { get; set; }
        public string ComplaintMainType { get; set; }

        public int ComplaintMainType_Id { get; set; }

        public string ComplaintSubType { get; set; }

        public int? ComplaintSubType_Id { get; set; }

        public string ComplaintFaultIds { get; set; }

        public string ComplaintFaults { get; set; }

        public int? Status { get; set; }

        public int? RiskAssessment { get; set; }

        public string CARCAPA { get; set; }
        public Boolean IsValid { get; set; }

        public Boolean IsClosed { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

    }
}