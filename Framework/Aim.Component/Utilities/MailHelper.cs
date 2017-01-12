using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;

namespace Aim.Component
{
    public class MailHelper
    {
        public static void SendMail(string from, string to, string title, string body, string loginUserName, string loginPassword, string smtpServerAddress)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = smtpServerAddress;//使用的SMTP服务器发送邮件
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(loginUserName, loginPassword);//SMTP服务器需要用邮箱的用户名和密码作认证
            System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
            Message.From = new System.Net.Mail.MailAddress(from);//这里需要注意，发信人的邮箱用户名必须和上面SMTP服务器认证时的用户名相同
            Message.To.Add(to);//将邮件发送
            Message.Subject = title;
            Message.Body = body;
            Message.SubjectEncoding = System.Text.Encoding.UTF8;
            Message.BodyEncoding = System.Text.Encoding.UTF8;
            Message.Priority = System.Net.Mail.MailPriority.High;
            Message.IsBodyHtml = true;
            client.Send(Message);

        }
    }
}
