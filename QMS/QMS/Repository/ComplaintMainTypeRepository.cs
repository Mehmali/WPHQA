
using QMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class ComplaintMainTypeRepository
    {
        public int AddComplaintMainType(ComplaintMainType complaintMainType)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.ComplaintMainTypes.Add(complaintMainType);
                    db.SaveChanges();
                    return complaintMainType.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }

        }

        public int UpdateComplaintMainType(ComplaintMainType complaintMainType)
        {
            try
            {
                using(var db=new QAData())
                {
                    db.Entry(complaintMainType).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return complaintMainType.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public ComplaintMainType GetComplaintMainTypeById(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var complaintMainType = new ComplaintMainType();
                    complaintMainType = db.ComplaintMainTypes.Find(id);
                    return complaintMainType;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<ComplaintMainType> GetComplaintMainTypeList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var complaintMainTypes = db.ComplaintMainTypes.ToList();
                    return complaintMainTypes;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<ComplaintMainType> GetComplaintMainTypeListByDepartmentId(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var complaintMainTypes = db.ComplaintMainTypes.Where(x=>x.Dept_Id.Equals(id)).ToList();
                    return complaintMainTypes;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public bool DeleteComplaintMainType(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var complaintMainType = db.ComplaintMainTypes.Find(id);
                    db.ComplaintMainTypes.Remove(complaintMainType);
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}