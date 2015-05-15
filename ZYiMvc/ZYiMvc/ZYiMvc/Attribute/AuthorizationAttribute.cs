using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace System
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsAuthenticated == true)
            {
                FormsIdentity identity = (FormsIdentity)httpContext.User.Identity;
                FormsAuthenticationTicket Ticket = identity.Ticket; //取得身份验证票
                if (!Ticket.IsPersistent)
                {
                    //如果是临时性的验证凭证，则过30分钟自动过期
                    if (Ticket.IssueDate.AddMinutes(30) < DateTime.Now)
                    {
                        FormsAuthentication.SignOut();
                        httpContext.Session.Abandon();
                        return false;
                    }
                }
                else
                {
                    if (Ticket.Expiration < DateTime.Now)
                    {
                        FormsAuthentication.SignOut();
                        httpContext.Session.Abandon();
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Home/Index#login?ReturnUrl="
                    + System.Web.HttpUtility.UrlEncode(filterContext.HttpContext.Request.CurrentExecutionFilePath));
                return;
        }
    }
}