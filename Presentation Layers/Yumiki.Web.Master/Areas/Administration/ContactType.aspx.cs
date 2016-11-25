using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Entity.Administration;
using Yumiki.Web.Base;

namespace Yumiki.Web.Administration
{
    public partial class ContactType: BasePage<IContactTypeService>
    {
        private const string showInactiveString = "Show Inactive Types";
        private const string showActiveString = "Show Active Types";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDisplayInactiveContactTypes.Text = showInactiveString;
                LoadContentTypes();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ResetControls();

            ckbIsActive.Enabled = false;
            lblContactTypeDialogHeader.Text = "New Type";
            pnlContactTypeDialog.Visible = true;
        }

        protected void btnDisplayInactiveContactTypes_Click(object sender, EventArgs e)
        {
            if (btnDisplayInactiveContactTypes.Text == showInactiveString)
            {
                btnDisplayInactiveContactTypes.Text = showActiveString;
            }
            else
            {
                btnDisplayInactiveContactTypes.Text = showInactiveString;
            }

            LoadContentTypes();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                TB_ContactType contactType = BusinessService.GetContactType(((LinkButton)sender).CommandArgument);

                hdnID.Value = contactType.ID.ToString();
                txtContactTypeName.Text = contactType.ContactTypeName;
                txtDescription.Text = contactType.Descriptions;
                ckbIsActive.Checked = contactType.IsActive;

                ckbIsActive.Enabled = true;
                lblContactTypeDialogHeader.Text = "Edit Type";
                pnlContactTypeDialog.Visible = true;
            }
            catch (Exception ex)
            {
                SendClientMessage(ex.Message);
            }
        }

        protected void btnDialogClose_Click(object sender, EventArgs e)
        {
            ResetControls();
            pnlContactTypeDialog.Visible = false;
        }

        protected void btnDialogSave_Click(object sender, EventArgs e)
        {
            try
            {
                TB_ContactType contactType = new TB_ContactType();
                if (!string.IsNullOrEmpty(hdnID.Value))
                {
                    contactType.ID = Guid.Parse(hdnID.Value);
                }
                contactType.ContactTypeName = txtContactTypeName.Text;
                contactType.Descriptions = txtDescription.Text;
                contactType.IsActive = ckbIsActive.Checked;

                BusinessService.SaveContactType(contactType);
                LoadContentTypes();

                ResetControls();
                pnlContactTypeDialog.Visible = false;
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
            txtContactTypeName.Text = txtDescription.Text = hdnID.Value = string.Empty;
            ckbIsActive.Checked = true;
        }

        /// <summary>
        /// Load all groups
        /// </summary>
        private void LoadContentTypes()
        {
            bool showInactive;
            if (btnDisplayInactiveContactTypes.Text == showInactiveString)
            {
                showInactive = false;
            }
            else
            {
                showInactive = true;
            }

            List<TB_ContactType> contactTypes = BusinessService.GetAllContactTypes(showInactive);
            rptContactType.DataSource = contactTypes;
            rptContactType.DataBind();
        }
    }
}