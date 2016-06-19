/*global angular */
/*global FB */
(function () {
    'use strict';

    function topToolbarDirective() {
        return {
            templateUrl: '/Scripts/topToolbar/topToolbarDirective.html',
            bindToController: true,
            controllerAs: 'vm',
            scope: {},
            restrict: 'E',
            replace: true,
            controller: function (userService, accountService, myProfileService, facebookLoginService, sideNavEventServiceService) {
                var vm = this;

                vm.user = userService;

                vm.url = '/Scripts/topToolbar/directives/accountMenuItemLoggedDirective.html';

                vm.openRightSideNav = function (sideNavAction) {
                    sideNavEventServiceService.emit({ action: sideNavAction });
                };

                vm.logout = function (ev) {
                    accountService.logout(ev);
                };

                vm.myProfile = function (ev) {
                    myProfileService.showDialog(ev);
                };

                vm.facebookLogin = function () {
                    facebookLoginService.login();

                    //    .then(
                    //    function () {
                    //        ...
                    //    },
                    //    function (error) {
                    //        ...
                    //    }
                    //);
                };

                //------------------

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