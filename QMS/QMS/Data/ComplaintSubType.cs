namespace QMS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ComplaintSubType
    {
        
        public int Id { get; set; }

       
        public int Dept_Id { get; set; }

       
        public int MainType_Id { get; set; }

        public string Department { get; set; }

        public string MainType { get; set; }

     
        public string SubType { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; }
    }
}
