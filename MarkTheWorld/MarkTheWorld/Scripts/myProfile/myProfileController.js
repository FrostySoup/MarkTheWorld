/*global angular */
(function () {
    'use strict';

    function myProfileController($scope, $mdDialog) {
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
    }

    angular.module('myProfile').controller('myProfileController', myProfileController);
}());