/*global angular */
(function () {
    'use strict';

    function myProfilePictureService($mdDialog, $q) {

        return {
            showDialog: function () {
                return $mdDialog.show({
                    controller: 'myProfilePictureController',
                    controllerAs: 'vm',
                    templateUrl: 'scripts/myProfile/myProfilePicture/myProfilePicture.html',
                    parent: angular.element(document.body),
                    fullscreen: true,
                    focusOnOpen: false,
                    clickOutsideToClose: true
                });
            },

            formImage: function(src) {
                var deferred = $q.defer();
                var image = new Image();

                if (src.substring(0, 4).toLowerCase() === 'http') {
                    image.crossOrigin = 'anonymous';
                }

                image.onerror = function () {
                    deferred.reject();
                };

                image.onload = function () {
                    deferred.resolve(image);
                };
                image.src = src;

                return deferred.promise;
            }
        };
    }

    angular.module('myProfilePicture').factory('myProfilePictureService', myProfilePictureService);
}());