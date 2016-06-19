/*global angular */
(function () {
    'use strict';

    function AppController($scope, $mdSidenav) {

        $scope.openRightSideNav = function (action) {
            $scope.rightSideNavAction = action;
            $mdSidenav('right_side_nav').open();
        };
    }

    angular.module('markTheWorld').controller('AppController', AppController);
}());