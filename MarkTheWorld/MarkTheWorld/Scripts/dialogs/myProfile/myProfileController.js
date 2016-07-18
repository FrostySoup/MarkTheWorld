/*global module */
'use strict';

function myProfileController($mdDialog, myProfileService, myProfilePictureService, myProfileColorService) {
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

    vm.myProfileColor = function () {
        myProfileColorService.showDialog(vm.profileData.colors).finally(
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

module.exports = ['$mdDialog', 'myProfileService', 'myProfilePictureService', 'myProfileColorService', myProfileController];