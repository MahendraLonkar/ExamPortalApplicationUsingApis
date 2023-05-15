using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExamPortalApplicationUsingApis.Models;
using System.Net.Mail;
using System.Net;
using System.Text;
namespace ExamPortalApplicationUsingApis.BL
{
    public class ExtraBL
    {
        static RepositoryBL<tblstudent_details, int> studrepo;
        public ExtraBL(){
            studrepo = new RepositoryBL<tblstudent_details, int>(new CIIT_CRMDBEntities());
        }
        public static string GenerateNewStudentCode(){
              int cnt = 0;
              cnt = studrepo.GetAll().Count;

            string centercode = "SCTI001";
            string lastcode = "";
            if(cnt==0)
            {
                lastcode = "0001";
            }
            else if(cnt>0 && cnt<10)
            {
                cnt = cnt + 1;
                lastcode = "000" + cnt;

            }
            else if (cnt >=10 && cnt < 100)
            {
                cnt = cnt + 1;
                lastcode = "00" + cnt;

            }
            else if (cnt >= 100 && cnt < 1000)
            {
                cnt = cnt + 1;
                lastcode = "0" + cnt;

            }

            else if (cnt >= 1000)
            {
                cnt = cnt + 1;
                lastcode = ""+ cnt;

            }

            string studentcode = centercode + lastcode;
            return studentcode;
        }


        public void Send_Email(string to, string subject, string body)
        {
            var fromAddress = new MailAddress("info@ciitinstitute.com", "CIIT Training Institute");
            var toAddress = new MailAddress(to,to);
            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            var smtp = new SmtpClient
            {
                Host = "smtpout.secureserver.net",
                Port = 25,
                Timeout = 2000000,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("info@ciitinstitute.com", "ciit@2022")
            };
            System.Net.ServicePointManager.Expect100Continue = false;
            smtp.Send(message);

        }
 
        public void Send_Gmail_Email(string to, string subject, string body)
        {
            var fromAddress = new MailAddress("ciitpune@gmail.com", "CIIT Training Institute");
            var toAddress = new MailAddress(to, to);
            // const string fromPassword = ac.accountPassword;
            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Timeout = 2000000,

                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ciitpune@gmail.com", "Champ050521")
            };
            smtp.Send(message);





        }
    



        public static string GetRandomPassword()
        {
            int length = 10;
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

    }
}