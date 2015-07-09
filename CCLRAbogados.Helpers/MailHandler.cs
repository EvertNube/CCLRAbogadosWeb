using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

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
        public static void Send2(string from, string subject, string body, string names, bool copia)
        {
            try
            {
                MailMessage mail = new MailMessage(from, ConfigurationManager.AppSettings["MailFrom"], subject, body);
                if (copia)
                { mail.Bcc.Add(new MailAddress(from)); }
                //mail.Bcc.Add(new MailAddress("boris@nube.la"));
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                //client.Host = "smtp.gmail.com";
                //client.Port = 587;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new System.Net.NetworkCredential
                //("evert@go3studios.com", "BladeKnight7");
                //client.UseDefaultCredentials = true;
                client.EnableSsl = true;
                //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                client.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
