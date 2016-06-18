/*global angular */
/*global FB */
(function () {
    'use strict';

    function topToolbarDirective() {
        return {
            templateUrl: '/Scripts/topToolbar/topToolbarDirective.html',
            scope: {
                action: "="
            },
            bindToController: true,
            controllerAs: 'vm',
            restrict: 'E',
            replace: true,
            controller: function (userService, accountService, myProfileService, $mdSidenav) {
                var vm = this;

                vm.user = userService;

                vm.url = '/Scripts/topToolbar/directives/accountMenuItemLoggedDirective.html';

                vm.openRightSideNav = function (action) {
                    vm.action = action;
                    $mdSidenav('right_side_nav').open();
                };

                vm.logout = function (ev) {
                    accountService.logout(ev);
                };

                vm.myProfile = function (ev) {
                    myProfileService.showDialog(ev);
                };

                vm.fbLogin = function () {
                    FB.login(function(response) {
                        console.log(response);
                    });
                };

                vm.fbStatus = function () {
                    FB.getLoginStatus(function(response) {
                        console.log(response);
                    });
                };

                vm.fbLogout = function () {
                    FB.logout(function(response) {
                        console.log(response);
                    });
                };

                vm.fbMe = function () {
                    FB.api('/me', function(response) {
                        console.log( response);
                    });
                };
            }
        };
    }

    angular.module('topToolbar').directive('topToolbar', topToolbarDirective);
}());