/*global angular */
(function () {
    'use strict';

    function loginSideNavDirective() {
        return {
            templateUrl: '/Scripts/sideNav/directives/loginSideNavDirective.html',
            scope: {
                switchPanel: "&"
            },
            bindToController: true,
            controllerAs: 'vm',
            restrict: 'E',
            controller: function ($mdSidenav, accountService) {
                var vm = this;

                vm.requesting = false;
                vm.requestError = undefined;

                vm.login = function (loginForm, loginData) {
                    if (!loginForm.$valid) {
                        return;
                    }

                    vm.requesting = true;
                    vm.requestError = undefined;

                    accountService.login(loginData).then(
                        function () {
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

    angular.module('sideNav').directive('loginSideNav', loginSideNavDirective);
}());