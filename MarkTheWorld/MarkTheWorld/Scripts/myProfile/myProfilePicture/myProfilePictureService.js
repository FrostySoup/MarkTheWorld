/*global angular */
(function () {
    'use strict';

    function myProfilePictureService($mdDialog) {

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
            }
        };
    }

    angular.module('myProfilePicture').factory('myProfilePictureService', myProfilePictureService);
}());