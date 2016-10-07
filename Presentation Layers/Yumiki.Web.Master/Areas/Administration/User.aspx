﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Yumiki.Web.Administration.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblUser').DataTable();
            $('#tblUser').wrapOverflowX();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <h1>Group Management</h1>
    <hr />
    <div class="row">
        <div class="col-md-10">
            <asp:Repeater runat="server" ID="rptUsers">
                <HeaderTemplate>
                        <table id="tblUser" class="table table-striped table-bordered">
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
                            <asp:Literal runat="server" ID="lblUserName" Text='<%# Eval("GroupName") %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Descriptions") %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="ckbIsActive" Checked='<%# (bool)Eval("IsActive") ? true : false %>' />
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblModifyDate" Text='<%# Eval("UpdateDate") %>'></asp:Literal>
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
