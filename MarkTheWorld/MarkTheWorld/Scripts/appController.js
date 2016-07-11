/*global angular */
(function () {
    'use strict';

    function AppController($scope, accountService, claimSpotService, userService) {
        accountService.appStartUpLoginCheck();
        $scope.temp = function (ev) {
            claimSpotService.showDialog(ev);
        };

        $scope.$watch(function () {
            return userService.currentPosition;
        }, function (newVal, oldVal) {
            if (newVal !== null) {

            }
        });
    }

    angular.module('markTheWorld').controller('AppController', AppController);
}());