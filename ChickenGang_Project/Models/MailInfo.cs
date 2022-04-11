using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ChickenGang_Project.Models
{
    public class MailInfo
    {
        public void SendEmail(string address, string tenkh)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(address));  // replace with valid value 
            message.From = new MailAddress(Properties.Settings.Default.MAIL_FROM);  // replace with valid value
            message.Subject = Properties.Settings.Default.MAIL_REGISTER_SUBJECT;
            message.Body = string.Format(Properties.Settings.Default.MAIL_REGISTER_BODY, tenkh);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = Properties.Settings.Default.MAIL_FROM,  // replace with valid value
                    Password = Properties.Settings.Default.MAIL_PASSWORD  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Timeout = 20000;
                smtp.Send(message);

            }
        }

        public void SendEmailOrder(string address, string tenkh)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(address));  // replace with valid value 
            message.From = new MailAddress(Properties.Settings.Default.MAIL_FROM);  // replace with valid value
            message.Subject = Properties.Settings.Default.MAIL_REGISTER_SUBJECT;
            message.Body = string.Format(Properties.Settings.Default.MAIL_ORDER_BODY, tenkh);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = Properties.Settings.Default.MAIL_FROM,  // replace with valid value
                    Password = Properties.Settings.Default.MAIL_PASSWORD  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Timeout = 20000;
                smtp.Send(message);

            }
        }
    }
}