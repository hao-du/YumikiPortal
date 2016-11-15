using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Common.Helper;

namespace Yumiki.Web.Base
{
    /// <summary>
    /// Parent class for all ASP.NET Webform master pages.
    /// </summary>
    public class MasterBasePage<T> : System.Web.UI.MasterPage
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
        /// <summary>
        /// Get Dependency Injection Service
        /// </summary>
        private DependencyHelper Service
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
