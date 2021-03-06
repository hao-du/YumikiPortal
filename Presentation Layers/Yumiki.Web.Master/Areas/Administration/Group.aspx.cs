﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Entity.Administration;
using Yumiki.Web.Base;

namespace Yumiki.Web.Administration
{
    public partial class Group : BasePage<IGroupService>
    {
        private const string showInactiveString = "Show Inactive Groups";
        private const string showActiveString = "Show Active Groups";

        private const string showUnassignedUserString = "Show Unassigned Users";
        private const string showAssignedUserString = "Show Assigned Users";

        private const string showUnassignedPrivilegeString = "Show Unassigned Privileges";
        private const string showAssignedPrivilegeString = "Show Assigned Privileges";

        private const string assignString = "Assign";
        private const string unassignString = "Unassign";

        private const int groupListTabIndex = 0;
        private const int userAssignmentTabIndex = 1;
        private const int privilegeAssignmentTabIndex = 2;

        /// <summary>
        /// If group has ID, it is Edit Mode. Otherwise, it is New Mode.
        /// </summary>
        private bool IsNewMode
        {
            get
            {
                return string.IsNullOrWhiteSpace(hdnGlobalGroupID.Value) ? true : false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGroups();
                btnDisplayInactiveGroups.Text = showInactiveString;
                btnDisplayUser.Text = showAssignedUserString;
                btnDisplayPrivilege.Text = showAssignedPrivilegeString;
            }
        }

        protected void linkButton_Click(object sender, EventArgs e)
        {
            SwitchPanel(sender);
        }

        #region Group List
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
                TB_Group group = BusinessService.GetGroup(((LinkButton)sender).CommandArgument);

                hdnDialogGroupID.Value = group.ID.ToString();
                txtGroupName.Text = group.GroupName;
                txtDescription.Text = group.Descriptions;
                ckbIsActive.Checked = group.IsActive;

                ckbIsActive.Enabled = true;
                lblGroupDialogHeader.Text = "Edit Group";
                pnlGroupDialog.Visible = true;
            }
            catch (Exception ex)
            {
                SendError(ex);
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
                if (!string.IsNullOrWhiteSpace(hdnDialogGroupID.Value))
                {
                    group.ID = Guid.Parse(hdnDialogGroupID.Value);
                }
                group.GroupName = txtGroupName.Text;
                group.Descriptions = txtDescription.Text;
                group.IsActive = ckbIsActive.Checked;

                BusinessService.SaveGroup(group);
                LoadGroups();

                ResetControls();
                pnlGroupDialog.Visible = false;
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }

        protected void btnShowOtherTabs_Click(object sender, EventArgs e)
        {
            hdnGlobalGroupID.Value = ((LinkButton)sender).CommandArgument;
            SwitchPanel(btnUserAssignmentTab);
            LoadUsersFromGroup();
            LoadPrivilegesFromGroup();
        }
        #endregion

        #region User Assignment
        protected void btnDisplayUser_Click(object sender, EventArgs e)
        {
            if (btnDisplayUser.Text == showUnassignedUserString)
            {
                btnDisplayUser.Text = showAssignedUserString;
                btnAssignUnassignUser.Text = assignString;
                lblDisplayUserDescription.Text = "Unassigned User List";
            }
            else
            {
                btnDisplayUser.Text = showUnassignedUserString;
                btnAssignUnassignUser.Text = unassignString;
                lblDisplayUserDescription.Text = "Assigned User List";
            }

            int userCount = LoadUsersFromGroup();
            if (userCount == 0)
            {
                btnAssignUnassignUser.Visible = false;
            }
            else
            {
                btnAssignUnassignUser.Visible = true;
            }

            int privilegeCount = LoadPrivilegesFromGroup();
            if (privilegeCount == 0)
            {
                btnAssignUnassignPrivilege.Visible = false;
            }
            else
            {
                btnAssignUnassignPrivilege.Visible = true;
            }
        }

        protected void btnAssignUnassignUser_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> userIDs = new List<string>();
                foreach (RepeaterItem item in rptUser.Items)
                {
                    CheckBox ckbSelect = item.FindControl("ckbSelect") as CheckBox;
                    HiddenField hdnUserID = item.FindControl("hdnUserID") as HiddenField;

                    if (ckbSelect.Checked)
                    {
                        userIDs.Add(hdnUserID.Value);
                    }
                }

                if (btnDisplayUser.Text == showAssignedUserString)
                {
                    BusinessService.AddUsersToGroup(hdnGlobalGroupID.Value, userIDs);
                }
                else
                {
                    BusinessService.RemoveUsersFromGroup(hdnGlobalGroupID.Value, userIDs);
                }

                LoadUsersFromGroup();
            }
            catch(Exception ex)
            {
                SendError(ex);
            }
        }

        /// <summary>
        /// Load users to User List for Assign/Unassgined to group purpose.
        /// </summary>
        private int LoadUsersFromGroup()
        {
            bool showAssignedUser;
            if (btnDisplayUser.Text == showAssignedUserString)
            {
                showAssignedUser = false;
            }
            else
            {
                showAssignedUser = true;
            }

            List<TB_User> users = BusinessService.GetUsersFromGroup(hdnGlobalGroupID.Value, showAssignedUser);
            rptUser.DataSource = users;
            rptUser.DataBind();

            return users.Count;
        }
        #endregion

        #region Privilege Assignment
        protected void btnDisplayPrivilege_Click(object sender, EventArgs e)
        {
            if (btnDisplayPrivilege.Text == showUnassignedPrivilegeString)
            {
                btnDisplayPrivilege.Text = showAssignedPrivilegeString;
                btnAssignUnassignPrivilege.Text = assignString;
                lblDisplayPrivilegeDescription.Text = "Unassigned Privilege List";
            }
            else
            {
                btnDisplayPrivilege.Text = showUnassignedPrivilegeString;
                btnAssignUnassignPrivilege.Text = unassignString;
                lblDisplayPrivilegeDescription.Text = "Assigned Privilege List";
            }

            int privilegeCount = LoadPrivilegesFromGroup();
            if (privilegeCount == 0)
            {
                btnAssignUnassignPrivilege.Visible = false;
            }
            else
            {
                btnAssignUnassignPrivilege.Visible = true;
            }
        }

        protected void btnAssignUnassignPrivilege_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> privilegeIDs = new List<string>();
                foreach (RepeaterItem item in rptPrivilege.Items)
                {
                    CheckBox ckbSelect = item.FindControl("ckbSelect") as CheckBox;
                    HiddenField hdnPrivilegeID = item.FindControl("hdnPrivilegeID") as HiddenField;

                    if (ckbSelect.Checked)
                    {
                        privilegeIDs.Add(hdnPrivilegeID.Value);
                    }
                }

                if (btnDisplayPrivilege.Text == showAssignedPrivilegeString)
                {
                    BusinessService.AddPrivilegesToGroup(hdnGlobalGroupID.Value, privilegeIDs);
                }
                else
                {
                    BusinessService.RemovePrivilegesFromGroup(hdnGlobalGroupID.Value, privilegeIDs);
                }

                LoadPrivilegesFromGroup();
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }

        /// <summary>
        /// Load privileges to Privilege List for Assign/Unassgined to group purpose.
        /// </summary>
        private int LoadPrivilegesFromGroup()
        {
            bool showAssignedPrivilege;
            if (btnDisplayPrivilege.Text == showAssignedPrivilegeString)
            {
                showAssignedPrivilege = false;
            }
            else
            {
                showAssignedPrivilege = true;
            }

            List<TB_Privilege> privileges = BusinessService.GetPrivilegesFromGroup(hdnGlobalGroupID.Value, showAssignedPrivilege);
            rptPrivilege.DataSource = privileges;
            rptPrivilege.DataBind();

            return privileges.Count;
        }
        #endregion

        /// <summary>
        /// Switch panel on UI by setting style and visibility of tabs and views
        /// </summary>
        /// <param name="sender">Link Button from tab headers</param>
        private void SwitchPanel(object sender)
        {
            if(sender == null)
            {
                sender = btnGroupListTab;
            }

            mtvGroupTabs.ActiveViewIndex = groupListTabIndex;
            liGroupList.Attributes["class"] = liUserAssignment.Attributes["class"] = liPrivilegeAssignment.Attributes["class"] = "nav-link";

            liUserAssignment.Visible = false;
            liPrivilegeAssignment.Visible = false;

            if (((LinkButton)sender).ID == btnGroupListTab.ID)
            {
                mtvGroupTabs.ActiveViewIndex = groupListTabIndex;
                liGroupList.Attributes["class"] = "nav-link active";
            }
            else
            {
                liUserAssignment.Visible = true;
                btnUserAssignmentTab.Visible = true;

                liPrivilegeAssignment.Visible = true;
                btnPrivilegeAssignmentTab.Visible = true;

                if (((LinkButton)sender).ID == btnUserAssignmentTab.ID)
                {
                    mtvGroupTabs.ActiveViewIndex = userAssignmentTabIndex;
                    liUserAssignment.Attributes["class"] = "nav-link active";
                }
                else if (((LinkButton)sender).ID == btnPrivilegeAssignmentTab.ID)
                {
                    mtvGroupTabs.ActiveViewIndex = privilegeAssignmentTabIndex;
                    liPrivilegeAssignment.Attributes["class"] = "nav-link active";
                }
            }
        }

        /// <summary>
        /// Reset control when creating new Group or closing Group Dialog
        /// </summary>
        private void ResetControls()
        {
            txtGroupName.Text = txtDescription.Text = hdnDialogGroupID.Value = string.Empty;
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

            List<TB_Group> groups = BusinessService.GetAllGroups(showInactive);
            rptGroup.DataSource = groups;
            rptGroup.DataBind();

            SwitchPanel(null);
        }
    }
}