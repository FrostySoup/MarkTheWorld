/*global angular */
(function () {
    'use strict';

    function myProfilePictureController($mdDialog, Upload) {
        var vm = this;

        vm.cancel = function () {
            $mdDialog.cancel();
        };
    }

    angular.module('myProfilePicture').controller('myProfilePictureController', myProfilePictureController);
}());