(function (win, doc, $, yumiki) {
    yumiki.webForm.validation = {
        validateInputs : function(group) {
            var isValid = true;
            //Reset all form group css by removing "has-error has-feedback" to fix issue "One control has many validators"
            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                $(control).siblings(".form-control").removeClass("is-invalid");
            }

            for (i = 0; i < Page_Validators.length; i++) {
                var control = Page_Validators[i];
                ValidatorValidate(control);
                if (!control.isvalid && (!group || control.validationGroup == group)) {
                    isValid = false;
                    $(control).siblings(".form-control").addClass("is-invalid");
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