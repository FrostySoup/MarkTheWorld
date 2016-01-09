/*global angular */
(function () {
    'use strict';

    function TopMarkersCtrl($scope, $mdDialog, $http) {

        $scope.toplist = {};

        $http.get('/api/topList/').
            success(function (data) {
                if (data) {
                    console.log(data);
                    $scope.toplist = data;
                }
            });

        $scope.cancel = function () {
            $mdDialog.hide();
        };
    }
    angular.module('markTheWorld').controller('TopMarkersCtrl', TopMarkersCtrl);
}());