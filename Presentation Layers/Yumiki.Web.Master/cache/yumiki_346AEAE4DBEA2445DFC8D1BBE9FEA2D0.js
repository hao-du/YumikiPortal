﻿(function(n,t,i,r){r.wellCovered={onListSubmit:function(n){var t=i("#"+n);t.submit()},onFormSubmit:function(n){if(!n)return!1;i("#action").val(n);var t=i("#action").closest("form");t.submit()},initDateTimePicker:function(n,t,r){n&&i(".yumiki-control-time").datetimepicker({format:n,ignoreReadonly:!0});r&&i(".yumiki-control-datetime").datetimepicker({format:r,ignoreReadonly:!0});t&&i(".yumiki-control-date").datetimepicker({format:t,ignoreReadonly:!0})},initSearchEvent:function(n,t,u){i(".y-search-item").mark(i("#"+n).val(),{separateWordSearch:!0,diacritics:!0});i("#"+n).on({keypress:function(t){if(t.which==13&&i("#"+n).val())r.wellCovered.onListSubmit(u)},focus:function(){this.selectionStart=0;this.selectionEnd=this.value.length}});i("#"+t).click(function(){if(i("#"+n).val())r.wellCovered.onListSubmit(u)})}}})(window,document,jQuery,yumiki);
