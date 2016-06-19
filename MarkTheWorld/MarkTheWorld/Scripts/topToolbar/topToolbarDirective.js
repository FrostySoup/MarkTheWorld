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
            controller: function (userService, accountService, myProfileService, facebookLoginService, sideNavEventServiceService, toastService) {
                var vm = this;

                vm.user = userService;

                vm.url = '/Scripts/topToolbar/directives/accountMenuItemLoggedDirective.html';

                vm.openRightSideNav = function (sideNavAction) {
                    sideNavEventServiceService.emit({ action: sideNavAction });
                };

                vm.logout = function (ev) {
                    accountService.logout(ev).then(
                        function (success) {
                            toastService.showToast('Logged out successfully', 5000);
                        },
                        function (error) {
                            console.log(error);
                        }
                    );
                };

                vm.myProfile = function (ev) {
                    myProfileService.showDialog(ev);
                };

                vm.facebookLogin = function () {
                    facebookLoginService.login().then(
                        function (success) {
                            toastService.showToast('Welcome back, ' + success.username, 5000);
                        },
                        function (error) {
                            console.log(error);
                        }
                    );
                };
            }
        };
    }

    angular.module('topToolbar').directive('topToolbar', topToolbarDirective);
}());