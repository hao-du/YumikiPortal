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
                    $("#txtDialogTitle").append(yumiki.message.constError);
                    break;
                case 'INFO':
                    $("#txtDialogTitle").append(yumiki.message.constInfo);
                    break;
                case 'WARN':
                    $("#txtDialogTitle").append(yumiki.message.constWarn);
                    break;
                default:
                    $("#txtDialogTitle").append('Message from System...');
                    break;
            }

            $("#txtMessageDetails").empty();
            if (!details) {
                $("#btnMessageDetails").hide();
            }
            else {
                $("#btnMessageDetails").show();
                $("#txtMessageDetails").append("<p>" + details + "</p>");
            }

            $("#txtMessage").empty();
            if (typeof message == 'string') {
                $("#txtMessage").append("<p>" + message + "</p>");

                yumiki.message.checkExpiredSession(message);
            }
            else {
                $("#txtMessage").append("<p>" + message.ExceptionMessage + "</p>");

                //Append the stack trace and other details
                $("#btnMessageDetails").show();
                $("#txtMessageDetails").append("<p><b>Exception Type:</b> " + message.ExceptionType + "</p>");
                $("#txtMessageDetails").append("<p><b>Stack Trace:</b> " + message.StackTrace + "</p>");

                yumiki.message.checkExpiredSession(message.ExceptionMessage);
            }

            $("#dlgMessageDialog").modal({ backdrop: 'static' });
        },

        //Check if session expired.
        checkExpiredSession : function(message) {
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