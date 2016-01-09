/*global angular */
(function () {
    'use strict';

    function AddPointCtrl($scope, $mdDialog, AccountFactory, SimpleModalFactory, mapService) {
        $scope.cancel = function() {
            $mdDialog.cancel();
        };

        $scope.confirm = function (message) {
            AccountFactory.addPoint(message, mapService.getClickedPosition().lat, mapService.getClickedPosition().lng);
                //.then(function (data) {
                //    if (data.success === true) {
                //        $mdDialog.hide().then(function() {
                //            SimpleModalFactory.showModal('Congrats!', 'You marked a spot!');
                //            MarkMapFactory.clearMap();
                //            if (map.getZoom() < 12) {
                //                MarkMapFactory.markAllPoint();
                //            }
                //            else {
                //                MarkMapFactory.markRectangles();
                //            }
                //        });
                //    } else {
                //        SimpleModalFactory.showModal('Error!', data.message);
                //    }
                //});
        };
    }
    angular.module('newSquare').controller('AddPointCtrl', AddPointCtrl);
}());