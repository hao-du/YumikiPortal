using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yumiki.Business.Administration;
using Yumiki.Entity.Administration;

namespace Yumiki.Web.Administration
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GroupService groupService = new GroupService();
            List<TB_Group> groups = groupService.GetAllGroups();

            rptUsers.DataSource = groups;
            rptUsers.DataBind();
        }
    }
}