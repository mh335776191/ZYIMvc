using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web;

namespace ZY.Extensions
{
    public class ExecNetEmail
    {

        private static readonly string fromEmail = "litianxiang2046@163.com";
        private static readonly string fromPwd = "dandanbaobei";
        private static readonly string fromUserName = "wmiku";
        private static readonly string emailServer = "smtp.163.com";
        private static readonly int emailServerPort = 25;
        private static MailMessage mailMessage = null;
        private static SmtpClient smtpClient = null;
        /// <summary>
        /// 创建实例
        /// </summary>
        private static void CreateMsg()
        {
            //if (mailMessage == null)
            //{
            mailMessage = new MailMessage();
            //}
        }
        /// <summary>
        /// 创建实例
        /// </summary>
        private static void CreateSmtpClient()
        {
            if (smtpClient == null)
            {
                smtpClient = new SmtpClient();
            }
        }

        /// <summary>
        /// 追加接受Email地址
        /// </summary>
        /// <param name="toEmail"></param>
        public static void ToEmailAppend(string toEmail)
        {
            try
            {
                CreateMsg();
                if (CheckEmail(toEmail))
                {
                    mailMessage.To.Add(new MailAddress(toEmail));
                }
                else
                {
                    throw new System.Exception("toEmail地址错误！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  追加抄送Email地址
        /// </summary>
        /// <param name="toCcEmail"></param>
        public static void ToCcEmailAppend(string toCcEmail)
        {
            try
            {
                CreateMsg();
                if (CheckEmail(toCcEmail))
                {
                    mailMessage.CC.Add(new MailAddress(toCcEmail));
                }
                else
                {
                    throw new System.Exception("toEmail地址错误！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  追加密送Email地址
        /// </summary>
        /// <param name="toCcEmail"></param>
        public static void ToBccEmailAppend(string toBccEmail)
        {
            try
            {
                CreateMsg();
                if (CheckEmail(toBccEmail))
                {
                    mailMessage.Bcc.Add(new MailAddress(toBccEmail));
                }
                else
                {
                    throw new System.Exception("toEmail地址错误！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 设置邮件
        /// </summary>
        /// <param name="toEmail">邮件接收地址</param>
        /// <param name="eSubject">邮件标题</param>
        /// <param name="eBody">邮件内容</param>
        public static void SetEmail(string toEmail, string eSubject, string eBody)
        {
            try
            {
                if (mailMessage == null)
                {
                    CreateMsg();
                }
                int index = toEmail.IndexOf(",");
                if (index > 0)
                {
                    string[] arrEmail = toEmail.Split(',');
                    if (arrEmail.Length > 0)
                    {
                        for (int i = 0; i < arrEmail.Length; i++)
                        {
                            if (CheckEmail(arrEmail[i]))
                            {
                                //if (i == 0)
                                //{
                                //    mailMessage.To.Add(new MailAddress(arrEmail[i]));
                                //}
                                //else
                                //{
                                mailMessage.Bcc.Add(new MailAddress(arrEmail[i], arrEmail[i]));
                                //}
                            }
                            else
                            {
                                throw new System.Exception("toEmail地址错误！");
                            }
                        }
                    }
                }
                else
                {
                    if (CheckEmail(toEmail))
                    {
                        mailMessage.To.Add(new MailAddress(toEmail));
                    }
                    else
                    {
                        throw new System.Exception("toEmail地址错误！");
                    }

                }
                mailMessage.From = new MailAddress(fromEmail);
                mailMessage.Subject = eSubject;
                mailMessage.Body = eBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.Priority = System.Net.Mail.MailPriority.Normal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置邮件
        /// </summary>
        /// <param name="toEmail">邮件接收地址</param>
        /// <param name="toCcMail">邮件抄送地址</param>
        /// <param name="eSubject">邮件标题</param>
        /// <param name="eBody">邮件内容</param>
        public static void SetEmail(string toEmail, string toCcMail, string eSubject, string eBody)
        {
            try
            {
                CreateMsg();
                if (!string.IsNullOrEmpty(toCcMail))
                {
                    int index = toCcMail.IndexOf(",");
                    if (index > 0)
                    {
                        string[] arrEmail = toCcMail.Split(',');
                        if (arrEmail.Length > 0)
                        {
                            string OldEmail = string.Empty;
                            for (int i = 0; i < arrEmail.Length; i++)
                            {
                                if (CheckEmail(arrEmail[i]))
                                {
                                    if (i == 0)
                                    {
                                        mailMessage.CC.Add(new MailAddress(arrEmail[i]));
                                        OldEmail = arrEmail[i];
                                    }
                                    else
                                    {
                                        if (OldEmail != arrEmail[i])
                                        {
                                            mailMessage.CC.Add(new MailAddress(arrEmail[i]));
                                        }
                                    }
                                }
                                else
                                {
                                    throw new System.Exception("toEmail地址错误！");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (CheckEmail(toCcMail))
                        {
                            mailMessage.CC.Add(new MailAddress(toCcMail));
                        }
                        else
                        {
                            throw new System.Exception("toEmail地址错误！");
                        }

                    }
                }
                SetEmail(toEmail, eSubject, eBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// 添加附件  
        /// </summary>
        /// <param name="pathFileList">文件绝对路径列表</param> 
        public static void Attachments(List<string> pathFileList)
        {
            CreateMsg();
            System.Net.Mail.Attachment data = null;
            ContentDisposition disposition;
            foreach (string path in pathFileList)
            {
                data = new System.Net.Mail.Attachment(path, MediaTypeNames.Application.Octet);//实例化 附件  
                disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(path);//获取 附件的创建日期  
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(path);// 获取附件的修改日期  
                disposition.ReadDate = System.IO.File.GetLastAccessTime(path);//获取附 件的读取日期  
                mailMessage.Attachments.Add(data);//添加到附件中  
            }
        }

        /// <summary>  
        /// 异步发送邮件  
        /// </summary>  
        /// <param name="CompletedMethod"></param>  
        public static void SendAsync(SendCompletedEventHandler CompletedMethod)
        {
            if (mailMessage != null)
            {
                CreateSmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, fromPwd);//设置发件人身份的票据  
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mailMessage.From.Host;
                smtpClient.Port = emailServerPort;
                smtpClient.SendCompleted += new SendCompletedEventHandler(CompletedMethod);//注册异步发送邮件完成时的事件  
                smtpClient.SendAsync(mailMessage, mailMessage.Body);
                mailMessage.CC.Clear();
                mailMessage.Headers.Remove("cc");
                mailMessage.Bcc.Clear();
                mailMessage.Body = string.Empty;
                mailMessage.To.Clear();
            }
        }

        /// <summary>  
        /// 发送邮件  
        /// </summary>  
        public static void SendEmail()
        {
            if (mailMessage != null)
            {
                try
                {
                    CreateSmtpClient();
                    smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, fromPwd);//设置发件人身份的票据  
                    smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //smtpClient.Host = "smtp." + mailMessage.From.Host;
                    smtpClient.Host = emailServer;
                    smtpClient.Port = emailServerPort;
                    smtpClient.Send(mailMessage);
                    mailMessage.Bcc.Clear();
                    mailMessage.CC.Clear();
                    mailMessage.Headers.Remove("cc");
                    mailMessage.To.Clear();
                    mailMessage.Body = string.Empty;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 检查Email地址
        /// </summary>
        /// <param name="email">需验证的Email地址</param>
        /// <returns>true=正确，false=错误</returns>
        private static bool CheckEmail(string email)
        {
            return Regex.Match(email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Success;
        }
    }
}