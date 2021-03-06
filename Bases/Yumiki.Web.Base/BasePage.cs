﻿using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Logging;
using Yumiki.Commons.Unity;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Settings;

namespace Yumiki.Web.Base
{
    /// <summary>
    /// Parent class for all ASP.NET Webform pages.
    /// </summary>
    public class BasePage<T> : System.Web.UI.Page
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
                    string containerName = this.GetType().BaseType.FullName.Split('.')[2];
                    _service = Dependency.GetService(containerName);
                }
                return _service;
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
        protected void RegisterScripts(params string[] scriptMethods)
        {
            StringBuilder builder = new StringBuilder();
            //Default Methods must be execcuted after postback.
            if (DefaultMethods != null)
            {
                foreach (string method in DefaultMethods)
                {
                    builder.Append(method);
                }
            }

            //Custom Methods
            foreach (string method in scriptMethods)
            {
                builder.Append(method);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "RegisterScripts", builder.ToString(), true);
        }

        protected void SendInformation(string message)
        {
            SendClientMessage(message, string.Empty, LogLevel.INFO);
        }

        protected void SendWarning(string message)
        {
            SendClientMessage(message, string.Empty, LogLevel.WARN);
        }

        protected void SendError(Exception ex)
        {
            SendClientMessage(ex.Message, ex, LogLevel.ERROR);
        }

        /// <summary>
        /// Send exception message to client side
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="ex">Exception Error</param>
        /// <param name="logType">To show exception type to client side</param>
        protected void SendClientMessage(string message, string details, LogLevel logType)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("yumiki.message.clientMessage('{0}', '{1}', '{2}');", message, details, logType.ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", stringBuilder.ToString(), true);
        }

        /// <summary>
        /// Send exception message to client side
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="ex">Exception Error</param>
        /// <param name="logType">To show exception type to client side</param>
        protected void SendClientMessage(string message, Exception ex, LogLevel logType)
        {
            if (ex != null && !(ex is YumikiException))
            {
                Logger.Append(logType, message, ex);
            }

            StringBuilder stringBuilder = new StringBuilder();
            string exMessage = Logger.GetExceptionDetails(ex);

            stringBuilder.AppendFormat("yumiki.message.clientMessage('{0}', '{1}', '{2}');", message.Replace('\'', CommonValues.HashTag), exMessage.Replace('\'', CommonValues.HashTag), logType.ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", stringBuilder.ToString(), true);
        }

        /// <summary>
        /// If session timeout, return to login page with querystring is the current working page.
        /// </summary>
        /// <param name="e">Nothing to do with this one.</param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (!CurrentUser.IsAuthenticated)   
            {
                this.Logger.Infomation(string.Format("No Session from IP: {0}, Browser: {1}, Website URL: {2}.", Request.UserHostAddress, Request.UserAgent, Request.Url));
                Response.Redirect(string.Format("/{0}{1}?{2}={3}", HttpConstants.RouteNames.WebFormMasterPrefix, SystemSettings.LoginPage, HttpConstants.QueryStrings.Path, Request.Path));
            }
        }
    }
}
