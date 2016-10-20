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
        private const string showInactiveString = "Show Inactive Users";
        private const string showActiveString = "Show Active Users";

        private const int listTabIndex = 0;
        private const int addEditTabIndex = 1;
        private const int moreDetailTabIndex = 2;
        private const int resetPasswordTabIndex = 3;

        IUserService userService;
        IUserService UserService
        {
            get
            {
                if (userService == null)
                {
                    userService = Service.GetInstance<IUserService>();
                }
                return userService;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
                btnDisplayInactiveUsers.Text = showInactiveString;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ResetControls();

            ckbIsActive.Enabled = false;
            btnUserProcess.Text = "New User";
            SwitchPanel(btnUserProcess);
        }

        protected void LinkButton_Click(object sender, EventArgs e)
        {
            SwitchPanel(sender);
        }

        protected void btnDisplayInactiveUsers_Click(object sender, EventArgs e)
        {
            if (btnDisplayInactiveUsers.Text == showInactiveString)
            {
                btnDisplayInactiveUsers.Text = showActiveString;
            }
            else
            {
                btnDisplayInactiveUsers.Text = showInactiveString;
            }

            LoadUsers();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                TB_User user = UserService.GetUser(((LinkButton)sender).CommandArgument);

                hdnID.Value = user.ID.ToString();
                txtUserLoginName.Text = user.UserLoginName;
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtPassword.Text = txtConfirmPassword.Text = user.CurrentPassword;
                txtDescription.Text = user.Descriptions;
                ckbIsActive.Checked = user.IsActive;

                ckbIsActive.Enabled = true;
                pnlPasswordSection.Visible = false;
                btnUserProcess.Text = "Edit User";
                SwitchPanel(btnUserProcess);
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        protected void btnDialogSave_Click(object sender, EventArgs e)
        {
            try
            {
                TB_User user = new TB_User();
                if (!string.IsNullOrEmpty(hdnID.Value))
                {
                    user.ID = Guid.Parse(hdnID.Value);
                }
                user.UserLoginName = txtUserLoginName.Text;
                user.FirstName = txtFirstName.Text;
                user.LastName = txtLastName.Text;
                user.CurrentPassword = txtPassword.Text;
                user.Descriptions = txtDescription.Text;
                user.IsActive = ckbIsActive.Checked;

                UserService.SaveUser(user);
                LoadUsers();

                ResetControls();
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        /// <summary>
        /// Reset all server side controls when focus on User List tab
        /// </summary>
        private void ResetControls()
        {
            txtUserLoginName.Text = txtPassword.Text = txtConfirmPassword.Text = txtFirstName.Text = txtLastName.Text = txtDescription.Text = hdnID.Value = string.Empty;
            ckbIsActive.Checked = true;
            pnlPasswordSection.Visible = true;
        }

        /// <summary>
        /// Load all users
        /// </summary>
        private void LoadUsers()
        {
            bool showInactive;
            if (btnDisplayInactiveUsers.Text == showInactiveString)
            {
                showInactive = false;
            }
            else
            {
                showInactive = true;
            }

            List<TB_User> users = UserService.GetAllUsers(showInactive);
            rptUser.DataSource = users;
            rptUser.DataBind();
        }

        /// <summary>
        /// Switch panel on UI by setting style and visibility of tabs and views
        /// </summary>
        /// <param name="sender">Link Button from tab headers</param>
        private void SwitchPanel(object sender)
        {
            mtvUserTabs.ActiveViewIndex = listTabIndex;
            liUserList.Attributes.Remove("class");
            liUserProcess.Attributes.Remove("class");
            liUserDetails.Attributes.Remove("class");
            liResetPassword.Attributes.Remove("class");

            liUserProcess.Visible = false;
            liUserDetails.Visible = false;
            liResetPassword.Visible = false;

            if (((LinkButton)sender).ID == btnUserList.ID)
            {
                mtvUserTabs.ActiveViewIndex = listTabIndex;
                liUserList.Attributes.Add("class", "active");
            }
            else
            {
                liUserProcess.Visible = true;
                btnUserProcess.Visible = true;
                liUserDetails.Visible = true;
                btnUserDetails.Visible = true;
                liResetPassword.Visible = true;
                btnResetPassword.Visible = true;

                if (((LinkButton)sender).ID == btnUserProcess.ID)
                {
                    mtvUserTabs.ActiveViewIndex = addEditTabIndex;
                    liUserProcess.Attributes.Add("class", "active");
                }
                else if (((LinkButton)sender).ID == btnUserDetails.ID)
                {
                    mtvUserTabs.ActiveViewIndex = moreDetailTabIndex;
                    liUserDetails.Attributes.Add("class", "active");
                }
                else if (((LinkButton)sender).ID == btnResetPassword.ID)
                {
                    mtvUserTabs.ActiveViewIndex = resetPasswordTabIndex;
                    liResetPassword.Attributes.Add("class", "active");
                }
            }
        }
    }
}