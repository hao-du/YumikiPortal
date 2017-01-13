using System;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
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
                lblUserName.Text = lblTimeZoneInfo.Text = string.Empty;

                lblReleaseYear.Text = DateTimeHelper.GetSystemDatetime().Year.ToString();

                if (HttpSession.IsAuthenticated)
                {
                    lblUserName.Text = HttpSession.UserLoginName;
                    lblTimeZoneInfo.Text = HttpSession.TimeZone.ToString();
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