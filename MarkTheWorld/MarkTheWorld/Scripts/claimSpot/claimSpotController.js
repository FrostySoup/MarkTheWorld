/*global angular */
(function () {
    'use strict';

    function claimSpotController($mdDialog) {
        var vm = this;

        vm.cancel = function () {
            $mdDialog.cancel();
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