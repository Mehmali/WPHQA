namespace QMS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class JobTitle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string JobTitle_Code { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? LastUpdated { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        [Required]
        [StringLength(1)]
        public string CompId { get; set; }
    }
}
