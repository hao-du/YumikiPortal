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
            if (!IsPostBack)
            {
                IGroupService groupService = Service.GetInstance<IGroupService>();
                List<TB_Group> groups = groupService.GetAllGroups();

                rptGroup.DataSource = groups;
                rptGroup.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnDialogClose_Click(object sender, EventArgs e)
        {

        }

        protected void btnDialogSave_Click(object sender, EventArgs e)
        {

        }
    }
}