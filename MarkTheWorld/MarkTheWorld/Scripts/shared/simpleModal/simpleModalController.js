/*global angular */
(function () {
    'use strict';

    function simpleModalController($scope, $mdDialog, title, content) {
        $scope.title = title;
        $scope.content = content;
        $scope.cancel = function() {
            $mdDialog.cancel();
        };
    }
    angular.module('simpleModal').controller('simpleModalController', simpleModalController);
}());