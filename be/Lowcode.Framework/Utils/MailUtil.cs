using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using log4net;

namespace Lowcode.Framework.Utils
{
    public static class MailUtil
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MailUtil));

        /// <summary>
        /// Sen email
        /// </summary>
        /// <param name="from"></param>
        /// <param name="fromDisplayName"></param>
        /// <param name="toCollection"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachCollection"></param>
        /// <param name="isHtml"></param>
        /// <param name="isSSL"></param>
        /// <param name="mailServer"></param>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        public static void SendEmail(string from, string fromDisplayName, MailAddressCollection toCollection, string subject, string body, AttachmentCollection attachCollection, bool isHtml, bool isSSL, string mailServer, int port, string userName, string password)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(from, fromDisplayName);
                if (toCollection != null && toCollection.Count > 0)
                {
                    foreach (MailAddress to in toCollection)
                        message.To.Add(to);
                }
                message.Subject = subject;
                message.Body = body;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = isHtml;
                //attachment
                if (attachCollection != null && attachCollection.Count > 0)
                {
                    foreach (Attachment attach in attachCollection)
                        message.Attachments.Add(attach);
                }
                //send
                SmtpClient smtpClient = new SmtpClient(mailServer, port);
                smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                smtpClient.EnableSsl = isSSL;
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
            }
        }

        public static bool SendEmail(string from, string fromDisplayName, List<string> tos, List<string> ccs, List<string> bccs, string subject, string body, AttachmentCollection attachCollection, bool isHtml, bool isSSL, string mailServer, int port, string userName, string password)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(from, fromDisplayName);
                if (tos != null)
                {
                    foreach (var item in tos)
                    {
                        message.To.Add(item);
                    }
                }

                if (ccs != null)
                {
                    foreach (var item in ccs)
                    {
                        message.CC.Add(item);
                    }
                }

                if (bccs != null)
                {
                    foreach (var item in bccs)
                    {
                        message.Bcc.Add(item);
                    }
                }

                message.Subject = subject;
                message.Body = body;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = isHtml;
                //attachment
                if (attachCollection != null && attachCollection.Count > 0)
                {
                    foreach (Attachment attach in attachCollection)
                        message.Attachments.Add(attach);
                }
                //send
                SmtpClient smtpClient = new SmtpClient(mailServer, port);
                smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                smtpClient.EnableSsl = isSSL;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
        }





        /// <summary>
        /// Send a mail message from full informations
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public static void SendMail(string to, string cc, string subject, string body)
        {
            SendMail("info@vnsolution.com.vn", to, cc, subject, body);
        }

        /// <summary>
        /// Send a short mail message
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public static void SendMail(string from, string to, string cc, string subject, string body)
        {
            MailMessage message = new MailMessage();

            if (from != null) message.From = new MailAddress(from);
            if (to != null)
            {
                foreach (string address in to.Split(';'))
                    message.To.Add(address);
            }
            if (cc != string.Empty)
            {
                foreach (string address in cc.Split(';'))
                    message.CC.Add(address);
            }

            message.Body = body;

            message.Subject = subject;

            SendMail(message);
        }

        /// <summary>
        /// Send a mail message from MailMessage class
        /// </summary>
        /// <param name="message"></param>
        public static void SendMail(MailMessage message)
        {
            SmtpClient smtpClient = new SmtpClient();

            message.IsBodyHtml = true;

            smtpClient.Send(message);
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            //Regular expression object
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            if (check.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
