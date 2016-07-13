/*global module */
'use strict';

function claimSpotController($mdDialog, claimSpotService, myProfilePictureService, toastService, mapService) {
    var vm = this;
    vm.file = null;
    vm.message = '';
    vm.fileError = '';
    vm.claimError = '';
    vm.pending = false;

    vm.cancel = function () {
        $mdDialog.cancel();
    };

    vm.cancelImageUpload = function () {
        vm.file = null;
        vm.fileError = '';
    };

    vm.handleSelectedFile = function (fileForm) {
        if (!fileForm.$valid || !vm.file) {
            var error = Object.keys(fileForm.$error)[0];
            vm.fileError = myProfilePictureService.fileError(error);
        }
    };

    vm.claim = function () {
        vm.pending = true;
        claimSpotService.claim(vm.file, vm.message).then(
            function (success) {
                $mdDialog.hide().then(function () {
                    toastService.showToast('Spot claimed!', 5000);
                    mapService.updateMap();
                });
            },
            function (error) {
                if (error.status === 400) {
                    vm.claimError = error.data;
                }
            }
        ).finally(function () {
            vm.pending = false;
        });
    };
}

module.exports = ['$mdDialog', 'claimSpotService', 'myProfilePictureService', 'toastService', 'mapService', claimSpotController];