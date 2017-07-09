(function (win, doc, $, yumiki) {
    yumiki.webForm.validation = {
        validateInputs : function() {
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
    };
}(window, document, jQuery, yumiki));