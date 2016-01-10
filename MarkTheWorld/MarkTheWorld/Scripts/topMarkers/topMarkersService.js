/*global angular */
(function () {
    'use strict';

    function topMarkersService($mdDialog) {
        return {
            showDialog : function () {
                $mdDialog.show({
                    controller: 'topMarkersController',
                    templateUrl: 'scripts/topMarkers/topMarkers.html',
                    parent: angular.element(document.body),
                    clickOutsideToClose: true
                });
            }
        };
    }

    angular.module('topMarkers').factory('topMarkersService', topMarkersService);
}());