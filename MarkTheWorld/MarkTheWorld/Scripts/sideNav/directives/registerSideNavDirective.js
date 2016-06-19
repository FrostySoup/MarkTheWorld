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
            controller: function ($mdSidenav, accountService, simpleModalService, countries) {
                var vm = this;

                vm.requesting = false;
                vm.requestError = undefined;
                vm.countries = countries;

                vm.register = function (registerForm, registerData) {
                    if (!registerForm.$valid) {
                        return;
                    }

                    vm.requesting = true;
                    vm.requestError = undefined;

                    accountService.register(registerData).then(
                        function () {
                            simpleModalService.showModal('Success!', 'Welcome ' + registerData.username + '!');
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