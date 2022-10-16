namespace QMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ComplaintById
    {
        public int Id { get; set; }

        public string Issuer_Name { get; set; }

        public string ComplaintNature { get; set; }

        public string ComplaintType { get; set; }

        public int IssuerDept_Id { get; set; }

        public string IssuerDepartment { get; set; }

        public string CustomerName { get; set; }

        public int ResponsibleDept_Id { get; set; }

        public string ResponsibleDepartment { get; set; }

        public string ProblemDescription { get; set; }

        public string ProblemArea { get; set; }

        public string ProblemReason { get; set; }

        public DateTime? ProblemDetectedOn { get; set; }

        public string ResponsiblePersonName { get; set; }

        public int? RiskAssessment { get; set; }

        public string CARCAPA { get; set; }

        public DateTime? ActionDueDate { get; set; }
        public string ActionsTaken { get; set; }

        public DateTime? ActionTakenOn { get; set; }

        public string ResponsibleDeptRemarks { get; set; }

        public int? CurrentState { get; set; }

        public string QARemarks { get; set; }

        public Boolean IsValid { get; set; }

        public Boolean IsClosed { get; set; }


        public DateTime? ClosedOn { get; set; }

        public int? ClosedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        
        public int ComplaintMainType_Id { get; set; }

        public string ComplaintMainType { get; set; }

        public int ComplaintSubType_Id { get; set; }

        public string ComplaintSubType { get; set; }


        public string ComplaintFaultIds { get; set; }


        public string ComplaintFaults { get; set; }

        public string[] NewComplaintFaultIds { get; set; }

      

        [NotMapped]
        public string ExistingComplaintFaultIds { get; set; }





    }
}
