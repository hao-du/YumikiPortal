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
    public partial class Group : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IGroupService groupService = Service.GetInstance<IGroupService>();
            List<TB_Group> groups = groupService.GetAllGroups();

            rptGroup.DataSource = groups;
            rptGroup.DataBind();
        }
    }
}