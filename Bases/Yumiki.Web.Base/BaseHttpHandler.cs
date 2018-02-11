using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Unity;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Settings;
using System.Web;
using System.Web.Routing;
using System.Web.Compilation;

namespace Yumiki.Web.Base
{
    /// <summary>
    /// Parent class for all ASP.NET Webform pages.
    /// </summary>
    public class HttpHandler<T> : IHttpHandler
    {
        private Logger _logger;
        public Logger Logger
        {
            get
            {
                if(_logger == null)
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
                    string containerName = this.GetType().FullName.Split('.')[2];
                    _service = Dependency.GetService(containerName);
                }
                return _service;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (!CurrentUser.IsAuthenticated)
            {
                throw new YumikiException(ExceptionCode.E_NO_SESSION, "No Authentication sufficient.");
            }

            StartProcess(context);
        }

        public virtual void StartProcess(HttpContext context)
        {

        }
    }
}
