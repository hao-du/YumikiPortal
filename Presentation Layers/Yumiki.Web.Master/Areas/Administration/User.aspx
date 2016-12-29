<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Yumiki.Web.Administration.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script>
        //Validation for asp.net controls on client side.
        var startUserValidation = function () {
            var isValid = true;
            //Reset all form group css by removing "has-error has-feedback" to fix issue "One control has many validators"
            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                $(control).closest(".form-group").removeClass("has-error has-feedback");
            }

            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                ValidatorValidate(control);
                if (!control.isvalid) {
                    isValid = false;
                    $(control).closest(".form-group").addClass("has-error has-feedback");
                }
            }

            if (!isValid) {
                return false;
            }
            else {
                return true;
            }
        }

        var initPicker = function () {
            $('.select-picker').selectpicker({
                style: 'btn-default'
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <asp:UpdatePanel runat="server" ID="upnlUser">
        <ContentTemplate>
            <div class="container">
                <asp:HiddenField ID="hdnID" runat="server" Value="" />
                <h2>User Management</h2>
                <ul class="nav nav-tabs">
                    <li class="active" runat="server" id="liUserList">
                        <asp:LinkButton runat="server" ID="btnUserListTab" Text="User List" CausesValidation="false" OnClick="LinkButton_Click"></asp:LinkButton></li>
                    <li runat="server" id="liUserProcess" visible="false">
                        <asp:LinkButton runat="server" ID="btnUserProcessTab" Text="Add/Edit User" CausesValidation="false" OnClick="LinkButton_Click"></asp:LinkButton></li>
                    <li runat="server" id="liUserDetails" visible="false">
                        <asp:LinkButton runat="server" ID="btnUserDetailsTab" Text="More Infomation" CausesValidation="false" OnClick="LinkButton_Click"></asp:LinkButton></li>
                    <li runat="server" id="liResetPassword" visible="false">
                        <asp:LinkButton runat="server" ID="btnResetPasswordTab" Text="Reset Password" CausesValidation="false" OnClick="LinkButton_Click"></asp:LinkButton></li>
                </ul>
                <asp:MultiView runat="server" ID="mtvUserTabs" ActiveViewIndex="0">
                    <asp:View ID="vwListTab" runat="server">
                        <div class="well well-sm">
                            <div class="btn-group">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="New" OnClick="btnAdd_Click" CausesValidation="false" />
                                <asp:Button ID="btnDisplayInactiveUsers" runat="server" CssClass="btn btn-primary" Text="Show Inactive Users" OnClick="btnDisplayInactiveUsers_Click" CausesValidation="false" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:Repeater runat="server" ID="rptUser">
                                        <HeaderTemplate>
                                            <table id="tblUser" class="table table-striped table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Login Name</th>
                                                        <th>Full Name</th>
                                                        <th>Descriptions</th>
                                                        <th>Active Status</th>
                                                        <th>Modify Date</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Literal runat="server" ID="lblUserName" Text='<%# Eval("UserLoginName") %>'></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("FullName") %>'></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.Descriptions) %>'></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="ckbIsActive" Checked='<%# (bool)Eval(Yumiki.Commons.Dictionaries.CommonProperties.IsActive) ? true : false %>' Enabled="false" />
                                                </td>
                                                <td>
                                                    <asp:Literal runat="server" ID="lblModifyDate" Text='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.LastUpdateDateUI) %>'></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn-link" Text="Edit" OnClick="btnEdit_Click" CommandArgument='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>' CausesValidation="false"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View runat="server" ID="vwAddEditTab">
                        <div class="well well-sm">
                            <div class="btn-group">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="startUserValidation()" OnClick="btnUserSave_Click" CausesValidation="true" />
                            </div>
                        </div>
                        <asp:ValidationSummary ID="vsUserValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="well well-sm alert alert-danger" />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Login Name</label>
                                    <asp:TextBox runat="server" ID="txtUserLoginName" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserLoginName" Display="None" ErrorMessage="User Name is required." />
                                </div>

                                <div class="form-group">
                                    <label>Time Zone</label>
                                    <asp:DropDownList runat="server" ID="ddlSystemTimeZone" CssClass="form-control select-picker"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSystemTimeZone" Display="None" ErrorMessage="Time Zone is required." />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>First Name</label>
                                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Last Name</label>
                                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlPasswordSection">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Password</label>
                                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Password is required." />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Confirm Password</label>
                                        <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword" Display="None" ErrorMessage="Confirm Password is required." />
                                        <asp:CompareValidator runat="server" ID="cmpPasswordCompare" ControlToValidate="txtPassword" ControlToCompare="txtConfirmPassword" Operator="Equal" Type="String" ErrorMessage="Confirm Password must be the same password." Display="None" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="form-group">
                            <label>Descriptions</label>
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescription" CssClass="form-control form-area"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="ckbIsActive" Checked="true" Text="Is Active" CssClass="checkbox" />
                        </div>
                    </asp:View>
                    <asp:View runat="server" ID="vwUserDetailsTab">
                        <div class="well well-sm">
                            <div class="btn-group">
                                <asp:Button ID="btnAddContact" runat="server" CssClass="btn btn-primary" Text="New" OnClick="btnAddContact_Click" CausesValidation="false" />
                            </div>
                        </div>
                        <asp:Repeater runat="server" ID="rptContactType">
                            <ItemTemplate>
                                <div class="panel-group">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" href='#<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>'><%# Eval("ContactTypeName") %></a>
                                            </h4>
                                        </div>
                                        <div id='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>' class="panel-collapse collapse in">
                                            <div class="panel-body">
                                                <asp:Repeater runat="server" ID="rptAddressDetail" DataSource='<%# Eval(Yumiki.Entity.Administration.TB_ContactType.FieldName.SortUserAddresses) %>'>
                                                    <HeaderTemplate>
                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Contact Detail</th>
                                                                        <th>Primary Contact</th>
                                                                        <th>Descriptions</th>
                                                                        <th>Modify Date</th>
                                                                        <th></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Literal runat="server" Text='<%# Eval("UserAddress") %>'></asp:Literal>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox runat="server" Checked='<%# (bool)Eval("IsPrimary") ? true : false %>' Enabled="false" />
                                                            </td>
                                                            <td>
                                                                <asp:Literal runat="server" Text='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.Descriptions) %>'></asp:Literal>
                                                            </td>
                                                            <td>
                                                                <asp:Literal runat="server" ID="lblModifyDate" Text='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.LastUpdateDateUI) %>'></asp:Literal>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="btAddressEdit" runat="server" CssClass="btn-link" Text="Edit" OnClick="btnEditContact_Click" CommandArgument='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>' CausesValidation="false"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                        </table>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:View>
                    <asp:View runat="server" ID="vwResetPassword">
                        <div class="well well-sm">
                            <div class="btn-group">
                                <asp:Button ID="btnResetPassword" runat="server" Text="Reset" CssClass="btn btn-primary" OnClientClick="startUserValidation()" OnClick="btnResetPassword_Click" CausesValidation="true" />
                            </div>
                        </div>
                        <asp:ValidationSummary ID="vsResetPasswordValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="well well-sm alert alert-danger" />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Password</label>
                                    <asp:TextBox runat="server" ID="txtResetPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtResetPassword" Display="None" ErrorMessage="Password is required." />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Confirm Password</label>
                                    <asp:TextBox runat="server" ID="txtConfirmResetPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="ResetPassword" runat="server" ControlToValidate="txtConfirmResetPassword" Display="None" ErrorMessage="Confirm Password is required." />
                                    <asp:CompareValidator runat="server" ID="cmpResetPassword" ControlToValidate="txtResetPassword" ControlToCompare="txtConfirmResetPassword" Operator="Equal" Type="String" ErrorMessage="Confirm Password must be the same password." Display="None" />
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
            <asp:Panel runat="server" ID="pnlUserAddressDialog" Visible="false">
                <div class="modal-backdrop"></div>
                <div id="dlgGroup" class="modal show" role="dialog">
                    <div class=" modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Literal runat="server" ID="lblUserAddressDialogHeader"></asp:Literal>
                                </h4>
                                <asp:HiddenField ID="hdnUserAddressID" runat="server" Value="" />
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>Contact Details</label>
                                    <asp:TextBox runat="server" ID="txtUserAddress" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserAddress" Display="None" ErrorMessage="Contact Details is required." />
                                </div>
                                <div class="form-group">
                                    <label>Contact Type</label>
                                    <asp:DropDownList runat="server" ID="ddlContactType" CssClass="form-control select-picker"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlContactType" Display="None" ErrorMessage="Contact Type is required." />
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="ckbIsPrimary" Checked="true" Text="Is Primary" CssClass="checkbox" />
                                </div>
                                <div class="form-group">
                                    <label>Descriptions</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtUserAddressDescriptions" CssClass="form-control form-area"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="ckbUserAddressIsActive" Checked="true" Text="Is Active" CssClass="checkbox" />
                                </div>
                                <asp:ValidationSummary ID="vsGroupValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="well well-sm alert alert-danger" />
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnDialogSave" runat="server" Text="Save" CssClass="btn btn-default" OnClientClick="startUserValidation()" OnClick="btnDialogUserContactSave_Click" CausesValidation="true" />
                                <asp:Button ID="btnDialogClose" runat="server" Text="Close" CssClass="btn btn-default" OnClick="btnDialogUserContactClose_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubContentContainer" runat="server">
</asp:Content>
