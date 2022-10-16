using EmailUtility.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMSService
{
    public class Scheduler
    {
        //private string reportpath1;

        public object HttpContext { get; private set; }

        public void RunSchedule()

        {
            // if ((DateTime.Now.Hour==14 && DateTime.Now.Day == 14) || (DateTime.Now.Hour == 17 && DateTime.Now.Day == 9) || (DateTime.Now.Hour == 11 && DateTime.Now.Day == 10) || DateTime.Now.Day == 1 || DateTime.Now.Day == 25)
            if ((DateTime.Now.Hour == 21 || DateTime.Now.Hour == 22)&&(DateTime.Now.Minute>00 && DateTime.Now.Minute<59))
            {
                // string exportpath = ConfigurationSettings.AppSettings.Get("ExportPath");//"E:\\ASA nuggets\\ITG-Email\\Email Utility CR and OT\\EmailUtility\\EmailUtility\\SharedFolder\\";
                // string reportpath = ConfigurationSettings.AppSettings.Get("ReportPath");//"E:\\ASA nuggets\\ITG-Email\\Email Utility CR and OT\\EmailUtility\\EmailUtility\\Crystal Reports\\DateWiseOTCR.rpt";
                // string reportpath1 = ConfigurationSettings.AppSettings.Get("ReportPath1");
                // AuditNotificationBLL bll = new AuditNotificationBLL();

                //bll.testhangfire(reportpath, reportpath1, exportpath);

                QMS.Utilities.ServiceActions sa = new QMS.Utilities.ServiceActions();
                sa.ComplaintDueAlerts();

            }

        }
    }
}
