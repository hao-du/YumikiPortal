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
            <button type="button" class="btn btn-default" onclick='showGroupDialog("<%# Eval("ID") %>")'>Create New Group</button>
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
                            <a href="#" class="btn-link" onclick='showGroupDialog("<%# Eval("ID") %>")'>Edit</a>
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
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Modal Header</h4>
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
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
