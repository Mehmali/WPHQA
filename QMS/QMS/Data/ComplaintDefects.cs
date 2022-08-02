namespace QMS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ComplaintDefect
    {
        public int Id { get; set; }

       public int Complaint_Id { get; set; }

        public int ComplaintMainType_Id { get; set; }

        public string ComplaintMainType { get; set; }

        public int ComplaintSubType_Id { get; set; }

        public string ComplaintSubType { get; set; }


       
        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }


    }
}
