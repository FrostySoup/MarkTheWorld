/*global angular */
(function () {
    'use strict';

    function squareDetailsService($mdDialog) {
        return {
            showDialog : function (markers) {
                $mdDialog.show({
                    controller: 'squareDetailsController',
                    templateUrl: 'scripts/squareDetails/squareDetails.html',
                    parent: angular.element(document.body),
                    locals: { "markers": markers },
                    bindToController: true,
                    clickOutsideToClose: true
                });
            }
        };
    }

    angular.module('squareDetails').factory('squareDetailsService', squareDetailsService);
}());