/* global module */
'use strict';

function completeSideNavDirectiveController ($mdSidenav, $scope, facebookLoginService, $q, toastService, statesUS, countries) {
    var vm = this;

    vm.requesting = false;
    vm.requestError = undefined;
    vm.countries = countries;
    vm.statesUS = statesUS;
    vm.registerData = {};

    var fbResponse = {};

    // the fix for strange dropdown position
    vm.statesDropFix = function () {
        var deferredObject = $q.defer();
        deferredObject.resolve();
        return deferredObject.promise;
    };

    $scope.$watch('vm.extraData', function (extraData) {
        if (angular.isDefined(extraData)) {
            vm.registerData.username = extraData.apiResponse.username;
            fbResponse.accessToken = extraData.fbResponse.authResponse.accessToken;
            fbResponse.userID = extraData.fbResponse.authResponse.userID;
        }
    });

    vm.completeRegistration = function (registerForm, registerData) {
        if (!registerForm.$valid) {
            return;
        }

        vm.requesting = true;
        vm.requestError = undefined;

        facebookLoginService.completeRegistration(registerData, fbResponse).then(
            function () {
                vm.close();
                toastService.showToast('Welcome, ' + registerData.username, 5000);
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

module.exports = ['$mdSidenav', '$scope', 'facebookLoginService', '$q', 'toastService', 'statesUS', 'countries', completeSideNavDirectiveController];