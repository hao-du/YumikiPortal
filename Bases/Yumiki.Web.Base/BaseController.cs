using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Yumiki.Commons.Configurations;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Unity;

namespace Yumiki.Web.Base
{
    public class BaseController<T> : Controller
    {
        private Logger logger;
        public Logger Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = new Logger(GetType());
                }
                return logger;
            }
        }

        private T businessService;
        protected T BusinessService
        {
            get
            {
                if (businessService == null)
                {
                    businessService = Service.GetInstance<T>();
                }
                return businessService;
            }
        }

        private Dependency service;
        private Dependency Service
        {
            get
            {
                if (service == null)
                {
                    // Get domain name which contains the current page such as "SampleWebsite" in "Yumiki.Web.SampleWebsite" (index = 2)
                    string containerName = this.GetType().FullName.Split('.')[2];
                    service = Dependency.GetService(containerName);
                }
                return service;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session[HttpConstants.Session.UserLoginName] == null)
            {
                this.Logger.Infomation(string.Format("Session End from IP: {0}, Browser: {1}, Website URL: {2}.", Request.UserHostAddress, Request.UserAgent, Request.Url));
                filterContext.Result = RedirectToLoginPage();
            }
        }

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);

            ViewBag.UserName = string.Empty;
            ViewBag.UserID = string.Empty;
            ViewBag.LastLoginTime = string.Empty;

            if (Session[HttpConstants.Session.UserLoginName] != null)
            {
                ViewBag.UserName = Session[HttpConstants.Session.UserLoginName].ToString();
                ViewBag.UserID = Session[HttpConstants.Session.UserID].ToString();
                ViewBag.LastLoginTime = Session[HttpConstants.Session.LastLoginTime].ToString();
            }
        }

        /// <summary>
        /// Redirect to login page
        /// </summary>
        /// <returns></returns>
        protected RedirectResult RedirectToLoginPage(bool hasQueryString = true)
        {
            if (hasQueryString)
            {
                return Redirect(string.Format("/{0}{1}?{2}={3}", HttpConstants.Pages.WebFormMasterPrefix, CustomConfigurations.LoginPage, HttpConstants.QueryStrings.Path, Request.Url));
            }
            else
            {
                return Redirect(string.Format("/{0}{1}", HttpConstants.Pages.WebFormMasterPrefix, CustomConfigurations.LoginPage));
            }
        }


    }
}
