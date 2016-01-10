/*global angular */
(function () {
    'use strict';

    function squareDetailsController($scope, $mdDialog, markers, accountService, mapService, toastService) {
        angular.forEach(markers, function (value) {
            value.date = new RegExp("[0-9-]*").exec(value.date)[0];
        });

        $scope.markers = markers;

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.userMap = function (mapUser) {
            accountService.setMapUser(mapUser);
            mapService.updateMap();
            $mdDialog.hide();
            toastService.showToast('Showing ' + mapUser + ' spots', 5000);
        };
    }

    angular.module('squareDetails').controller('squareDetailsController', squareDetailsController);
}());