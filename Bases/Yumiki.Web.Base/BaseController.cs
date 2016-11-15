using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Yumiki.Common.Dictionary;
using Yumiki.Common.Helper;

namespace Yumiki.Web.Base
{
    public class BaseController<T> : Controller
    {
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

        private DependencyHelper service;
        private DependencyHelper Service
        {
            get
            {
                if (service == null)
                {
                    // Get domain name which contains the current page such as "SampleWebsite" in "Yumiki.Web.SampleWebsite" (index = 2)
                    string containerName = this.GetType().FullName.Split('.')[2];
                    service = DependencyHelper.GetService(containerName);
                }
                return service;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session[HttpConstants.Session.UserLoginName] == null)
            {
                filterContext.Result = Redirect(string.Format("{0}?{1}={2}", HttpConstants.Pages.Login, HttpConstants.QueryStrings.Path, Request.Url));
            }
        }
    }
}
