(function (win, doc, $, yumiki) {
    yumiki.message = {
        //constant string for displaying
        constError: 'Error',
        constWarn: 'Warning',
        constInfo: 'Information',

        //Show message dialog in client side.
        clientMessage: function (message, details, logType) {
            $("#txtDialogTitle").empty();
            switch (logType) {
                case 'ERROR':
                    $("#txtDialogTitle").append(constError);
                    break;
                case 'INFO':
                    $("#txtDialogTitle").append(constInfo);
                    break;
                case 'WARN':
                    $("#txtDialogTitle").append(constWarn);
                    break;
                default:
                    $("#txtDialogTitle").append('Message from System...');
                    break;
            }

            $("#txtMessageDetails").empty();
            if (details == null || details == undefined || details == "") {
                $("#btnMessageDetails").hide();
            }
            else {
                $("#btnMessageDetails").show();
                $("#txtMessageDetails").append("<p>" + details + "</p>");
            }

            $("#txtMessage").empty();
            if (typeof message == 'string') {
                $("#txtMessage").append("<p>" + message + "</p>");
            }
            else {
                $("#txtMessage").append("<p>" + message.ExceptionMessage + "</p>");
            }

            $("#dlgMessageDialog").modal({ backdrop: 'static' });

            //If session expired, refresh page to navigate to Login screen.
            if (message.indexOf(yumiki.noSession) >= 0) {
                location.href = location.href;
            }
        },

        //Display/Hide Loading Dialog.
        displayLoadingDialog: function (show) {
            if (show) {
                $('#dlgLoadingBar').modal({ backdrop: 'static' });
            }
            else {
                $('#dlgLoadingBar').modal('hide');
            }
        },
    };
}(window, document, jQuery, yumiki));