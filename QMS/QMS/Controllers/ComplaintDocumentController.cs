using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using QMS.CrystalReports;
using QMS.Data;
using QMS.Filters;
using QMS.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace QMS.Controllers
{
    [CustomAuthorizeFilter]
    public class ComplaintDocumentController : Controller
    {
        ComplaintDocumentRepository _repoComplaintDocument = new ComplaintDocumentRepository();


    [HttpPost]
    public ActionResult UploadFile(HttpPostedFileBase myFile, Int32 ComplaintId)
        {
            int employeeId = (int)Session["employeeId"];
            var fileName = myFile.FileName;
            var filePath = Server.MapPath("~/UploadedFiles") +"/"+ fileName;
            myFile.SaveAs(filePath);
            var complaintDocument = new ComplaintDocument();
            complaintDocument.Complaint_Id = ComplaintId;
            complaintDocument.File_Name = fileName;
            complaintDocument.File_Path = filePath;
            complaintDocument.UploadedBy = employeeId;
            complaintDocument.UploadedOn = DateTime.Now;
            _repoComplaintDocument.AddComplaintDocument(complaintDocument);
            return RedirectToAction("ViewDetail", "Complaint",new {id= complaintDocument.Complaint_Id });
        }
 


        [HttpPost]
        public ActionResult Add(ComplaintDocument complaintDocument)
        {
            
            int employeeId = (int)Session["employeeId"];
            
            complaintDocument.UploadedOn = DateTime.Now;
            complaintDocument.UploadedBy = employeeId;
          
            int deptid = _repoComplaintDocument.AddComplaintDocument(complaintDocument);
            if (deptid > 0)
            {
                return RedirectToAction("ViewDetail", "Complaint", new {id=complaintDocument.Complaint_Id });
            }
            else
            {
                return RedirectToAction("ViewDetail", "Complaint", new { complaintDocument.Complaint_Id });
            }
        }
       
        //[RoleBasedAccessFilter(subModule = "ComplaintDocuments", accessLevel = 4)]
        public ActionResult Delete(int id)
        {
            var document = _repoComplaintDocument.GetComplaintDocumentById(id);
            var FileName = document.File_Name;
            var Complaint_Id = document.Complaint_Id;
            bool deletedDocument = _repoComplaintDocument.DeleteComplaintDocument(id);
            if (deletedDocument)
            {
                string Path = Server.MapPath("~/UploadedFiles/" + FileName);
                if (System.IO.File.Exists(Path))
                {
                    System.IO.File.Delete(Path);
                }
            }
            return RedirectToAction("ViewDetail", "Complaint", new { id = Complaint_Id });
        }

        
        //[HttpGet]
        //public void DownloadFile(int id)
        //{
        //    var document = _repoComplaintDocument.GetComplaintDocumentById(id);
        //    string Path = Server.MapPath("~/UploadedFiles/" + document.File_Name);
        //    //WebClient webClient = new WebClient();
           
        //    //webClient.DownloadFile(Path,document.File_Name);

        //    WebClient req = new WebClient();
        //    HttpResponse response = HttpContext.;
        //    string filePath = Path;
        //    response.Clear();
        //    response.ClearContent();
        //    response.ClearHeaders();
        //    response.Buffer = true;
        //    response.AddHeader("Content-Disposition", "attachment;filename=Filename.extension");
        //    byte[] data = req.DownloadData(Server.MapPath(filePath));
        //    response.BinaryWrite(data);
        //    response.End();


        //}

        public ActionResult DownloadFile(int id)        {
            var document = _repoComplaintDocument.GetComplaintDocumentById(id);
            string FileName = document.File_Name;
            if (!string.IsNullOrEmpty(FileName))            {                Session["Message"] = "";                //string FileName = "";                string FPath = "";                //FileName = FilePath.Split('-')[1];                FPath = Server.MapPath("~/UploadedFiles\\") + FileName;                HttpContext.Response.Buffer = true;                HttpContext.Response.Charset = "";                HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);                //HttpContext.Response.ContentType = "application/vnd.ms-word";                HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + FileName);                HttpContext.Response.WriteFile(FPath);                HttpContext.Response.Flush();            }            return null;        }


    }
}