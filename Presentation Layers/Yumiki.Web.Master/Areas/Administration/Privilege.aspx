<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="Privilege.aspx.cs" inherits="Yumiki.Web.Administration.Privilege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script src="/clients/scripts/yumiki-webform-validation.js"></script>
    <style>
        .ymk-breadcrumb a {
            color: white;
            padding-top: 8px;
        }

            .ymk-breadcrumb a:hover {
                text-decoration: underline !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <asp:UpdatePanel runat="server" ID="upnlPrivilege">
        <ContentTemplate>
            <div class="container">
                <h1>Privileges</h1>
                <div class="card bg-secondary">
                    <div class="card-body">
                        <div class="btn-group">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="New" OnClick="btnAdd_Click" CausesValidation="false" />
                            <asp:Button ID="btnDisplayInactivePrivileges" runat="server" CssClass="btn btn-default" Text="Show Inactive Privileges" OnClick="btnDisplayInactivePrivileges_Click" CausesValidation="false" />
                        </div>
                        <div class="float-md-right text-white">
                            <asp:HiddenField ID="hdnNavigationIDs" runat="server" Value="" />
                            <asp:HiddenField ID="hdnNavigationNames" runat="server" Value="" />
                            <div class="ymk-breadcrumb">
                                <asp:Menu runat="server" ID="mnuNavigation" OnMenuItemClick="mnuNavigation_MenuItemClick" Orientation="Horizontal">
                                </asp:Menu>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive-lg">
                    <asp:Repeater runat="server" ID="rptPrivilege">
                        <HeaderTemplate>
                            <table id="tblPrivilege" class="table table-striped table-bordered table-hover">
                                <thead class="thead-dark">
                                    <tr>
                                        <th></th>
                                        <th>Privilege Name</th>
                                        <th>Page Path</th>
                                        <th>Displayable on UI</th>
                                        <th>Descriptions</th>
                                        <th>Active Status</th>
                                        <th>Modify Date</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CssClass="btn-link fa fa-edit" Text="" OnClick="btnEdit_Click" CommandArgument='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>' CausesValidation="false"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnShowChilden" runat="server" CssClass="btn-link" Text='<%# Eval("PrivilegeName") %>' OnClick="btnShowChilden_Click" CommandArgument='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID).ToString() + Yumiki.Commons.Dictionaries.CommonValues.SeparateCharUnique +  Eval(Yumiki.Entity.Administration.TB_Privilege.FieldName.PrivilegeName).ToString()%>' CausesValidation="false"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("PagePath") %>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="ckbIsDisplayable" Checked='<%# (bool)Eval("IsDisplayable") ? true : false %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.Descriptions) %>'></asp:Literal>
                                </td>
                                <td>
                                    <asp:CheckBox runat="server" ID="ckbIsActiveDisplay" Checked='<%# (bool)Eval(Yumiki.Commons.Dictionaries.CommonProperties.IsActive) ? true : false %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="lblModifyDate" Text='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.LastUpdateDateUI) %>'></asp:Literal>
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
            <asp:Panel runat="server" ID="pnlPrivilegeDialog" Visible="false">
                <div class="modal-backdrop show"></div>
                <div id="dlgPrivilege" class="modal d-block">
                    <div class=" modal-dialog modal-lg modal-dialog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">
                                    <asp:Literal runat="server" ID="lblPrivilegeDialogHeader"></asp:Literal>
                                </h5>
                                <asp:HiddenField ID="hdnID" runat="server" Value="" />
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>Privilege Name</label>
                                    <asp:TextBox runat="server" ID="txtPrivilegeName" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPrivilegeName" Display="None" ErrorMessage="Privilege Name is required." />
                                </div>
                                <div class="form-group">
                                    <label>Page Path</label>
                                    <asp:TextBox runat="server" ID="txtPagePath" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPagePath" Display="None" ErrorMessage="Page Path is required." />
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="ckbIsDisplayable" Checked="true" Text="Is Displayable on UI" />
                                </div>
                                <div class="form-group">
                                    <label>Descriptions</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" Rows="5" ID="txtDescription" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="ckbIsActive" Checked="true" Text="Is Active" />
                                </div>
                                <asp:ValidationSummary ID="vsPrivilegeValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="alert alert-danger" />
                            </div>
                            <div class="modal-footer bg-dark">
                                <asp:Button ID="btnDialogSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="yumiki.webForm.validation.validateInputs();" OnClick="btnDialogSave_Click" CausesValidation="true" />
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
