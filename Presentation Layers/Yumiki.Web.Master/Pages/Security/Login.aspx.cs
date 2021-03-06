﻿using System;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Master;
using Yumiki.Web.Base;

namespace Yumiki.Web.Master.Pages.Security
{
    public partial class Login : BasePage<ISecurityService>
    {
        /// <summary>
        /// Overide this to prevent loop when it checks NULL session in BasePage
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            //Nothing to do here.
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                TB_User user = BusinessService.Login(txtUserLoginName.Text, txtPassword.Text);

                CurrentUser.UserID = user.ID;
                CurrentUser.UserLoginName = user.UserLoginName;
                CurrentUser.UserFullName = $"{user.FirstName} {user.LastName}".Trim();
                CurrentUser.LastLoginTime = DateTimeExtension.GetSystemDatetime();
                CurrentUser.TimeZone = DateTimeExtension.GetTimeZoneById(user.TimeZone);

                string path = Request.QueryString[HttpConstants.QueryStrings.Path];
                if (string.IsNullOrWhiteSpace(path))
                {
                    path = string.Format("{0}://{1}:{2}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port.ToString());
                }
                Response.Redirect(path);
                
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            try
            {
                SendInformation("To do...");
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }
    }
}