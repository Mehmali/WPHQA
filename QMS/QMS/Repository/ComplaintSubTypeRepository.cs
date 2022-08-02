using QMS.Data;
using QMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class ComplaintSubTypeRepository
    {
        public int AddComplaintSubType(ComplaintSubType complaintSubType)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.ComplaintSubTypes.Add(complaintSubType);
                    db.SaveChanges();
                    return complaintSubType.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }

        }

        public int UpdateComplaintSubType(ComplaintSubType complaintSubType)
        {
            try
            {
                using(var db=new QAData())
                {
                    db.Entry(complaintSubType).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return complaintSubType.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public ComplaintSubType GetComplaintSubTypeById(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var complaintSubType = new ComplaintSubType();
                    complaintSubType = db.ComplaintSubTypes.Find(id);
                    return complaintSubType;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<ComplaintSubType> GetComplaintSubTypeList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var complaintSubTypes = db.ComplaintSubTypes.ToList();
                    return complaintSubTypes;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool DeleteComplaintSubType(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var complaintSubType = db.ComplaintSubTypes.Find(id);
                    db.ComplaintSubTypes.Remove(complaintSubType);
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<ComplaintSubType> GetComplaintSubTypeListByMainTypeId(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var complaintSubTypes = db.ComplaintSubTypes.Where(x => x.MainType_Id.Equals(id)).ToList();
                    return complaintSubTypes;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}