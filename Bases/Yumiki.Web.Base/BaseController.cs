using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Settings;
using Yumiki.Commons.Unity;

namespace Yumiki.Web.Base
{
    public class BaseController<T> : Controller
    {
        private Logger _logger;
        public Logger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = new Logger(GetType());
                }
                return _logger;
            }
        }

        private T _businessService;
        protected T BusinessService
        {
            get
            {
                if (_businessService == null)
                {
                    _businessService = Service.GetInstance<T>();
                }
                return _businessService;
            }
        }

        private Dependency _service;
        private Dependency Service
        {
            get
            {
                if (_service == null)
                {
                    // Get domain name which contains the current page such as "SampleWebsite" in "Yumiki.Web.SampleWebsite" (index = 2)
                    string containerName = this.GetType().FullName.Split('.')[2];
                    _service = Dependency.GetService(containerName);
                }
                return _service;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (!CurrentUser.IsAuthenticated)
            {
                this.Logger.Infomation(string.Format("No Session from IP: {0}, Browser: {1}, Website URL: {2}.", Request.UserHostAddress, Request.UserAgent, Request.Url));
                filterContext.Result = RedirectToLoginPage();
            }
        }

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);

            ViewBag.UserName = ViewBag.UserID = ViewBag.LastLoginTime = ViewBag.TimeZone = string.Empty;

            if (CurrentUser.IsAuthenticated)
            {
                ViewBag.UserName = CurrentUser.UserLoginName;
                ViewBag.UserID = CurrentUser.UserID;
                ViewBag.LastLoginTime = CurrentUser.LastLoginTime;
                ViewBag.TimeZone = CurrentUser.TimeZone;
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
                return Redirect(string.Format("/{0}{1}?{2}={3}", HttpConstants.Pages.WebFormMasterPrefix, SystemSettings.LoginPage, HttpConstants.QueryStrings.Path, Request.Url));
            }
            else
            {
                return Redirect(string.Format("/{0}{1}", HttpConstants.Pages.WebFormMasterPrefix, SystemSettings.LoginPage));
            }
        }


    }
}
