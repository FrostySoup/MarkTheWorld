/*global angular */
(function () {
    'use strict';

    function claimSpotController($mdDialog, claimSpotService, toastService) {
        var vm = this;
        vm.file = null;
        vm.message = '';
        vm.fileError = '';

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
                switch (error) {
                case 'pattern':
                    vm.fileError = 'Only images are allowed';
                    break;
                case 'minHeight':
                    vm.fileError = 'Image dimensions should be at least 100x100';
                    break;
                case 'minWidth':
                    vm.fileError = 'Image dimensions should be at least 100x100';
                    break;
                case 'maxSize':
                    vm.fileError = 'Image should be smaller than 20MB';
                    break;
                default:
                    vm.fileError = 'Your picture couldn\'t be processed';
                }
            }
        };

        vm.claim = function () {
            claimSpotService.claim(vm.file, vm.message).then(
                function (success) {
                    toastService.showToast('Spot claimed!', 5000);
                    vm.cancel();
                },
                function (error) {
                    console.log('error', error);
                }
            );
        };

        //$scope.confirm = function (message) {
        //    accountService.addPoint(message, mapService.getClickedPosition().lat, mapService.getClickedPosition().lng)
        //        .then(function (data) {
        //            if (data.success === true) {
        //                $mdDialog.hide().then(function () {
        //                    simpleModalService.showModal('Congrats!', 'You marked a spot!');
        //                    mapService.updateMap();
        //                });
        //            } else {
        //                simpleModalService.showModal('Error!', data.message);
        //            }
        //        });
        //};
    }
    angular.module('claimSpot').controller('claimSpotController', claimSpotController);
}());