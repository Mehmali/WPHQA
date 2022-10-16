using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace QMS.Utilities
{
    public class EmailUtility
    {
        public bool SendEmail(string[] EmailAddress)
        {
            const string username = "bahsystem.alert@wphome.com";
            const string password = "Bah@dmin2020";




            SmtpClient smtpclient = new SmtpClient();
            MailMessage mail = new MailMessage();
            MailAddress from = new MailAddress("bahsystem.alert@wphome.com", "BahSystem_Alert");

            smtpclient.Host = "east.exch083.serverdata.net";
            smtpclient.Port = 587;

            mail.From = from;

         

            foreach(string email in EmailAddress)
            {
                mail.To.Add(email);
            }
            









            mail.Subject = ("CAR/CAPA Complaint");
            mail.IsBodyHtml = true;

            mail.Body = "<p></p>" +

                "<p>Hi Team,</p>" +
                "<p>New Complaint has been registered.</p>" +

            "<p>Thank you</p>" +

            "<p>***This is an auto generated email. Please do not reply. ***</p>";

            smtpclient.EnableSsl = true;
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpclient.Credentials = new System.Net.NetworkCredential(username, password);
            smtpclient.Send(mail);

            return true;
        }

    }
}