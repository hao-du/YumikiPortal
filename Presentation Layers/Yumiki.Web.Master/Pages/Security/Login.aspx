<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Yumiki.Web.Master.Pages.Security.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script>
        //Validation for asp.net controls on client side.
        var startUserValidation = function () {
            var isValid = true;
            //Reset all form group css by removing "has-error has-feedback" to fix issue "One control has many validators"
            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                $(control).closest(".form-group").removeClass("has-error has-feedback");
            }

            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                ValidatorValidate(control);
                if (!control.isvalid) {
                    isValid = false;
                    $(control).closest(".form-group").addClass("has-error has-feedback");
                }
            }

            if (!isValid) {
                return false;
            }
            else {
                return true;
            }
        }

        var initPicker = function () {
            $('.selectpicker').selectpicker({
                style: 'btn-default'
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <div class="container">
        <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
        <h2 class="text-uppercase">Welcome to Yumiki Portal!</h2>
        <div class="well well-sm">
            <div class="btn-group">
                <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="btn btn-primary" OnClientClick="startUserValidation()" OnClick="btnLogin_Click" CausesValidation="true" />
            </div>
        </div>
        <asp:ValidationSummary ID="vsUserValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="well well-sm alert alert-danger" />
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>User Name</label>
                    <asp:TextBox runat="server" ID="txtUserLoginName" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserLoginName" Display="None" ErrorMessage="User Name is required." />
                </div>
                <div class="form-group">
                    <label>Password</label>
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Password is required." />
                </div>
                <div class="form-group">
                    <blockquote>
                        <asp:LinkButton runat="server" ID="btnForgotPassword" Text="Forgot Password?" OnClick="btnForgotPassword_Click" CausesValidation="false"></asp:LinkButton>
                    </blockquote>
                </div>
            </div>
            <div class="col-md-8">
                <blockquote>
                    <p class="text-info">Need to activate your account?</p>
                    <p class="text-muted">Please send an email to <a href="yumiki.help@gmail.com">Yumiki Support</a> to get activation code.</p>
                    <p class="bg-info paragraph-padding">
                        Yumiki Portal contains vary application modules which can help managing things better and quicker such as task/time/expense module etc... and is able to expend or add more modules to adapt your business needs.
                        <br />
                        <br />
                        Portal is applicable for a small business or personal. If you need any further information, please contact us at <a href="yumiki.help@gmail.com">Yumiki Support</a> to get better knowlegde on our products.
                    </p>

                    <footer>&nbsp;© <% Yumiki.Commons.Helpers.DateTimeExtension.GetSystemDatetime().Year.ToString(); %> - Yumiki Portal.</footer>
                </blockquote>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubContentContainer" runat="server">
</asp:Content>

