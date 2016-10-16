using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Common.Helper;
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
            ResetControls();
            pnlGroupDialog.Visible = true;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                IGroupService groupService = Service.GetInstance<IGroupService>();
                TB_Group group = groupService.GetGroup(((LinkButton)sender).CommandArgument);

                hdnID.Value = group.ID.ToString();
                txtGroupName.Text = group.GroupName;
                txtDescription.Text = group.Descriptions;

                pnlGroupDialog.Visible = true;
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        protected void btnDialogClose_Click(object sender, EventArgs e)
        {
            ResetControls();
            pnlGroupDialog.Visible = false;
        }

        protected void btnDialogSave_Click(object sender, EventArgs e)
        {
        }

        private void ResetControls()
        {
            txtGroupName.Text = txtDescription.Text = hdnID.Value = string.Empty;
        }
    }
}