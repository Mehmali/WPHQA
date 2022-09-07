namespace QMS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Dept_Code { get; set; }

        [Required]
        [StringLength(500)]
        public string Dept_Name { get; set; }

        public Boolean IsExternal { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdateOn { get; set; }

        [Required]
        [StringLength(1)]
        public string CompId { get; set; }
    }
}
