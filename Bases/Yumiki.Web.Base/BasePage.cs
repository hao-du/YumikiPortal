using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;

namespace Yumiki.Web.Base
{
    /// <summary>
    /// Parent class for all ASP.NET Webform pages.
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// Get Dependency Injection Service
        /// </summary>
        protected DependencyHelper Service
        {
            get
            {
                // Get domain name which contains the current page such as "SampleWebsite" in "Yumiki.Web.SampleWebsite" (index = 2)
                string containerName = this.GetType().BaseType.FullName.Split('.')[2];
                return DependencyHelper.GetService(containerName);
            }
        }
    }
}
