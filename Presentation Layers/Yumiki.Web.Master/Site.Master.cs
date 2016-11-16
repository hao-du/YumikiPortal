using System;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Common.Dictionary;
using Yumiki.Web.Base;

namespace Yumiki.Web.Master
{
    public partial class Site : MasterBasePage<IGUIService>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenu();

                liLogoutSection.Visible = false;
                lblUserName.Text = string.Empty;

                if (Session[HttpConstants.Session.UserLoginName] != null)
                {
                    lblUserName.Text = Session[HttpConstants.Session.UserLoginName].ToString();
                    liLogoutSection.Visible = true;
                }
            }
        }

        /// <summary>
        /// Load all menu list which current user has permission
        /// </summary>
        private void LoadMenu()
        {
            string userID = Session[HttpConstants.Session.UserID]?.ToString();
            if (!string.IsNullOrEmpty(userID))
            {
                lblMenu.Text = BusinessService.GetPrivilege(userID);
            }
            else
            {
                lblMenu.Text = string.Empty;
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            RedirectToLoginPage(false);
        }
    }
}