/*global angular */
(function () {
    'use strict';

    function newSquareService($mdDialog) {
        return {
            showDialog : function (ev) {
                $mdDialog.show({
                    controller: 'newSquareController',
                    templateUrl: 'scripts/newSquare/newSquare.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true
                });
            }
        };
    }

    angular.module('newSquare').factory('newSquareService', newSquareService);
}());