using System;
using System.Web.Http;
using System.Web.Http.Results;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Logging;
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

        protected override ExceptionResult InternalServerError(Exception exception)
        {
            if (!(exception is YumikiException))
            {
                this.Logger.Error("Error during performning http method.", exception);
            }
            return base.InternalServerError(exception);
        }
    }
}
