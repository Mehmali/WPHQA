namespace QMS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(5)]
        public string Emp_Code { get; set; }

        [Required]
        [StringLength(35)]
        public string Emp_Name { get; set; }

        public int? Manager_Id { get; set; }

        public int JobTitle_Id { get; set; }

        public int Department_Id { get; set; }


        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        [Required]
        [StringLength(1)]
        public string CompId { get; set; }
    }
}
