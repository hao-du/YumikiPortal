using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Settings;
using Yumiki.Commons.Unity;

namespace Yumiki.Web.Base
{
    /// <summary>
    /// Parent class for all ASP.NET Webform master pages.
    /// </summary>
    public class MasterBasePage<T> : System.Web.UI.MasterPage
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
        /// <summary>
        /// Get Dependency Injection Service
        /// </summary>
        private Dependency Service
        {
            get
            {
                if (_service == null)
                {
                    // Get domain name which contains the current page such as "SampleWebsite" in "Yumiki.Web.SampleWebsite" (index = 2)
                    string containerName = this.GetType().BaseType.FullName.Split('.')[2];
                    _service = Dependency.GetService(containerName);
                }
                return _service;
            }
        }

        /// <summary>
        /// Redirect to login page
        /// </summary>
        protected void RedirectToLoginPage(bool hasQueryString = true)
        {
            if (hasQueryString)
            {
                Response.Redirect(string.Format("/{0}{1}?{2}={3}", HttpConstants.Pages.WebFormMasterPrefix, SystemSettings.LoginPage, HttpConstants.QueryStrings.Path, Request.Path));
            }
            else
            {
                Response.Redirect(string.Format("/{0}{1}", HttpConstants.Pages.WebFormMasterPrefix, SystemSettings.LoginPage));
            }
        }
    }
}
