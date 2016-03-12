/*global angular */
(function () {
    'use strict';

    function uniqueUsernameDirective($q, $timeout, $http) {
        return {
            require: 'ngModel',
            link: function (scope, el, attr, ctrl) {
                var config = scope.$eval(attr.uniqueUsername);
                var delay = config.delay;

                function doAsyncValidation(modelValue) {
                    var deferredObject = $q.defer();

                    $http.get('api/username/' + modelValue)
                        .then(function (response) {
                            console.log(response);
                            if (response.data === true) {
                                deferredObject.resolve();
                            }
                            else {
                                deferredObject.reject();
                            }
                        },
                        function (error) {
                            console.log('Unique username check error: ', error);
                            deferredObject.reject();
                        });

                    return deferredObject.promise;
                }

                var pendingValidation;

                ctrl.$asyncValidators.unique = function (modelValue, viewValue) {
                    if (pendingValidation) {
                        $timeout.cancel(pendingValidation);
                    }

                    pendingValidation = $timeout(function () {
                        pendingValidation = null;
                        return doAsyncValidation(modelValue);
                    }, delay);

                    return pendingValidation;
                };

            }
        };
    }

    angular.module('account').directive('uniqueUsername', uniqueUsernameDirective);
}());