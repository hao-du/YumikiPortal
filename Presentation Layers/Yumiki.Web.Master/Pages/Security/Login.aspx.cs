using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Common.Dictionary;
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

                Session[HttpConstants.Session.UserID] = user.ID.ToString();
                Session[HttpConstants.Session.UserLoginName] = user.UserLoginName.ToString();
                Session[HttpConstants.Session.LastLoginTime] = DateTime.Now;

                string path = Request.QueryString[HttpConstants.QueryStrings.Path];
                if (string.IsNullOrEmpty(path))
                {
                    path = HttpConstants.Pages.Home;
                }
                Response.Redirect(path);
                
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            try
            {
                SendClientMessage("To do...");
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }
    }
}