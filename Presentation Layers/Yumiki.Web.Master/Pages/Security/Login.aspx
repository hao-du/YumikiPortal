<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="Login.aspx.cs" inherits="Yumiki.Web.Master.Pages.Security.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script>
        //Validation for asp.net controls on client side.
        var startUserValidation = function () {
            var isValid = true;
            //Reset all form group css by removing "has-danger" to fix issue "One control has many validators"
            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                $(control).closest(".form-group").removeClass("has-danger");
            }

            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                ValidatorValidate(control);
                if (!control.isvalid) {
                    isValid = false;
                    $(control).closest(".form-group").addClass("has-danger");
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
        <div class="row justify-content-md-center">
            <div class="col-12 col-lg-8 col-xl-6">
                <h1 class="text-uppercase">Yumiki Portal</h1>
                <div class="card bg-secondary mb-3">
                    <div class="card-body p-2">
                        <div class="btn-group">
                            <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="btn btn-primary" OnClientClick="startUserValidation()" OnClick="btnLogin_Click" CausesValidation="true" />
                        </div>
                    </div>
                </div>
                <asp:ValidationSummary ID="vsUserValidationSummary" DisplayMode="List" EnableClientScript="true" ShowSummary="true" ShowMessageBox="false" ShowValidationErrors="true" runat="server" CssClass="alert alert-danger" />
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
                        <asp:LinkButton runat="server" ID="btnForgotPassword" Text="Forgot Password?" OnClick="btnForgotPassword_Click" CausesValidation="false" Visible="false"></asp:LinkButton>
                    </blockquote>
                </div>
                © <%=Yumiki.Commons.Helpers.DateTimeExtension.GetLocalSystemDatetime().Year%> - Yumiki Portal.
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubContentContainer" runat="server">
</asp:Content>

