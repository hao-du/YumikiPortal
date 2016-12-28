using System;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Commons.Dictionaries;
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

                if (HttpSession.IsAuthenticated)
                {
                    lblUserName.Text = HttpSession.UserLoginName;
                    liLogoutSection.Visible = true;
                }
            }
        }

        /// <summary>
        /// Load all menu list which current user has permission
        /// </summary>
        private void LoadMenu()
        {
            if (HttpSession.IsAuthenticated)
            {
                lblMenu.Text = BusinessService.GetPrivilege(HttpSession.UserID.ToString());
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