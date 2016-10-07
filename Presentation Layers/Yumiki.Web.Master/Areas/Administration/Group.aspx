<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="Yumiki.Web.Administration.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblGroup').DataTable();
            $('#tblGroup').wrapOverflowX();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <h1>Group Management</h1>
    <div class="row">
        <div class="col-md-10">
            <asp:Repeater runat="server" ID="rptGroup">
                <HeaderTemplate>
                        <table id="tblGroup" class="table table-striped table-bordered">
                            <thead>
                                <tr>
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
                            <asp:Literal runat="server" ID="lblGroupName" Text='<%# Eval("GroupName") %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Descriptions") %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="ckbIsActive" Checked='<%# (bool)Eval("IsActive") ? true : false %>' Enabled="false"/>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblModifyDate" Text='<%# Eval("LastUpdateDateUI") %>'></asp:Literal>
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
