/*global angular */
(function () {
    'use strict';

    function sideNavDirective() {
        return {
            templateUrl: '/Scripts/sideNav/directives/sideNavDirective.html',
            scope: {
                action: "="
            },
            bindToController: true,
            controllerAs: 'vm',
            restrict: 'E',
            controller: function ($mdSidenav) {
                var vm = this;
                vm.switchPanel = function (newAction) {
                    $mdSidenav('right_side_nav').close().then(function () {
                        vm.action = newAction;
                        $mdSidenav('right_side_nav').open();
                    });
                };
            }
        };
    }

    angular.module('sideNav').directive('sideNav', sideNavDirective);
}());