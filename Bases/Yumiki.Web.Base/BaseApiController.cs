using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.SessionState;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Settings;
using Yumiki.Commons.Unity;

namespace Yumiki.Web.Base
{
    public class ApiBaseController<T> : ApiController
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

        private HttpSession httpSession;
        public HttpSession HttpSession
        {
            get
            {
                if(httpSession == null)
                {
                    httpSession = new HttpSession(HttpContext.Current.Session);
                }
                return httpSession;
            }
        }

        protected override ExceptionResult InternalServerError(Exception exception)
        {
            
            if (!(exception is YumikiException))
            {
                this.Logger.Error("Error during performning http method.", exception);
            }
            return base.InternalServerError(exception);
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            if (!HttpSession.IsAuthenticated)
            {
                HttpResponseException exception = new HttpResponseException(new HttpResponseMessage(HttpStatusCode.GatewayTimeout)
                {
                    Content = new StringContent(string.Format("{0} - {1}", ExceptionCode.E_NO_SESSION.ToString(), "Session has not been initialized or expired.")),
                    ReasonPhrase = ExceptionCode.E_NO_SESSION.ToString()
                });
                this.Logger.Error(ExceptionCode.E_NO_SESSION.ToString(), exception);
                throw exception;
            }
        }
    }
}
