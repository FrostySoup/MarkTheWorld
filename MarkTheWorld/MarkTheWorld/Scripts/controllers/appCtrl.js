/*global angular */
(function () {
    'use strict';

    function AppCtrl($scope, $log, $mdSidenav, $state, newSquareService, mapSettingsService, accountService, topMarkersService, simpleModalService, $mdDialog) {

        $scope.toggleRight = function (state) {
            $state.transitionTo(state, { param1 : 'something' }, { reload: true });
            sideBar();
        };

        $scope.isLogged = function () {
            return accountService.isLogged();
        };

        $scope.getLoggedUser = function () {
            return accountService.getLoggedUser();
        };

        $scope.logout = function () {
            accountService.logout();
        };

        $scope.openMenu = function ($mdOpenMenu, ev) {
            $mdOpenMenu(ev);
        };

        $scope.topMarkers = function () {
            topMarkersService.showDialog();
        };

        $scope.mapSettings = function (ev) {
            mapSettingsService.showDialog(ev);
        };

        $scope.addNewSquare = function (ev) {
            newSquareService.showDialog(ev);
        };

        if (!accountService.isLogged()) {
            simpleModalService.showModal('Welcome!', 'Join MarkTheWorld and leave your first mark!');
        }

        function buildToggler(navID) {
            return function () {
                $mdSidenav(navID)
                    .toggle()
                    .then(function () {
                        $log.debug("toggle " + navID + " is done");
                    });
            };
        }

        var sideBar = buildToggler('right');
    }

    angular.module('markTheWorld').controller('AppCtrl', AppCtrl);
}());