﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Yumiki.Web.Administration.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script>
        //Validation for asp.net controls on client side.
        var startValidation = function () {
            var isValid = true;
            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                ValidatorValidate(control);
                if (!control.isvalid) {
                    isValid = false;
                    $(control).closest(".form-user").addClass("has-error has-feedback");

                } else {
                    $(control).closest(".form-user").removeClass("has-error has-feedback");
                }
            }

            if (!isValid) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <asp:UpdatePanel runat="server" ID="upnlUser">
        <ContentTemplate>
            <div class="container">
                <h2>User Management</h2>

                <div class="well well-sm">
                    <div class="btn-user">
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
                                            <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval(Yumiki.Common.Dictionary.CommonProperties.Descriptions) %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="ckbIsActive" Checked='<%# (bool)Eval(Yumiki.Common.Dictionary.CommonProperties.IsActive) ? true : false %>' Enabled="false" />
                                        </td>
                                        <td>
                                            <asp:Literal runat="server" ID="lblModifyDate" Text='<%# Eval(Yumiki.Common.Dictionary.CommonProperties.LastUpdateDateUI) %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn-link" Text="Edit" OnClick="btnEdit_Click" CommandArgument='<%# Eval(Yumiki.Common.Dictionary.CommonProperties.ID) %>' CausesValidation="false"></asp:LinkButton>
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
            </div>
            <asp:Panel runat="server" ID="pnlUserDialog" Visible="false">
                <div class="modal-backdrop"></div>
                <div id="dlgUser" class="modal show" role="dialog">
                    <div class=" modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Literal runat="server" ID="lblUserDialogHeader"></asp:Literal>
                                </h4>
                                <asp:HiddenField ID="hdnID" runat="server" Value="" />
                            </div>
                            <div class="modal-body">
                                <div class="form-user">
                                    <label>Login Name</label>
                                    <asp:TextBox runat="server" ID="txtUserLoginName" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserName" Display="None" ErrorMessage="User Name is required." />
                                </div>
                                <asp:Panel runat="server" ID="pnlPasswordSection">
                                    <div class="form-user">
                                        <label>Password</label>
                                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Password is required." />
                                    </div>
                                    <div class="form-user">
                                        <label>Confirm Password</label>
                                        <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword" Display="None" ErrorMessage="Confirm Password is required." />
                                    </div>
                                </asp:Panel>
                                <div class="form-user">
                                    <label>First Name</label>
                                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-user">
                                    <label>Last Name</label>
                                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-user">
                                    <label>Descriptions</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescription" CssClass="form-control form-area"></asp:TextBox>
                                </div>
                                <div class="form-user">
                                    <asp:CheckBox runat="server" ID="ckbIsActive" Checked="true" Text="Is Active" CssClass="checkbox" />
                                </div>
                                <asp:ValidationSummary ID="vsUserValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="well well-sm alert alert-danger" />
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnDialogSave" runat="server" Text="Save" CssClass="btn btn-default" OnClientClick="startValidation()" OnClick="btnDialogSave_Click" CausesValidation="true" />
                                <asp:Button ID="btnDialogClose" runat="server" Text="Close" CssClass="btn btn-default" OnClick="btnDialogClose_Click" CausesValidation="false" />
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
