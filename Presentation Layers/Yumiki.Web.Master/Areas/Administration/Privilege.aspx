<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Privilege.aspx.cs" Inherits="Yumiki.Web.Administration.Privilege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script src="/clients/scripts/yumiki-webform-validation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <asp:UpdatePanel runat="server" ID="upnlPrivilege">
        <ContentTemplate>
            <div class="container">
                <h2>Privilege Management</h2>

                <div class="well well-sm">
                    <div class="btn-group">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="New" OnClick="btnAdd_Click" CausesValidation="false" />
                        <asp:Button ID="btnDisplayInactivePrivileges" runat="server" CssClass="btn btn-default" Text="Show Inactive Privileges" OnClick="btnDisplayInactivePrivileges_Click" CausesValidation="false" />
                    </div>
                </div>
                <div class="row">
                    <asp:HiddenField ID="hdnNavigationIDs" runat="server" Value="" />
                    <asp:HiddenField ID="hdnNavigationNames" runat="server" Value="" />
                    <div class="col-md-12">
                        <asp:Menu runat="server" ID="mnuNavigation" OnMenuItemClick="mnuNavigation_MenuItemClick" Orientation="Horizontal">
                        </asp:Menu>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:Repeater runat="server" ID="rptPrivilege">
                                <HeaderTemplate>
                                    <table id="tblPrivilege" class="table table-striped table-bordered">
                                        <thead>
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
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn-link fa fa-edit" Text="" OnClick="btnEdit_Click" CommandArgument='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>' CausesValidation="false"></asp:LinkButton>
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
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlPrivilegeDialog" Visible="false">
                <div class="modal-backdrop"></div>
                <div id="dlgPrivilege" class="modal show" role="dialog">
                    <div class=" modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Literal runat="server" ID="lblPrivilegeDialogHeader"></asp:Literal>
                                </h4>
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
                                    <asp:CheckBox runat="server" ID="ckbIsDisplayable" Checked="true" Text="Is Displayable on UI" CssClass="checkbox" />
                                </div>
                                <div class="form-group">
                                    <label>Descriptions</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescription" CssClass="form-control form-area"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="ckbIsActive" Checked="true" Text="Is Active" CssClass="checkbox" />
                                </div>
                                <asp:ValidationSummary ID="vsPrivilegeValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="well well-sm alert alert-danger"/>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnDialogSave" runat="server" Text="Save" CssClass="btn btn-default" OnClientClick="yumiki.webForm.validation.validateInputs();" OnClick="btnDialogSave_Click" CausesValidation="true" />
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
