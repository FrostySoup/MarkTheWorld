/*global angular */
(function () {
    'use strict';

    function AppCtrl($scope, $log, $mdSidenav, $state, newSquareService, mapSettingsService, myProfileService, accountService, topMarkersService, simpleModalService) {

        $scope.toggleRight = function (state) {
            $state.transitionTo(state);
            sideBar();
        };

        $scope.isLogged = function () {
            return accountService.isLogged();
        };

        $scope.getLoggedUser = function () {
            return accountService.getLoggedUser();
        };

        $scope.logout = function (ev) {
            accountService.logout(ev);
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

        $scope.myProfile = function (ev) {
            myProfileService.showDialog(ev);
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