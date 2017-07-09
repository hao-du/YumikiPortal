(function (win, doc, $, yumiki) {
    yumiki.angular = {
        //Common Directive
        datetimepicker: angular.module('yumiki-module', [])
            //Bind Datetimepicker with Angular
            .directive('datetimepicker', function () {
                return {
                    require: '?ngModel',
                    restrict: 'A',
                    scope: {
                        datetimeformat: '=', //Parameter
                    },
                    link: function (scope, element, attrs, ngModel) {

                        if (!ngModel) return; // do nothing if no ng-model

                        ngModel.$render = function () {
                            element.find('input').val(ngModel.$viewValue || '');
                        }

                        element.datetimepicker({
                            format: scope.datetimeformat,
                            ignoreReadonly: true,
                        });


                        element.on('dp.change', function (value) {
                            scope.$apply(read);
                        });

                        read();

                        function read() {
                            var value = element.find('input').val();
                            ngModel.$setViewValue(value);
                        }
                    }
                }
            }),
    };
}(window, document, jQuery, yumiki));