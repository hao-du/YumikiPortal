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
        /// <summary>
        /// Get Dependency Injection Service
        /// </summary>
        private Dependency Service
        {
            get
            {
                if (service == null)
                {
                    // Get domain name which contains the current page such as "SampleWebsite" in "Yumiki.Web.SampleWebsite" (index = 2)
                    string containerName = this.GetType().BaseType.FullName.Split('.')[2];
                    service = Dependency.GetService(containerName);
                }
                return service;
            }
        }

        /// <summary>
        /// Redirect to login page
        /// </summary>
        protected void RedirectToLoginPage(bool hasQueryString = true)
        {
            if (hasQueryString)
            {
                Response.Redirect(string.Format("/{0}{1}?{2}={3}", HttpConstants.Pages.WebFormMasterPrefix, CustomConfigurations.LoginPage, HttpConstants.QueryStrings.Path, Request.Path));
            }
            else
            {
                Response.Redirect(string.Format("/{0}{1}", HttpConstants.Pages.WebFormMasterPrefix, CustomConfigurations.LoginPage));
            }
        }
    }
}
