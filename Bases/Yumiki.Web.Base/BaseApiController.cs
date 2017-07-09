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

        /// <summary>
        /// Call another service instance
        /// </summary>
        /// <typeparam name="E">Business Service Layer Class</typeparam>
        /// <returns>Instance of Business Service Layer Class</returns>
        protected E GetServiceInstance<E>()
        {
            return Service.GetInstance<E>();
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

            if (!CurrentUser.IsAuthenticated)
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
