﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Common.Dictionary;
using Yumiki.Common.Helper;
using Yumiki.Entity.Administration;
using Yumiki.Web.Base;

namespace Yumiki.Web.Administration
{
    public partial class Privilege : BasePage
    {
        private const string showInactiveString = "Show Inactive Privileges";
        private const string showActiveString = "Show Active Privileges";

        IPrivilegeService privilegeService;
        IPrivilegeService PrivilegeService
        {
            get
            {
                if (privilegeService == null)
                {
                    privilegeService = Service.GetInstance<IPrivilegeService>();
                }
                return privilegeService;
            }
        }

        #region Navigation
        /// <summary>
        /// To track navigation ids of current privilege list. The root id is Guid.Empty.
        /// </summary>
        private List<Navigation> navigations;
        private List<Navigation> Navigations
        {
            get
            {
                if (navigations == null || navigations.Count == 0)
                {
                    navigations = new List<Navigation>();
                    //In case of initializing the page.
                    if (string.IsNullOrEmpty(hdnNavigationIDs.Value))
                    {
                        navigations.Add(new Navigation { NavigationID = Guid.Empty.ToString(), NavigationName = "Home" });
                        hdnNavigationIDs.Value = navigations[0].NavigationID;
                        hdnNavigationNames.Value = navigations[0].NavigationName;
                    }
                    //In case of posting back page.
                    else
                    {
                        string[] ids = hdnNavigationIDs.Value.Split(CommonValues.SeparateChar);
                        string[] names = hdnNavigationNames.Value.Split(CommonValues.SeparateChar);
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            navigations.Add(new Navigation { NavigationID = ids[i], NavigationName = names[i] });
                        }
                    }
                }
                return navigations;
            }
            set
            {
                navigations.Clear();
                hdnNavigationIDs.Value = hdnNavigationNames.Value = string.Empty;
                foreach (Navigation navigation in value)
                {
                    navigations.Add(navigation);

                    if (hdnNavigationIDs.Value == string.Empty)
                    {
                        hdnNavigationIDs.Value = navigation.NavigationID;
                        hdnNavigationNames.Value = navigation.NavigationName;
                    }
                    else
                    {
                        hdnNavigationIDs.Value = string.Format("{0}{1}{2}", hdnNavigationIDs.Value, CommonValues.SeparateChar, navigation.NavigationID);
                        hdnNavigationNames.Value = string.Format("{0}{1}{2}", hdnNavigationNames.Value, CommonValues.SeparateChar, navigation.NavigationName);
                    }
                }
            }
        }

        /// <summary>
        /// Base on Navigation IDs to get the closest parent Id.
        /// </summary>
        private Navigation ClosestNavigation
        {
            get
            {
                if (Navigations.Count == 0)
                {
                    return new Navigation { NavigationID = Guid.Empty.ToString(), NavigationName = "Home" };
                }
                return Navigations[Navigations.Count - 1];
            }
            set
            {
                if (value != null && !value.NavigationID.Equals(ClosestNavigation.NavigationID))
                {
                    Navigations.Add(value);
                    hdnNavigationIDs.Value = string.Format("{0}{1}{2}", hdnNavigationIDs.Value, CommonValues.SeparateChar, value.NavigationID);
                    hdnNavigationNames.Value = string.Format("{0}{1}{2}", hdnNavigationNames.Value, CommonValues.SeparateChar, value.NavigationName);
                }
            }
        }

        /// <summary>
        /// Refresh Navigations
        /// </summary>
        /// <param name="selectedNavigationID">closest Parent node need to reach to</param>
        private void RefreshNavigations(string selectedNavigationID)
        {
            List<Navigation> temp = new List<Navigation>();
            foreach (Navigation navigation in Navigations)
            {
                temp.Add(navigation);

                if (string.Equals(selectedNavigationID, navigation.NavigationID))
                {
                    break;
                }
            }
            Navigations = temp;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPrivileges();
                btnDisplayInactivePrivileges.Text = showInactiveString;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ResetControls();

            ckbIsActive.Enabled = false;
            lblPrivilegeDialogHeader.Text = "New Privilege";
            pnlPrivilegeDialog.Visible = true;
        }

        protected void btnDisplayInactivePrivileges_Click(object sender, EventArgs e)
        {
            if (btnDisplayInactivePrivileges.Text == showInactiveString)
            {
                btnDisplayInactivePrivileges.Text = showActiveString;
            }
            else
            {
                btnDisplayInactivePrivileges.Text = showInactiveString;
            }
            LoadPrivileges();            
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                TB_Privilege privilege = PrivilegeService.GetPrivilege(((LinkButton)sender).CommandArgument);

                hdnID.Value = privilege.ID.ToString();
                txtPrivilegeName.Text = privilege.PrivilegeName;
                txtPagePath.Text = privilege.PagePath;
                ckbIsDisplayable.Checked = privilege.IsDisplayable;
                txtDescription.Text = privilege.Descriptions;
                ckbIsActive.Checked = privilege.IsActive;

                ckbIsActive.Enabled = true;
                lblPrivilegeDialogHeader.Text = "Edit Privilege";
                pnlPrivilegeDialog.Visible = true;
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        protected void btnShowChilden_Click(object sender, EventArgs e)
        {
            try
            {
                string[] navigation = ((LinkButton)sender).CommandArgument.Split(CommonValues.SeparateChar);

                ClosestNavigation = new Navigation {  NavigationID = navigation[0], NavigationName = navigation[1] };

                LoadPrivileges();
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        protected void btnDialogClose_Click(object sender, EventArgs e)
        {
            ResetControls();
            pnlPrivilegeDialog.Visible = false;
        }

        protected void btnDialogSave_Click(object sender, EventArgs e)
        {
            try
            {
                TB_Privilege privilege = new TB_Privilege();
                if (!string.IsNullOrEmpty(hdnID.Value))
                {
                    privilege.ID = Guid.Parse(hdnID.Value);
                }
                privilege.PrivilegeName = txtPrivilegeName.Text;
                privilege.PagePath = txtPagePath.Text;
                privilege.IsDisplayable = ckbIsDisplayable.Checked;
                privilege.Descriptions = txtDescription.Text;
                privilege.IsActive = ckbIsActive.Checked;
                privilege.ParentPrivilegeID = Guid.Parse(ClosestNavigation.NavigationID);

                PrivilegeService.SavePrivilege(privilege);
                LoadPrivileges();

                ResetControls();
                pnlPrivilegeDialog.Visible = false;
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        /// <summary>
        /// Load the navigation path on UI to back to parent privilege purpose
        /// </summary>
        private void LoadNavigations()
        {
            int count = Navigations.Count;
            mnuNavigation.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                Navigation navigation = Navigations[i];

                MenuItem item = new MenuItem(navigation.NavigationName, navigation.NavigationID);   
                mnuNavigation.Items.Add(item);

                if(i < count - 1)
                {
                    MenuItem itemSeparator = new MenuItem("\\", "\\");
                    itemSeparator.Enabled = false;
                    mnuNavigation.Items.Add(itemSeparator);
                }
            }
        }

        protected void mnuNavigation_MenuItemClick(object sender, MenuEventArgs e)
        {
            try
            {
                string navigationID = e.Item.Value;
                RefreshNavigations(navigationID);
                LoadPrivileges();
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
            txtPrivilegeName.Text = txtDescription.Text = txtPagePath.Text = hdnID.Value = string.Empty;
            ckbIsActive.Checked = ckbIsDisplayable.Checked = true;
        }

        /// <summary>
        /// Load all groups
        /// </summary>
        private void LoadPrivileges()
        {
            bool showInactive;
            if (btnDisplayInactivePrivileges.Text == showInactiveString)
            {
                showInactive = false;
            }
            else
            {
                showInactive = true;
            }

            List<TB_Privilege> privileges = PrivilegeService.GetAllPrivileges(showInactive, ClosestNavigation.NavigationID);
            rptPrivilege.DataSource = privileges;
            rptPrivilege.DataBind();

            LoadNavigations();
        }

        
    }

    public class Navigation
    {
        public string NavigationID { get; set; }
        public string NavigationName { get; set; }
    }
}