namespace QMS.CrystalReports
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CDepartment
    {
        public string Code { get; set; }

        public string Name { get; set; }

    }
}
