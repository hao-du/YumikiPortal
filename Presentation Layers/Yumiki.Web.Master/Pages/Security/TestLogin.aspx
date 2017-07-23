<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="TestLogin.aspx.cs" Inherits="Yumiki.Web.Master.Pages.Security.TestLogin" %>

<!DOCTYPE html>
<html>
<head>
    <title>Test Login Page</title>
    <script>
        //Validation for asp.net controls on client side.
        var startUserValidation = function () {
            var isValid = true;
            //Reset all form group css by removing "has-error has-feedback" to fix issue "One control has many validators"
            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
            }

            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                ValidatorValidate(control);
                if (!control.isvalid) {
                    isValid = false;
                }
            }

            if (!isValid) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body>
    <form runat="server" defaultfocus="txtUserLoginName">
        <h1>Sign In</h1>
        <asp:TextBox runat="server" ID="txtUserLoginName" Text="Administrator"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserLoginName" Display="None" ErrorMessage="User Name is required." />
        <br />
        <asp:ValidationSummary ID="vsUserValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" />
        <asp:Button ID="btnLogin" runat="server" Text="Sign In" OnClientClick="startUserValidation()" OnClick="btnLogin_Click" CausesValidation="true" />
        <hr />
        <h1>Testing Account</h1>
        <ul>
            <asp:Repeater runat="server" ID="rptUserNames">
                <ItemTemplate>
                    <li>
                        <asp:LinkButton runat="server" ID="btnLoginLink" Text='<%# Eval("UserLoginName") %>'  OnClick="btnLogin_Click"></asp:LinkButton>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </form>
</body>
</html>

