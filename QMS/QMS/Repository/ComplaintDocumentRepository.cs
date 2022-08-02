using QMS.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class ComplaintDocumentRepository
    {
        public int AddComplaintDocument(ComplaintDocument complaintDocument)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.ComplaintDocuments.Add(complaintDocument);
                    db.SaveChanges();
                    return complaintDocument.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }

        }

        public int UpdateComplaintDocument(ComplaintDocument complaintDocument)
        {
            try
            {
                using(var db=new QAData())
                {
                    db.Entry(complaintDocument).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return complaintDocument.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public ComplaintDocument GetComplaintDocumentById(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var complaintDocument = new ComplaintDocument();
                    complaintDocument = db.ComplaintDocuments.Find(id);
                    return complaintDocument;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<ComplaintDocument> GetComplaintDocumentList(int Complaint_Id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var complaintDocuments = db.ComplaintDocuments.Where(x=>x.Complaint_Id.Equals(Complaint_Id)).ToList();
                    return complaintDocuments;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool DeleteComplaintDocument(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var complaintDocument = db.ComplaintDocuments.Find(id);
                    db.ComplaintDocuments.Remove(complaintDocument);
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