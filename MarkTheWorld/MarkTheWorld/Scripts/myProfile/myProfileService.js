/*global angular */
(function () {
    'use strict';

    function myProfileService($mdDialog) {
        return {
            showDialog : function (ev) {
                $mdDialog.show({
                    controller: 'myProfileController',
                    templateUrl: 'scripts/myProfile/myProfile.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    fullscreen: true,
                    clickOutsideToClose: true
                });
            }
        };
    }

    angular.module('myProfile').factory('myProfileService', myProfileService);
}());