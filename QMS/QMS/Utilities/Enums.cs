using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QMS.Utilities
{
    public class Enums
    {
        public enum Gender
        {
            Male = 1,
            Female = 2,
            BiGender = 3
        }

      

        public enum Company
        {
            Spinning = 1,
            Weaving = 2,
            Finishing = 3,
            Fabrication = 4
        }

        public enum ComplaintStatus
        {
            Open = 1,
            Pending = 2,
            Closed = 3
        }

      
    }
}