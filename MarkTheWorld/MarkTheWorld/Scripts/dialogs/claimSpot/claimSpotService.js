/*global module */
'use strict';

function claimSpotService($mdDialog, $q, $http, userService, Upload) {
    return {
        showDialog: function (ev) {
            $mdDialog.show({
                controller: 'claimSpotController',
                controllerAs: 'vm',
                templateUrl: '/Scripts/dialogs/claimSpot/claimSpot.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                fullscreen: true,
                focusOnOpen: false,
                clickOutsideToClose: true
            });
        },

        claim: function (file, message) {
            var deferredObject = $q.defer();

            if (!file) {
                $http.post('/api/Dot',
                    {
                        "token": userService.token,
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
            } else {
                Upload.upload({
                    url: 'api/dotPhoto',
                    data: {
                        file: file,
                        token: userService.token,
                        lat: userService.currentPosition.lat,
                        lng: userService.currentPosition.lng,
                        message: message
                    }
                }).then(
                    function (success) {
                        deferredObject.resolve(success);
                    },
                    function (error) {
                        deferredObject.reject(error);
                    }
                );
            }

            return deferredObject.promise;
        }
    };
}

module.exports = ['$mdDialog', '$q', '$http', 'userService', 'Upload', claimSpotService];
