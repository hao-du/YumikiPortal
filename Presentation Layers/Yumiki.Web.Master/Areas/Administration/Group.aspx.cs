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
        private const string showInactiveString = "Show Inactive Groups";
        private const string showActiveString = "Show Active Groups";

        IGroupService groupService;
        IGroupService GroupService
        {
            get
            {
                if (groupService == null)
                {
                    groupService = Service.GetInstance<IGroupService>();
                }
                return groupService;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGroups();
                btnDisplayInactiveGroups.Text = showInactiveString;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ResetControls();

            ckbIsActive.Enabled = false;
            lblGroupDialogHeader.Text = "New Group";
            pnlGroupDialog.Visible = true;
        }

        protected void btnDisplayInactiveGroups_Click(object sender, EventArgs e)
        {
            if (btnDisplayInactiveGroups.Text == showInactiveString)
            {
                btnDisplayInactiveGroups.Text = showActiveString;
            }
            else
            {
                btnDisplayInactiveGroups.Text = showInactiveString;
            }

            LoadGroups();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                TB_Group group = GroupService.GetGroup(((LinkButton)sender).CommandArgument);

                hdnID.Value = group.ID.ToString();
                txtGroupName.Text = group.GroupName;
                txtDescription.Text = group.Descriptions;
                ckbIsActive.Checked = group.IsActive;

                lblGroupDialogHeader.Text = "Edit Group";
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
            try
            {
                TB_Group group = new TB_Group();
                if (!string.IsNullOrEmpty(hdnID.Value))
                {
                    group.ID = Guid.Parse(hdnID.Value);
                }
                group.GroupName = txtGroupName.Text;
                group.Descriptions = txtDescription.Text;
                group.IsActive = ckbIsActive.Checked;

                GroupService.SaveGroup(group);
                LoadGroups();

                ResetControls();
                pnlGroupDialog.Visible = false;
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        /// <summary>
        /// Reset control when creating new Group or closing Group Dialog
        /// </summary>
        private void ResetControls()
        {
            txtGroupName.Text = txtDescription.Text = hdnID.Value = string.Empty;
            ckbIsActive.Checked = true;
        }

        /// <summary>
        /// Load all groups
        /// </summary>
        private void LoadGroups()
        {
            bool showInactive;
            if (btnDisplayInactiveGroups.Text == showInactiveString)
            {
                showInactive = false;
            }
            else
            {
                showInactive = true;
            }

            List<TB_Group> groups = GroupService.GetAllGroups(showInactive);
            rptGroup.DataSource = groups;
            rptGroup.DataBind();
        }
    }
}