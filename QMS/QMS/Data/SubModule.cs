namespace QMS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setup.SubModules")]
    public partial class SubModule
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }
    }
}
