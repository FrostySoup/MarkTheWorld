/*global angular */
(function () {
    'use strict';

    function myProfileController($mdDialog, myProfileService) {
        var vm = this;
        vm.cancel = function () {
            $mdDialog.cancel();
        };

        myProfileService.getProfileData().then(
            function (success) {
                console.log(success);
                vm.flagAddress = success.data.flagAdress;
            },
            function (error) {
                //TODO: [polishing] there should be an elegant preloader and nice failure handling
                console.log(error);
            }
        );
    }

    angular.module('myProfile').controller('myProfileController', myProfileController);
}());