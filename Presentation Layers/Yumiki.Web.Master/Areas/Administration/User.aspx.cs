using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Entity.Administration;
using Yumiki.Web.Base;

namespace Yumiki.Web.Administration
{
    public partial class User : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IGroupService groupService = Service.GetInstance<IGroupService>();
            List<TB_Group> groups = groupService.GetAllGroups();

            rptUsers.DataSource = groups;
            rptUsers.DataBind();
        }
    }
}