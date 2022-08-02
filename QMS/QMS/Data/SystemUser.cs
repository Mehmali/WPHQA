namespace QMS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SystemUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Login_Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        public int Employee_Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Emp_Name { get; set; }

        public int Role_Id { get; set; }

        public bool? IsActive { get; set; }

        public int? DeActivatedBy { get; set; }

        public DateTime? DeActivatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public int? PasswordLastModifiedBy { get; set; }

        public DateTime? PasswordLastModifiedOn { get; set; }
    }
}
