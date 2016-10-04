<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Yumiki.Web.Administration.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <h1>
        ADMINISTRATION USER PAGE
    </h1>
    <asp:Repeater runat="server" ID="rptUsers">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>User Name</th>
                    <th>Descriptions</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Literal runat="server" ID="lblUserName" Text='<%# Eval("GroupName") %>'></asp:Literal>
                </td>
                <td>
                    <asp:Literal runat="server" ID="lblDescription" Text='<%# Eval("Descriptions") %>'></asp:Literal>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>