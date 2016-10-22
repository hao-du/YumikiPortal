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

        /// <summary>
        /// If user has ID, it is Edit Mode. Otherwise, it is New Mode.
        /// </summary>
        private bool IsNewMode
        {
            get
            {
                return string.IsNullOrEmpty(hdnID.Value) ? true : false;
            }
        }

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

        protected void LinkButton_Click(object sender, EventArgs e)
        {
            SwitchPanel(sender);
        }

        #region User List Tab Events
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ResetControls();

            ckbIsActive.Enabled = false;
            btnUserProcessTab.Text = "New User";
            SwitchPanel(btnUserProcessTab);
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

                LoadUserAddresses();

                txtUserLoginName.Enabled = false;
                ckbIsActive.Enabled = true;
                pnlPasswordSection.Visible = false;
                btnUserProcessTab.Text = "Edit User";
                SwitchPanel(btnUserProcessTab);
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }
        #endregion

        #region User Add/Edit Tab Events
        protected void btnDialogSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.Equals(txtPassword.Text, txtConfirmPassword.Text))
                {
                    SendClientMessage("Confirm Password must be the same password.");
                }

                TB_User user = new TB_User();
                if (!IsNewMode)
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

                if (IsNewMode)
                {
                    //If user was just created, switch to User List Tab to avoid saving twice.
                    SwitchPanel(btnUserListTab);
                    ResetControls();
                }
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }
        #endregion

        #region User Reset Password Tab Events
        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string userID = hdnID.Value;
                string password = txtResetPassword.Text;
                string confirmPassword = txtConfirmResetPassword.Text;

                if (!string.Equals(password, confirmPassword))
                {
                    SendClientMessage("Confirm Password must be the same password.");
                }

                UserService.ResetPassword(userID, txtUserLoginName.Text, password);
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }
        #endregion

        #region User Address Detail Tab Events
        protected void btnAddContact_Click(object sender, EventArgs e)
        {
        }

        protected void btnEditContact_Click(object sender, EventArgs e)
        {
        }
        #endregion

        /// <summary>
        /// Reset all server side controls when focus on User List tab
        /// </summary>
        private void ResetControls()
        {
            txtUserLoginName.Text = txtPassword.Text = txtConfirmPassword.Text = txtFirstName.Text = txtLastName.Text = txtDescription.Text = hdnID.Value = string.Empty;
            ckbIsActive.Checked = true;
            txtUserLoginName.Enabled = true;
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
        /// Load all contacts of selected user
        /// </summary>
        private void LoadUserAddresses()
        {
            if (!IsNewMode)
            {
                List<TB_ContactType> contactTypes = UserService.GetAllContacts(hdnID.Value, false);

                rptContactType.DataSource = contactTypes;
                rptContactType.DataBind();
            }
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

            if (((LinkButton)sender).ID == btnUserListTab.ID)
            {
                mtvUserTabs.ActiveViewIndex = listTabIndex;
                liUserList.Attributes.Add("class", "active");
            }
            else
            {
                liUserProcess.Visible = true;
                btnUserProcessTab.Visible = true;
                if (!IsNewMode)
                {
                    liUserDetails.Visible = true;
                    btnUserDetailsTab.Visible = true;
                    liResetPassword.Visible = true;
                    btnResetPasswordTab.Visible = true;
                }

                if (((LinkButton)sender).ID == btnUserProcessTab.ID)
                {
                    mtvUserTabs.ActiveViewIndex = addEditTabIndex;
                    liUserProcess.Attributes.Add("class", "active");
                }
                else if (((LinkButton)sender).ID == btnUserDetailsTab.ID)
                {
                    mtvUserTabs.ActiveViewIndex = moreDetailTabIndex;
                    liUserDetails.Attributes.Add("class", "active");
                }
                else if (((LinkButton)sender).ID == btnResetPasswordTab.ID)
                {
                    mtvUserTabs.ActiveViewIndex = resetPasswordTabIndex;
                    liResetPassword.Attributes.Add("class", "active");
                }
            }
        }
    }
}