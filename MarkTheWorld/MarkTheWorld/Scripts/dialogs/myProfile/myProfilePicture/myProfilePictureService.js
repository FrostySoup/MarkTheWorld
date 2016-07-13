/*global module */
'use strict';

function myProfilePictureService($mdDialog, $q) {

    return {
        showDialog: function () {
            return $mdDialog.show({
                controller: 'myProfilePictureController',
                controllerAs: 'vm',
                templateUrl: 'scripts/dialogs/myProfile/myProfilePicture/myProfilePicture.html',
                parent: angular.element(document.body),
                fullscreen: true,
                focusOnOpen: false,
                clickOutsideToClose: true
            });
        },

        formImage: function (src) {
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
        },

        fileError: function (error) {
            switch (error) {
            case 'pattern':
                return 'Only images are allowed';
            case 'minHeight':
                return 'Image dimensions should be at least 100x100';
            case 'minWidth':
                return 'Image dimensions should be at least 100x100';
            case 'maxSize':
                return 'Image should be smaller than 20MB';
            default:
                return 'Your picture couldn\'t be processed';
            }
        }
    };
}

module.exports = ['$mdDialog', '$q', myProfilePictureService];