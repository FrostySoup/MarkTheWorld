/*global angular */
(function () {
    'use strict';

    function toastController($scope, $mdToast, message) {
        $scope.message = message;

        $scope.closeToast = function () {
            $mdToast.hide();
        };
    }
    angular.module('toast').controller('toastController', toastController);
}());