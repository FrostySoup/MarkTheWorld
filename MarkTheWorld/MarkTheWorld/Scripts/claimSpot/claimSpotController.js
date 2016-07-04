/*global angular */
(function () {
    'use strict';

    function claimSpotController($mdDialog, Upload) {
        var vm = this;
        vm.file = null;

        vm.cancel = function () {
            $mdDialog.cancel();
        };

        vm.cancelImageUpload = function () {
            vm.file = null;
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