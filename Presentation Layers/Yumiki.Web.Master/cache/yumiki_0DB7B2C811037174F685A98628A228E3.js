﻿(function(n,t,i,r){r.wellCovered={onListSubmit:function(n){var t=i("#"+n);t.submit()},onFormSubmit:function(n){if(!n)return!1;i("#action").val(n);var t=i("#action").closest("form");t.submit()},initDateTimePicker:function(n,t,r){n&&i(".yumiki-control-time").datetimepicker({format:n,ignoreReadonly:!0});r&&i(".yumiki-control-datetime").datetimepicker({format:r,ignoreReadonly:!0});t&&i(".yumiki-control-date").datetimepicker({format:t,ignoreReadonly:!0})}}})(window,document,jQuery,yumiki);
