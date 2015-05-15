using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using ZYiMvc.Models;
using System.Drawing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ZY.Extensions
{
    public class Common
    {
       

        /// <summary>
        /// 获取凭据中的用户名
        /// </summary>
        /// <returns>用户名</returns>
        public static int getMemberID()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                return DawnTypeConverter.TypeToInt32(strUserData, 0);
            }
            else
            {
                return 0;
            }
        }
        
        public static string getMemberInfo()
        {
            StringBuilder sb = new StringBuilder();
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.Name;
                sb.Append("<a href='javascript:void(0);'>加工币： <strong>100</strong></a>");
                sb.Append("<a href='javascript:void(0);'>资金： <strong>￥20</strong></a>");
                sb.Append("<a href='javascript:void(0);'>站内信： <strong>0</strong></a>");
                sb.Append(" <span>您好！" + strUserData + "</span>");
            }
            return sb.ToString();
        }

        public class ImageHelper
        {
            public static Image GetNewImage(Image oldImg, int newWidth, int newHeight)
            {
                Image newImg = oldImg.GetThumbnailImage(newWidth, newHeight, new Image.GetThumbnailImageAbort(IsTrue), IntPtr.Zero); // 对原图片进行缩放 
                return newImg;
            }
            private static bool IsTrue() // 在Image类别对图片进行缩放的时候,需要一个返回bool类别的委托 
            {
                return true;
            }
        }

        /// <summary>
        /// 密码加密
        /// </summary>
        public static string Encode(string pwd)
        {
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(pwd));
        }

        /// <summary>
        /// 密码解密
        /// </summary>
        public static string Decode(string pwd)
        {
            return System.Text.Encoding.Default.GetString(Convert.FromBase64String(pwd));
        }

        public static string webSite = "http://FuLife.wmiku.com";

        public static void SendEmail(string receiver, string title, string content)
        {
            ExecNetEmail.SetEmail(receiver, title, content);
            ExecNetEmail.SendEmail();
        }
        /// <summary>  
        /// 生成12位唯一的数字 并发可用  
        /// </summary>  
        /// <returns></returns>  
        public static string GenerateShortUniqueID()
        {
            System.Threading.Thread.Sleep(1); //保证yyyyMMddHHmmssffff唯一  
            Random d = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string strUnique = DateTime.Now.ToString("yyMMddHHmmssffff") + d.Next(100000, 999999);
            return strUnique;
        }

        /// 生成24位唯一的数字 并发可用  
        /// <summary>  
        /// </summary>  
        /// <returns></returns>  
        public static string GenerateUniqueID()
        {
            System.Threading.Thread.Sleep(1); //保证yyyyMMddHHmmssffff唯一  
            Random d = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string strUnique = DateTime.Now.ToString("yyyyMMddHHmmssffff") + d.Next(100000, 999999);
            return strUnique;
        }
        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="rawUrl"></param>
        /// <param name="Code"></param>
        /// <param name="OpenId"></param>
        public static string getMemberMsg()
        {
            StringBuilder sb = new StringBuilder();
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.Name;
                sb.Append("<span class=\"top_nav_login\">hi,<a target=\"_top\" href=\"/Account/Index\">" + strUserData + "</a></span>");
            }
            else
            {
                string strUserNo = CookieHelper.GetCookieValue("account_no");
                if (string.IsNullOrEmpty(strUserNo))
                {
                    sb.Append("<span class=\"top_nav_login\"><a href='/Home/Index#login'>请登录</a></span><span class=\"top_nav_login\"><a class='register' href='/Account/Register'>免费注册</a></span>");
                }
                else
                {
                    strUserNo = Decode(strUserNo);
                    sb.Append("<span class=\"top_nav_login\"><a href='/Home/Index#login'><em>" + strUserNo + "</em>/登录</a></span><span class=\"top_nav_login\"><a class='register' href='/Account/Register'>免费注册</a></span>");
                }
            }
            return sb.ToString();
        }
        #region 短信相关
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="content">短信内容</param>
        public static string SmsSend(string mobile, string content)
        {
            try
            {
                dynamic result = null;
                string Access = "2394";
                string Secret = "df7f16d36d43d9453611a06c0da8c00214518270";
                string Request = "http://sms.bechtech.cn/Api/send/data/json?accesskey={0}&secretkey={1}&mobile={2}&content={3}";
                string UrlString = string.Format(Request, Access, Secret, mobile, content);
                string JsonText = System.Text.Encoding.UTF8.GetString(new System.Net.WebClient().DownloadData(UrlString));
                JArray list = (JArray)JsonConvert.DeserializeObject("[" + JsonText + "]");
                result = list[0]["result"];
                return result;
            }
            catch (Exception ex)
            {
                return "404";
            }
            //var smsResult = Common.SmsSend("13430807567", string.Format(SMS.Order, 20140716, "朱炀炀", 13430807567, "南山区科苑北科兴科学园A19"));
        }

        /// <summary>
        /// 查询短信余额
        /// </summary>
        public static string SmsMoney()
        {
            dynamic result = null;
            string Access = "2394";
            string Secret = "df7f16d36d43d9453611a06c0da8c00214518270";
            string Request = "http://sms.bechtech.cn/Api/getLeft/data/json?accesskey={0}&secretkey={1}";
            string UrlString = string.Format(Request, Access, Secret);
            string JsonText = System.Text.Encoding.UTF8.GetString(new System.Net.WebClient().DownloadData(UrlString));
            JArray list = (JArray)JsonConvert.DeserializeObject("[" + JsonText + "]");
            result = list[0]["result"];
            return result;
        }
        #endregion

    }
}
/// <summary>
/// 能带参数的异常类
/// </summary>
public class MyException : Exception
{
    public string ErrorCode { get; set; }
    public string[] Param { get; set; }
    public MyException(string code)
    {
        this.ErrorCode = code;
    }
    public MyException(string code, string[] param)
    {
        this.ErrorCode = code;
        this.Param = param;
    }
    public string Message { get; set; }
}

public static class StringHelpers
{
    /// <summary>
    /// 将字符串进行Html编码
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string HtmlEncode(this string code)
    {
        return HttpUtility.HtmlEncode(code);
    }

    /// <summary>
    /// 将字符串进行Hmtl解码
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string HtmlDecode(this string code)
    {
        return HttpUtility.HtmlDecode(code);
    }

}