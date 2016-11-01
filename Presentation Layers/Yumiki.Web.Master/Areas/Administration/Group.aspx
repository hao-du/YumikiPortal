<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="Yumiki.Web.Administration.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <asp:UpdatePanel runat="server" ID="upnlGroup">
        <ContentTemplate>
            <div class="container">
                <h2>Group Management</h2>
                <!--hdnGlobalGroupID: To keep group id for User Assignment tab-->
                <asp:HiddenField ID="hdnGlobalGroupID" runat="server" Value="" />
                <ul class="nav nav-tabs">
                    <li class="active" runat="server" id="liGroupList">
                        <asp:LinkButton runat="server" ID="btnGroupListTab" Text="Group List" CausesValidation="false" OnClick="linkButton_Click"></asp:LinkButton></li>
                    <li runat="server" id="liUserAssignment" visible="false">
                        <asp:LinkButton runat="server" ID="btnUserAssignmentTab" Text="User Assignment" CausesValidation="false" OnClick="linkButton_Click"></asp:LinkButton></li>
                </ul>
                <asp:MultiView runat="server" ID="mtvGroupTabs" ActiveViewIndex="0">
                    <asp:View ID="vwGroupListTab" runat="server">
                        <div class="well well-sm">
                            <div class="btn-group">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="New" OnClick="btnAdd_Click" CausesValidation="false" />
                                <asp:Button ID="btnDisplayInactiveGroups" runat="server" CssClass="btn btn-primary" Text="Show Inactive Groups" OnClick="btnDisplayInactiveGroups_Click" CausesValidation="false" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:Repeater runat="server" ID="rptGroup">
                                        <HeaderTemplate>
                                            <table id="tblGroup" class="table table-striped table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Group Name</th>
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
                                                    <asp:LinkButton ID="btnShowOtherTabs" runat="server" CssClass="btn-link" Text='<%# Eval(Yumiki.Entity.Administration.TB_Group.FieldName.GroupName) %>' OnClick="btnShowOtherTabs_Click" CommandArgument='<%# Eval(Yumiki.Common.Dictionary.CommonProperties.ID).ToString().ToString()%>' CausesValidation="false"></asp:LinkButton>
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
                    </asp:View>
                    <asp:View ID="vwUserAssignmentTab" runat="server">
                        <div class="well well-sm">
                            <div class="btn-group">
                                <asp:Button ID="btnUserAssign" runat="server" CssClass="btn btn-primary" Text="Assign" OnClick="btnUserAssign_Click" CausesValidation="false" />
                                <asp:Button ID="btnUserUnassign" runat="server" CssClass="btn btn-primary" Text="Unassign" OnClick="btnUserUnassign_Click" CausesValidation="false" />
                            </div>
                        </div>

                    </asp:View>
                </asp:MultiView>
            </div>
            <asp:Panel runat="server" ID="pnlGroupDialog" Visible="false">
                <div class="modal-backdrop"></div>
                <div id="dlgGroup" class="modal show" role="dialog">
                    <div class=" modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Literal runat="server" ID="lblGroupDialogHeader"></asp:Literal>
                                </h4>
                                <asp:HiddenField ID="hdnDialogGroupID" runat="server" Value="" />
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label>Group Name</label>
                                    <asp:TextBox runat="server" ID="txtGroupName" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtGroupName" Display="None" ErrorMessage="Group Name is required." />
                                </div>
                                <div class="form-group">
                                    <label>Descriptions</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescription" CssClass="form-control form-area"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="ckbIsActive" Checked="true" Text="Is Active" CssClass="checkbox" />
                                </div>
                                <asp:ValidationSummary ID="vsGroupValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="well well-sm alert alert-danger" />
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
