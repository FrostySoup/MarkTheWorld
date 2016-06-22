/*global angular */
(function () {
    'use strict';

    function registerSideNavDirective() {
        return {
            templateUrl: '/Scripts/sideNav/directives/registerSideNavDirective.html',
            bindToController: true,
            controllerAs: 'vm',
            scope: {},
            restrict: 'E',
            controller: function ($mdSidenav, accountService, countries, statesUS, $q, toastService) {
                var vm = this;

                vm.requesting = false;
                vm.requestError = undefined;
                vm.countries = countries;
                vm.statesUS = statesUS;

                // the fix for strange dropdown position
                vm.statesDropFix = function () {
                    var deferredObject = $q.defer();
                    deferredObject.resolve();
                    return deferredObject.promise;
                };

                vm.register = function (registerForm, registerData) {
                    if (!registerForm.$valid) {
                        return;
                    }

                    vm.requesting = true;
                    vm.requestError = undefined;

                    accountService.register(registerData).then(
                        function () {
                            toastService.showToast('Welcome, ' + registerData.username, 5000);
                            vm.close();
                        },
                        function (error) {
                            vm.requestError = error.data;
                        }
                    ).finally(function () {
                        vm.requesting = false;
                    });

                };

                vm.close = function () {
                    $mdSidenav('right_side_nav').close();
                };
            }
        };
    }

    angular.module('sideNav').directive('registerSideNav', registerSideNavDirective);
}());