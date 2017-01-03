<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maintenance.aspx.cs" Inherits="Yumiki.Web.Administration.Maintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script type="text/javascript">
        var confirmation = function () {
            $('#dlgConfirmationDialog').modal({ backdrop: 'static' });
        }

        var sendShutdownRequest = function () {
            $('<%=btnShutdownServer.ClientID%>').click();
            $('#dlgConfirmationDialog').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <div class="container">
        <h2>Group Management</h2>
        <div class="row">
            <div class="col-md-12">
                <asp:LinkButton runat="server" ID="btnShowConfirmation" Text="Shutdown Server..." CausesValidation="false" OnClientClick="confirmation()"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="btnShutdownServer" OnClick="btnShutdownServer_Click" CssClass="hidden"></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubContentContainer" runat="server">
    <div id="dlgConfirmationDialog" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Confirmation</h4>
                </div>
                <div class="modal-body">
                    <p>
                        Are you sure to shut down Web Server?
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" onclick="sendShutdownRequest();">Yes</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
