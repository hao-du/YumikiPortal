<%@ page title="" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="Maintenance.aspx.cs" inherits="Yumiki.Web.Administration.Maintenance" %>

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
        <h1>Maintenance</h1>
        <div class="card">
            <div class="card-header bg-danger text-white">
                Shutdown server...
            </div>
            <div class="card-body">
                <button type="button" onclick="confirmation()" class="btn btn-danger">Shutdown Server...</button>
                <asp:Button runat="server" ID="btnShutdownServer" OnClick="btnShutdownServer_Click" CssClass="d-none" />
            </div>
        </div>

        <div class="card">
            <div class="card-header bg-primary text-white">
                Backup Database...
            </div>
            <div class="card-body">
                <div class="form-group">
                    <label>Database Names</label>
                    <asp:TextBox ID="txtDatabases" runat="server" TextMode="MultiLine" CssClass="form-control form-area" Text="DB_Administration,DB_Master,DB_MoneyTrace,DB_WellCovered,DB_OnTime,DB_Shopper"></asp:TextBox>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubContentContainer" runat="server">
    <div id="dlgConfirmationDialog" class="modal fade">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmation</h5>
                </div>
                <div class="modal-body">
                    <p>
                        Are you sure to shut down Web Server?
                    </p>
                </div>
                <div class="modal-footer bg-dark">
                    <button type="button" class="btn btn-primary" onclick="sendShutdownRequest();">Yes</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
