/*global angular */
(function () {
    'use strict';

    function claimSpotService($mdDialog) {
        return {
            showDialog: function (ev) {
                $mdDialog.show({
                    controller: 'claimSpotController',
                    controllerAs: 'vm',
                    templateUrl: 'scripts/claimSpot/claimSpot.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    fullscreen: true,
                    focusOnOpen: false,
                    clickOutsideToClose: true
                });
            }
        };
    }

    angular.module('claimSpot').factory('claimSpotService', claimSpotService);
}());