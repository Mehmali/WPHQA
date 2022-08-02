namespace QMS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ComplaintDocument
    {
        public int Id { get; set; }

        public int Complaint_Id { get; set; }

       public string File_Name { get; set; }

        public string File_Path { get; set; }

        public Boolean IsDeleted { get; set; }


        public DateTime? DeletedOn { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? UploadedOn { get; set; }

        public int? UploadedBy { get; set; }
        
    


    }
}
