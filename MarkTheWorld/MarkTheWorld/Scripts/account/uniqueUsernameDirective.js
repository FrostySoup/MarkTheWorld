/*global angular */
(function () {
    'use strict';

    function uniqueUsernameDirective($q, $timeout, $http) {
        return {
            require: 'ngModel',
            template: "<div></div>",
            link: function (scope, el, attr, ctrl) {
                ctrl.$asyncValidators.unique = function (modelValue, viewValue) {
                    var deferredObject = $q.defer();

                    $http.get('api/username/' + modelValue)
                        .then(function (response) {
                            if (response.data === true) {
                                deferredObject.resolve();
                            }
                            else {
                                deferredObject.reject();
                            }
                        },
                        function (error) {
                            console.log("Error checking unique username", error);
                            deferredObject.reject();
                        });

                    return deferredObject.promise;
                };

            }
        };
    }

    angular.module('account').directive('uniqueUsername', uniqueUsernameDirective);
}());