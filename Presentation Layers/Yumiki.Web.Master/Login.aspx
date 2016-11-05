<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Yumiki.Web.Master.Login" %>

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
        <h2>Welcome to Yumiki Portal!</h2>


        <div class="well well-sm">
            <div class="btn-group">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClientClick="startUserValidation()" OnClick="btnLogin_Click" CausesValidation="true" />
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
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Password is required." />
                </div>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-7">
                AAAAAAAAAAA AAAAAA AAAAAA AAAAAAAAA AAAAAA AAAAAAA AAAAAA  AAAAAA AAAAAAAA AAAAAA AAAAAAAAA AAAAAAA AAAAAAAAAAAAA
                AAAAA AAAA AAAAA AAA AAAAAAAAA AAA AA AAA AA AAAA AAA AAA AAAAAAA A AA AA AAAAA AAA AAAAAAAAAA AAAAAAAAAAAAA
                AA  AA AAA AAAAA AAAA AA A AAAAAA  AAAA  A  AAAA A AA  AAAAA AA AAA  AAA AAAAAAA A AAAA A AAAAAAAAAAA AA AAAA AAAA AAA AAA  AAAAAA
                 AAA AAAAAA AAAAAAAAAAA  AAA AAAAAA AAAA AAAA AAAA AAA AAA AAAAAAAA AAA AAAAAAA AAAAAA AAA AAAAAA AAAAAA AAAAAAA AAAAAAAAAAAAAA
                AAA AAAAA  AAAAAAA AAAAAAAAAA A AAA AAA AA AA AA A  AAAAAAAAA A A AA  AAA AA AA AAAA AA AA AAAA AAA AA A AAAA  A AAAAAA
                AAAA AAAA AA AAA AA AA AAA A AA A AA  AAA AAAAAAA AAAAAAAA AA AAAA AAAA AAAAAAA AAAA AAAA AAAAAAAA AAA AAA A AAAA AAAAA AAA
                AAA A AA AA AAAAA A AAA AAAAA A  AAA    AAAAA  AAAAAAA   
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

