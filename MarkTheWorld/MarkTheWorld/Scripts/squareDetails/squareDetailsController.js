/*global angular */
(function () {
    'use strict';

    function squareDetailsController($scope, $mdDialog, markers) {
        angular.forEach(markers, function (value) {
            value.date = new RegExp("[0-9-]*").exec(value.date)[0];
        });

        $scope.markers = markers;
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
    }

    angular.module('squareDetails').controller('squareDetailsController', squareDetailsController);
}());