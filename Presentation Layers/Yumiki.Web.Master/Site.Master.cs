using System;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Common.Dictionary;
using Yumiki.Web.Base;

namespace Yumiki.Web.Master
{
    public partial class Site : MasterBasePage
    {
        IGUIService guiService;
        IGUIService GUIService
        {
            get
            {
                if (guiService == null)
                {
                    guiService = Service.GetInstance<IGUIService>();
                }
                return guiService;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenu();

                if(Session[HttpConstants.Session.UserLoginName] != null)
                {
                    lblUserName.Text = Session[HttpConstants.Session.UserLoginName].ToString();
                }
                else
                {
                    lblUserName.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Load all menu list which current user has permission
        /// </summary>
        private void LoadMenu()
        {
            try
            {
                lblMenu.Text = GUIService.GetPrivilege(Session[HttpConstants.Session.UserID]?.ToString());
            }
            catch(Exception ex)
            {

            }
        }
    }
}