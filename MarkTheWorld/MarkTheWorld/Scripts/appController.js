/*global angular */
(function () {
    'use strict';

    function AppController($scope, accountService, claimSpotService) {
        accountService.appStartUpLoginCheck();
        $scope.temp = function (ev) {
            claimSpotService.showDialog(ev);
        };
    }

    angular.module('markTheWorld').controller('AppController', AppController);
}());