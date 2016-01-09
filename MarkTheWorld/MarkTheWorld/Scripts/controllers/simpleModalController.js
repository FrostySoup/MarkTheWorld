/*global angular */
(function () {
    'use strict';

    function SimpleModalController($scope, $mdDialog, title, content) {
        $scope.title = title;
        $scope.content = content;
        $scope.cancel = function() {
            $mdDialog.cancel();
        };
    }
    angular.module('markTheWorld').controller('SimpleModalController', SimpleModalController);
}());