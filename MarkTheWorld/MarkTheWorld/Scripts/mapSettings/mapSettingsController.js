/*global angular */
(function () {
    'use strict';

    function mapSettingsController($scope, $mdDialog, mapService, accountService, toastService) {

        if (accountService.getMapUser() === null || accountService.getMapUser() === 'all') {
            $scope.selection = 'all';
        } else if (accountService.getMapUser() === accountService.getLoggedUser()) {
            $scope.selection = 'own';
        } else {
            $scope.selection = 'user';
            $scope.username = accountService.getMapUser();
        }

        $scope.cancel = function () {
            $mdDialog.hide();
        };

        $scope.confirm = function () {
            var mapUser;
            var message;
            if ($scope.selection === 'own') {
                mapUser = accountService.getLoggedUser();
                message = 'Showing only your spots';
            } else if ($scope.selection === 'all') {
                mapUser = 'all';
                message = 'Showing all spots';
            } else {
                mapUser = $scope.username;
                message = 'Showing ' + $scope.username + ' spots';
            }

            accountService.setMapUser(mapUser);
            mapService.updateMap();
            $mdDialog.cancel();
            toastService.showToast(message, 5000);
        };
    }
    angular.module('mapSettings').controller('mapSettingsController', mapSettingsController);
}());