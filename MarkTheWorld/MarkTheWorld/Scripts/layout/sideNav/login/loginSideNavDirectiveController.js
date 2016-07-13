/* global module */
'use strict';

function loginSideNavDirectiveController ($mdSidenav, accountService, toastService) {
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
                toastService.showToast('Welcome back, ' + loginData.username, 5000);
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

module.exports = ['$mdSidenav', 'accountService', 'toastService', loginSideNavDirectiveController];