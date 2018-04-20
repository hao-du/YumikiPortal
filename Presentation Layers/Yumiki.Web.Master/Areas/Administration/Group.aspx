<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="Group.aspx.cs" inherits="Yumiki.Web.Administration.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script src="/clients/scripts/yumiki-webform-validation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <asp:UpdatePanel runat="server" ID="upnlGroup">
        <ContentTemplate>
            <div class="container">
                <h1>Group</h1>
                <!--hdnGlobalGroupID: To keep group id for User Assignment tab-->
                <asp:HiddenField ID="hdnGlobalGroupID" runat="server" Value="" />
                <ul class="nav nav-tabs">
                    <li runat="server" id="liGroupList" class="nav-item">
                        <asp:LinkButton runat="server" ID="btnGroupListTab" Text="Group List" CausesValidation="false" OnClick="linkButton_Click"></asp:LinkButton>
                    </li>
                    <li runat="server" id="liUserAssignment" class="nav-item" visible="false">
                        <asp:LinkButton runat="server" ID="btnUserAssignmentTab" Text="User Assignment" CausesValidation="false" OnClick="linkButton_Click"></asp:LinkButton>
                    </li>
                    <li runat="server" id="liPrivilegeAssignment" class="nav-item" visible="false">
                        <asp:LinkButton runat="server" ID="btnPrivilegeAssignmentTab" Text="Privilege Assignment" CausesValidation="false" OnClick="linkButton_Click"></asp:LinkButton>
                    </li>
                </ul>
                <asp:MultiView runat="server" ID="mtvGroupTabs" ActiveViewIndex="0">
                    <asp:View ID="vwGroupListTab" runat="server">
                        <div class="card bg-secondary">
                            <div class="card-body">
                                <div class="btn-group">
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="New" OnClick="btnAdd_Click" CausesValidation="false" />
                                    <asp:Button ID="btnDisplayInactiveGroups" runat="server" CssClass="btn btn-default" Text="Show Inactive Groups" OnClick="btnDisplayInactiveGroups_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive-lg">
                            <asp:Repeater runat="server" ID="rptGroup">
                                <HeaderTemplate>
                                    <table id="tblGroup" class="table table-striped table-bordered table-hover">
                                        <thead class="thead-dark">
                                            <tr>
                                                <th></th>
                                                <th>Group Name</th>
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
                                            <asp:LinkButton ID="btnEdit" ToolTip="Edit" runat="server" CssClass="btn-link fa fa-edit" Text="" OnClick="btnEdit_Click" CommandArgument='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>' CausesValidation="false"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnShowOtherTabs" runat="server" CssClass="btn-link" Text='<%# Eval(Yumiki.Entity.Administration.TB_Group.FieldName.GroupName) %>' OnClick="btnShowOtherTabs_Click" CommandArgument='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID).ToString().ToString()%>' CausesValidation="false"></asp:LinkButton>
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
                    </asp:View>
                    <asp:View ID="vwUserAssignmentTab" runat="server">
                        <div class="card bg-secondary">
                            <div class="card-body">
                                <div class="btn-group">
                                    <asp:Button ID="btnDisplayUser" runat="server" CssClass="btn btn-primary" Text="Show Unassigned Users" OnClick="btnDisplayUser_Click" CausesValidation="false" />
                                    <asp:Button ID="btnAssignUnassignUser" runat="server" CssClass="btn btn-primary" Text="Assign" OnClick="btnAssignUnassignUser_Click" CausesValidation="false" />
                                </div>
                                <h4 class="float-md-right text-white">
                                    <asp:Literal runat="server" ID="lblDisplayUserDescription" Text="Unassigned User List"></asp:Literal>
                                </h4>
                            </div>
                        </div>
                        <div class="table-responsive-lg">
                            <asp:Repeater runat="server" ID="rptUser">
                                <HeaderTemplate>
                                    <table id="tblUser" class="table table-striped table-bordered table-hover">
                                        <thead class="thead-dark">
                                            <tr>
                                                <th></th>
                                                <th>Login Name</th>
                                                <th>Full Name</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="ckbSelect" Text="" />
                                                    <asp:HiddenField runat="server" ID="hdnUserID" Value='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>' />
                                                </td>
                                                <td>
                                                    <asp:Literal runat="server" ID="lblUserName" Text='<%# Eval("UserLoginName") %>'></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal runat="server" ID="lblFullName" Text='<%# Eval("FullName") %>'></asp:Literal>
                                                </td>
                                            </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                        </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:View>
                    <asp:View ID="vwPrivilegeAssignmentTab" runat="server">
                        <div class="card bg-secondary">
                            <div class="card-body">
                                <div class="btn-group">
                                    <asp:Button ID="btnDisplayPrivilege" runat="server" CssClass="btn btn-primary" Text="Show Unassigned Privileges" OnClick="btnDisplayPrivilege_Click" CausesValidation="false" />
                                    <asp:Button ID="btnAssignUnassignPrivilege" runat="server" CssClass="btn btn-primary" Text="Assign" OnClick="btnAssignUnassignPrivilege_Click" CausesValidation="false" />
                                </div>
                                <h4 class="float-md-right text-white">
                                    <asp:Literal runat="server" ID="lblDisplayPrivilegeDescription" Text="Unassigned Privilege List"></asp:Literal>
                                </h4>
                            </div>
                        </div>
                            <div class="table-responsive">
                                <asp:Repeater runat="server" ID="rptPrivilege">
                                    <HeaderTemplate>
                                        <table id="tblPrivilege" class="table table-striped table-bordered table-hover">
                                            <thead class="thead-dark">
                                                <tr>
                                                    <th></th>
                                                    <th>Privilege Name</th>
                                                    <th>Page Path</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox runat="server" ID="ckbSelect" Text="" />
                                                <asp:HiddenField runat="server" ID="hdnPrivilegeID" Value='<%# Eval(Yumiki.Commons.Dictionaries.CommonProperties.ID) %>' />
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="lblPrivilegeName" Text='<%# Eval("PrivilegeName") %>'></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="lblFullName" Text='<%# Eval("PagePath") %>'></asp:Literal>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                    </asp:View>
                </asp:MultiView>
            </div>
            <asp:Panel runat="server" ID="pnlGroupDialog" Visible="false">
                <div class="modal-backdrop show"></div>
                <div id="dlgGroup" class="modal d-block">
                    <div class="modal-dialog modal-lg modal-dialog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">
                                    <asp:Literal runat="server" ID="lblGroupDialogHeader"></asp:Literal>
                                </h5>
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
                                    <asp:TextBox runat="server" TextMode="MultiLine" Rows="5" ID="txtDescription" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="ckbIsActive" Checked="true" Text="Is Active" />
                                </div>
                                <asp:ValidationSummary ID="vsGroupValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="alert alert-danger" />
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
