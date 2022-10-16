using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public class ComplaintsListParams
    {
        public int? CurrentState { get; set; }

        public string CARCAPA { get; set; }

        public string ResponsibleDepartment { get; set; }
    }
}