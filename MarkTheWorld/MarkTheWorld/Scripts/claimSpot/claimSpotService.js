/*global angular */
(function () {
    'use strict';

    function claimSpotService($mdDialog, $q, $http, userService) {
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
            },
            claim: function (file, message) {
                var deferredObject = $q.defer();

                $http.post('/api/Dot',
                    {
                        "username": userService.token,
                        "lat": userService.currentPosition.lat,
                        "lng": userService.currentPosition.lng,
                        "message": message
                    }).then(
                    function (success) {
                        deferredObject.resolve(success);
                    },
                    function (error) {
                        deferredObject.reject(error);
                    }
                );

                return deferredObject.promise;
            }
        };
    }

    angular.module('claimSpot').factory('claimSpotService', claimSpotService);
}());