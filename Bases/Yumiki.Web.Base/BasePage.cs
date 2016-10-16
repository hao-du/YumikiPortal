using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Yumiki.Common.Helper;

namespace Yumiki.Web.Base
{
    /// <summary>
    /// Parent class for all ASP.NET Webform pages.
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        private DependencyHelper service;
        /// <summary>
        /// Get Dependency Injection Service
        /// </summary>
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

        /// <summary>
        /// Only for Postback/Partial Postback to call Javascript functions.
        /// This property is to contain all default methods must be executed for multiple code behind's methods.
        /// </summary>
        private ArrayList DefaultMethods
        {
            get;
            set;
        }

        /// <summary>
        /// Add default methods to DefaultMethods property
        /// </summary>
        /// <param name="scriptMethods">>Methods needed to call. Eg. "testFunction1();"</param>
        protected void AddDefaultMethods(params string[] methods)
        {
            if (DefaultMethods == null)
            {
                DefaultMethods = new ArrayList();
            }

            DefaultMethods.AddRange(methods);
        }

        /// <summary>
        /// To call all required Javascript method for postback.
        /// It will call the methods defined in 'DefaultMethods' property first, then call the custom methods.
        /// </summary>
        /// <param name="scriptMethods">Methods needed to call. Eg. "testFunction1();", "testFunction2();"</param>
        protected void ResetClientPlugins(params string[] scriptMethods)
        {
            StringBuilder builder = new StringBuilder();
            //Default Methods must be execcuted after postback.
            foreach (string method in DefaultMethods)
            {
                builder.Append(method);
            }

            //Custom Methods
            foreach (string method in scriptMethods)
            {
                builder.Append(method);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ResetClientPlugins", builder.ToString(), true);
        }

        /// <summary>
        /// Send a message to client side
        /// </summary>
        /// <param name="message">A message from server side</param>
        protected void SendClientMessage(string message)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("alert('{0}');", message);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", stringBuilder.ToString(), true);
        }
    }
}
