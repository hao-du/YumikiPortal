using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Entity.Master;
using Yumiki.Web.Base;

namespace Yumiki.Web.Master
{
    public partial class Login : BasePage
    {
        ISecurityService securityService;
        ISecurityService SecurityService
        {
            get
            {
                if (securityService == null)
                {
                    securityService = Service.GetInstance<ISecurityService>();
                }
                return securityService;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                TB_User user = SecurityService.Login(txtUserLoginName.Text, txtPassword.Text);
                Session["UserID"] = user.ID.ToString();
                Response.Redirect("/Administration/User");
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }
    }
}