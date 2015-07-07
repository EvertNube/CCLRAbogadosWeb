using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Helpers
{
    public static class MailHandler
    {
        public static void Send(string to, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["MailFrom"], to, subject, body);
                mail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["MailFrom"]));
                //mail.Bcc.Add(new MailAddress("boris@nube.la"));
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                //client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Send2(string from, string subject, string body, string? names = null)
        {
            try
            {
                MailMessage mail = new MailMessage(from, ConfigurationManager.AppSettings["MailFrom"], subject, body);
                mail.Bcc.Add(new MailAddress(from));
                //mail.Bcc.Add(new MailAddress("boris@nube.la"));
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                //client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
