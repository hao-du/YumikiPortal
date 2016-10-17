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
            lblUserDialogHeader.Text = "New User";
            pnlUserDialog.Visible = true;
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

                pnlPasswordSection.Visible = false;
                lblUserDialogHeader.Text = "Edit User";
                pnlUserDialog.Visible = true;
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        protected void btnDialogClose_Click(object sender, EventArgs e)
        {
            ResetControls();
            pnlUserDialog.Visible = false;
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
                pnlUserDialog.Visible = false;
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        /// <summary>
        /// Reset control when creating new User or closing User Dialog
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
    }
}