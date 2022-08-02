
using QMS.Data;
using QMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class ComplaintRepository
    {
        public int AddComplaint(Complaint complaint)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Complaints.Add(complaint);
                    db.SaveChanges();
                    int complaintId = complaint.Id;
                    if (complaintId > 0)
                    {
                        ComplaintMainTypeRepository _repoComplaintMainType = new ComplaintMainTypeRepository();
                        ComplaintSubTypeRepository _repoComplaintSubType = new ComplaintSubTypeRepository();
                        var MaintTypes = _repoComplaintMainType.GetComplaintMainTypeList();
                        var SubTypes = _repoComplaintSubType.GetComplaintSubTypeList();
                        List<ComplaintDefect> defects = new List<ComplaintDefect>();
                        foreach(int complaintSubTypeId in complaint.ComplaintSubTypeIds)
                        {
                            ComplaintDefect defect = new ComplaintDefect();
                            defect.ComplaintMainType_Id = complaint.ComplaintMianType_Id;
                            defect.ComplaintSubType_Id = complaintSubTypeId;
                            defect.Complaint_Id = complaintId;
                            defect.ComplaintMainType = MaintTypes.Where(x => x.Id.Equals(defect.ComplaintMainType_Id)).FirstOrDefault().MainType;
                            defect.ComplaintSubType = SubTypes.Where(x => x.Id.Equals(defect.ComplaintSubType_Id)).FirstOrDefault().SubType;
                            defect.CreatedOn = DateTime.Now;
                            defect.CreatedBy = 0;
                            defects.Add(defect);
                        }
                        AddComplaintDefects(defects);
                    }
                    return complaintId;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateComplaint(Complaint complaint)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Entry(complaint).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return complaint.Id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool AddComplaintDefects(List<ComplaintDefect> complaintDefects)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.ComplaintDefects.AddRange(complaintDefects);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool RemoveComplaintDefects(List<ComplaintDefect> complaintDefects)
        {
            try
            {
                using (var db = new QAData())
                {
                    var ComplaintDefects = db.ComplaintDefects.ToList();
                    var removableComplaintDefects = ComplaintDefects.Where(x => complaintDefects.Any(y => y.Complaint_Id == x.Complaint_Id && y.ComplaintSubType_Id == x.ComplaintSubType_Id)).ToList();
                    db.ComplaintDefects.RemoveRange(removableComplaintDefects);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        public List<Complaint> GetComplaintList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var complaints = db.Complaints.ToList();
                    return complaints;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<CustomComplaint> GetComplaintListBySP()
        {
            try
            {
                using (var db = new QAData())
                {
                    //var sqlParam = new SqlParameter("@Manager_Id", id);
                   
                    var complaints = db.Database.SqlQuery<CustomComplaint>("SP_ComplaintList").ToList();
                    return complaints;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ComplaintById GetComplaintByIdSP(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var sqlParam = new SqlParameter("@ComplaintId", id);

                    var complaint = db.Database.SqlQuery<ComplaintById>("SP_ComplaintById @ComplaintId", sqlParam).FirstOrDefault();
                    return complaint;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}