/*global angular */
(function () {
    'use strict';

    function newSquareService($mdDialog) {
        return {
            showDialog : function (ev) {
                $mdDialog.show({
                    controller: 'AddPointCtrl',
                    templateUrl: 'scripts/newSquare/newSquare.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    locals: { "lat": window.lat, "lng": window.lng },
                    bindToController: true,
                    clickOutsideToClose: true
                });
            }
        };
    }

    angular.module('squareDetails').factory('newSquareService', newSquareService);
}());