using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Exceptions;
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

        protected override void OnException(ExceptionContext filterContext)
        {
            if (!(filterContext.Exception is YumikiException))
            {
                this.Logger.Error("Error during process request.", filterContext.Exception);
            }

            base.OnException(filterContext);

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

        protected void SendInformation(string message)
        {
            SendClientMessage(message, string.Empty, LogLevel.INFO);
        }

        protected void SendWarning(string message)
        {
            SendClientMessage(message, string.Empty, LogLevel.WARN);
        }

        protected void SendError(Exception ex)
        {
            SendClientMessage(ex.Message, ex, LogLevel.ERROR);
        }

        /// <summary>
        /// Send message to client side
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="details">Message Details</param>
        /// <param name="logType">Log Level to display dialog in Client side</param>
        protected void SendClientMessage(string message, string details, LogLevel logType)
        {
            Message clientMessage = new Message(message, details, logType);

            ViewBag.Message = clientMessage;
        }

        /// <summary>
        /// Send exception message to client side
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="ex">Exception Error</param>
        /// <param name="logType">To show exception type to client side</param>
        protected void SendClientMessage(string message, Exception ex, LogLevel logType)
        {
            if (ex != null && !(ex is YumikiException))
            {
                Logger.Append(logType, message, ex);
            }

            string details = Logger.GetExceptionDetails(ex);

            SendClientMessage(message, details, logType);
        }
    }
}
