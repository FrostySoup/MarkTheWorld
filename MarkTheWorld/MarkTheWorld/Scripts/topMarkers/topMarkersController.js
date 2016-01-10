/*global angular */
(function () {
    'use strict';

    function topMarkersController($scope, $mdDialog, $http, accountService, mapService, toastService) {

        $scope.toplist = {};

        $http.get('/api/topList/').
            success(function (data) {
                if (data) {
                    $scope.toplist = data;
                }
            });

        $scope.cancel = function () {
            $mdDialog.hide();
        };

        $scope.userMap = function (mapUser) {
            accountService.setMapUser(mapUser);
            mapService.updateMap();
            $mdDialog.hide();
            toastService.showToast('Showing ' + mapUser + ' spots', 5000);
        };
    }
    angular.module('topMarkers').controller('topMarkersController', topMarkersController);
}());