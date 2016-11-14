using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yumiki.Common.Helper;

namespace Yumiki.Web.Base
{
    public class BaseController : Controller
    {
        private DependencyHelper service;
        protected DependencyHelper Service
        {
            get
            {
                if (service == null)
                {
                    // Get domain name which contains the current page such as "SampleWebsite" in "Yumiki.Web.SampleWebsite" (index = 2)
                    string containerName = this.GetType().BaseType.FullName.Split('.')[2];
                    service = DependencyHelper.GetService(containerName);
                }
                return service;
            }
        }
    }
}
