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

                lblUserName.Text = lblTimeZoneInfo.Text = string.Empty;

                lblReleaseYear.Text = DateTimeExtension.GetSystemDatetime().Year.ToString();

                if (CurrentUser.IsAuthenticated)
                {
                    lblUserName.Text = CurrentUser.UserFullName;
                    lblTimeZoneInfo.Text = CurrentUser.TimeZone?.ToString();
                    ulLogoutSection.Visible = true;
                    ulTimeZoneSection.Visible = true;
                }
            }
        }

        /// <summary>
        /// Load all menu list which current user has permission
        /// </summary>
        private void LoadMenu()
        {
            if (CurrentUser.IsAuthenticated)
            {
                lblMenu.Text = BusinessService.GetPrivilege(CurrentUser.UserID.ToString());
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