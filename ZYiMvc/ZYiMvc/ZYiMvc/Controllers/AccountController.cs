using FuLife.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZY.Extensions;
using ZYiMvc.Models;

namespace ZYiMvc.Controllers
{
    public class AccountController : Controller
    {
        private DBContext db = new DBContext();

        [AllowAnonymous]
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        [HttpPost]
        public JsonResult Login(string No, string Pwd, bool RemberMe)
        {
            dynamic result = "";
            using (var db = new DBContext())
            {
                Pwd = Common.Encode(Pwd);
                var member = db.Member.FirstOrDefault(m => m.PassWord == Pwd && (m.UserName == No || m.Email == No || m.Mobile == No));
                if (member != null)
                {
                    result = new { No = member.UserName };
                    //定义身份验证 Cookie   
                    var curDate = DateTime.Now;
                    DateTime expireDate;
                    bool isPersistent;
                    if (RemberMe)
                    {
                        expireDate = curDate.Add(FormsAuthentication.Timeout);
                        isPersistent = true;
                    }
                    else
                    {
                        expireDate = curDate.AddMinutes(30);
                        isPersistent = false;
                    }
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        member.UserName,
                        curDate,
                        expireDate,
                        isPersistent,
                        member.ID.ToString());
                    CookieHelper.SetCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket), expireDate);
                    CookieHelper.SetCookie(Utils.GetAppSeting("account_no"), Common.Encode(No), DateTime.Now.AddDays(7));


                }
                else
                {
                    result = new { errorCode = "账号或密码错误" };
                }
            }
            return Json(result);
        }
        //退出登录
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult register(Member member)
        {
            dynamic result = "";
            member.Status = "1";
            member.CreateDate = member.ModifyDate = DateTime.Now;
            member.PassWord = Common.Encode(member.PassWord);
            member.Levels = "1";
            member.Address = "";
            member.Sex = "1";
            member.Fax = "";
            member.WebSite = "";
            member.IndustryID = 1;
            member.StarDate = DateTime.Now;
            member.ValidityDate = DateTime.Now;
            member.LastLoginDate = DateTime.Now;
            member.QQ = Comvert.GetString(member.QQ);
            member.Mobile = Comvert.GetString(member.Mobile);
            db.Member.Add(member);
            db.SaveChanges();
            return Json(result);
        }

        [HttpPost]
        public JsonResult getMemberID()
        {
            return Json(Common.getMemberID());
        }
        [HttpPost]
        public JsonResult getMemberByID(int id)
        {
            dynamic result = "";
            var i = db.Member.FirstOrDefault(m => m.ID == id);
            if (i != null)
            {
                result = new
                {
                    i.ID,
                    i.UserName,
                    i.Name
                };
            }
            return Json(result);
        }
        /// <summary>
        /// 验证手机号是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JsExistsOfMobile(string mobile)
        {
            dynamic result = "";
            using (var db = new DBContext())
            {
                var member = db.Member.FirstOrDefault(m => m.Mobile == mobile);
                if (member != null)
                {
                    result = new { status = true };
                }
            }
            return Json(result);
        }
        /// <summary>
        /// 验证账号是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JsExistsOfUserName(string userName)
        {
            dynamic result = "";
            using (var db = new DBContext())
            {
                var member = db.Member.FirstOrDefault(m => m.UserName == userName);
                if (member == null)
                {
                    result = new { status = true };
                }
            }
            return Json(result);
        }
        /// <summary>
        /// 验证邮箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JsMemberOfEmail(string email)
        {
            dynamic result = "";
            using (var db = new DBContext())
            {
                var member = db.Member.FirstOrDefault(m => m.Email == email);
                if (member == null)
                {
                    result = new { status = true };
                }
            }
            return Json(result);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

    }
}