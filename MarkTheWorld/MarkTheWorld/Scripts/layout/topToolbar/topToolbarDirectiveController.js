/* global module */
'use strict';

function topToolbarDirectiveController(userService, accountService, myProfileService, facebookLoginService,
                                       sideNavEventService, toastService, filterMapService, toplistService) {
    var vm = this;

    vm.user = userService;

    vm.openRightSideNav = function (sideNavAction) {
        sideNavEventService.emit({action: sideNavAction});
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

    vm.filterMap = function (ev) {
        filterMapService.showDialog(ev);
    };

    vm.toplist = function (ev) {
        toplistService.showDialog(ev);
    };
}

module.exports = ['userService', 'accountService', 'myProfileService', 'facebookLoginService', 'sideNavEventService', 'toastService',
    'filterMapService', 'toplistService', topToolbarDirectiveController];