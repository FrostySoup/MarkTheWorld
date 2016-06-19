/*global angular */
(function () {
    'use strict';

    function sideNavDirective() {
        return {
            templateUrl: '/Scripts/sideNav/directives/sideNavDirective.html',
            bindToController: true,
            controllerAs: 'vm',
            scope: {},
            restrict: 'E',
            controller: function ($mdSidenav, sideNavEventServiceService) {
                var vm = this;

                sideNavEventServiceService.listen(function (event, data) {
                    vm.sideNavAction = data.action;
                    vm.extraData = data.extraData;
                    $mdSidenav('right_side_nav').open();
                });

                vm.openPanel = function (sideNavAction) {
                    vm.sideNavAction = sideNavAction;
                    sideNavEventServiceService.emit({ action: sideNavAction });
                };

                vm.switchPanel = function (sideNavAction) {
                    $mdSidenav('right_side_nav').close().then(function () {
                        sideNavEventServiceService.emit({ action: sideNavAction });
                    });
                };
            }
        };
    }

    angular.module('sideNav').directive('sideNav', sideNavDirective);
}());