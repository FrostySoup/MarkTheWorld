/*global angular */
(function () {
    'use strict';

    function myProfileController($mdDialog, myProfileService, myProfilePictureService) {
        var vm = this;

        vm.cancel = function () {
            $mdDialog.cancel();
        };

        vm.myProfilePicture = function () {
            myProfilePictureService.showDialog().finally(
                function () {
                    myProfileService.showDialog();
                }
            );
        };

        myProfileService.getProfileData().then(
            //TODO: [preRelease] there should be an elegant preloader and nice failure handling
            function (success) {
                vm.profileData = success.data;
            },
            function (error) {
                console.log(error);
            }
        );
    }

    angular.module('myProfile').controller('myProfileController', myProfileController);
}());