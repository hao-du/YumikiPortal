<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="Yumiki.Web.Administration.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblGroup').DataTable();
            $('#tblGroup').wrapOverflowX();
        });

        var showGroupDialog = function (groupID) {
            $('#dlgGroup').modal();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <h2>Group Management</h2>
    <div class="well well-sm">
        <div class="btn-group">
            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="New" OnClientClick="showGroupDialog('')" OnClick="btnAdd_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
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
                            <asp:Literal runat="server" ID="lblGroupName" Text='<%# Eval("GroupName") %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Descriptions") %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="ckbIsActive" Checked='<%# (bool)Eval("IsActive") ? true : false %>' Enabled="false" />
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblModifyDate" Text='<%# Eval("LastUpdateDateUI") %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn-link" Text="Edit" OnClientClick='showGroupDialog("<%# Eval("ID") %>")' OnClick="btnEdit_Click"></asp:LinkButton>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubContentContainer" runat="server">
    <asp:UpdatePanel runat="server" ID="upnlGroupDialog">
        <ContentTemplate>
            <div id="dlgGroup" class="modal fade" role="dialog">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Modal Header</h4>
                            <asp:HiddenField ID="hdnID" runat="server" Value="" />
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblGroupName" Text="Group Name"></asp:Label>
                                <asp:TextBox runat="server" ID="txtGroupName" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblDescription" Text="Description"></asp:Label>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescription" CssClass="form-control form-area"></asp:TextBox>
                            </div>
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="ckbIsActive" Checked="true" Text="Is Active" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDialogSave" runat="server" Text="Save" CssClass="btn btn-default" data-dismiss="modal" OnClick="btnDialogSave_Click" />
                            <asp:Button ID="btnDialogClose" runat="server" Text="Close" CssClass="btn btn-default" data-dismiss="modal" OnClick="btnDialogClose_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
