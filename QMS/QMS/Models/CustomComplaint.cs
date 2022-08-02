﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Models
{
    public class CustomComplaint
    {
        public int Id { get; set; }
        public string Issuer_Name { get; set; }
        public string IssuerDepartment { get; set; }
        public string ResponsibleDepartment { get; set; }
        public string ComplaintMainType { get; set; }

        public int ComplaintMainType_Id { get; set; }

        public string ComplaintSubTypeIds { get; set; }

        public string ComplaintSubTypes { get; set; }

    }
}