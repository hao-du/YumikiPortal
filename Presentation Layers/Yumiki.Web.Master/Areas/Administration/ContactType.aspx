<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="ContactType.aspx.cs" inherits="Yumiki.Web.Administration.ContactType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script src="/clients/scripts/yumiki-webform-validation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <asp:UpdatePanel runat="server" ID="upnlContactType">
        <ContentTemplate>
            <div class="container">
                <h1>Contact Types</h1>
                <div class="card bg-secondary">
                    <div class="card-body">
                        <div class="btn-group">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="New" OnClick="btnAdd_Click" CausesValidation="false" />
                            <asp:Button ID="btnDisplayInactiveContactTypes" runat="server" CssClass="btn btn-default" Text="Show Inactive ContactTypes" OnClick="btnDisplayInactiveContactTypes_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
                <div class="table-responsive-lg">
                    <asp:Repeater runat="server" ID="rptContactType">
                        <HeaderTemplate>
                            <table id="tblContactType" class="table table-striped table-bordered table-hover">
                                <thead class="thead-dark">
                                    <tr>
                                        <th></th>
                                        <th>Contact Type Name</th>
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
                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn-link fa fa-edit" ToolTip="Edit" Text="" OnClick="btnEdit_Click" CommandArgument='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>' CausesValidation="false"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:Literal runat="server" ID="lblContactTypeName" Text='<%# Eval("ContactTypeName") %>'></asp:Literal>
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
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlContactTypeDialog" Visible="false">
                <div class="modal-backdrop show"></div>
                <div id="dlgContactType" class="modal d-block">
                    <div class=" modal-dialog modal-lg modal-dialog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">
                                    <asp:Literal runat="server" ID="lblContactTypeDialogHeader"></asp:Literal>
                                </h5>
                                <asp:HiddenField ID="hdnID" runat="server" Value="" />
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>Contact Type Name</label>
                                    <asp:TextBox runat="server" ID="txtContactTypeName" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtContactTypeName" Display="None" ErrorMessage="ContactType Name is required." />
                                </div>
                                <div class="form-group">
                                    <label>Descriptions</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" Rows="5" ID="txtDescription" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="ckbIsActive" Checked="true" Text="Is Active" />
                                </div>
                                <asp:ValidationSummary ID="vsContactTypeValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="well well-sm alert alert-danger" />
                            </div>
                            <div class="modal-footer">
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
