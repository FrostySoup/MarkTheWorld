/*global angular */
(function () {
    'use strict';

    function countDownDirective (myProfileService, $interval) {
        console.log('directyva');
        return {
            restrict: 'A',
            scope: {
                date: '@'
            },
            link: function (scope, element) {
                console.log('linkas');
                var future;
                future = new Date(scope.date);
                $interval(function () {
                    var diff;
                    diff = Math.floor((future.getTime() - new Date().getTime()) / 1000);
                    element.text(myProfileService.getFormattedTime(diff));
                }, 1000);
            }
        };
    }

    angular.module('myProfile').directive('countDown', countDownDirective);
}());