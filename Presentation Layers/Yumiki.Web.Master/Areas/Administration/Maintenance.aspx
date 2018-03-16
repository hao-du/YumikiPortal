<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maintenance.aspx.cs" Inherits="Yumiki.Web.Administration.Maintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContainer" runat="server">
    <script type="text/javascript">
        var confirmation = function () {
            $('#dlgConfirmationDialog').modal({ backdrop: 'static' });
            return false;
        }

        var sendShutdownRequest = function () {
            $('#<%=btnShutdownServer.ClientID%>').click();
            $('#dlgConfirmationDialog').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentContainer" runat="server">
    <div class="container">
        <h2>Maintenance</h2>
        <div class="panel-group">
            <div class="panel panel-danger">
                <div class="panel-heading">Shutdown server...</div>
                <div class="panel-body">
                    <button type="button" onclick="confirmation()" class="btn btn-danger">Shutdown Server...</button>
                    <asp:Button runat="server" ID="btnShutdownServer" OnClick="btnShutdownServer_Click" CssClass="hidden" />
                </div>
            </div>

            <div class="panel panel-primary">
                <div class="panel-heading">Backup Database...</div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Database Names</label>
                        <asp:TextBox ID="txtDatabases" runat="server" TextMode="MultiLine" CssClass="form-control form-area" Text="DB_Administration,DB_Master,DB_MoneyTrace,DB_WellCovered"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="valBackup" ControlToValidate="txtDatabases" Display="Dynamic" ErrorMessage="Database Name is required." />
                    </div>
                    <div class="form-group">
                        <label>Backup Media Folder Zip Names</label>
                        <asp:TextBox ID="txtBackupMediaZipName" runat="server" CssClass="form-control" Text="MediaFiles.zip"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="valBackup" ControlToValidate="txtBackupMediaZipName" Display="Dynamic" ErrorMessage="Zip name is required." />
                    </div>

                    <asp:Button runat="server" ID="btnBackupServer" OnClick="btnBackupServer_Click" Text="Backup" class="btn btn-primary" ValidationGroup="valBackup" />
                </div>
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
