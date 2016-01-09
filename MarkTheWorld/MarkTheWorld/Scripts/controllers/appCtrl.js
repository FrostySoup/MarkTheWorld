/*global angular */
(function () {
    'use strict';

    function AppCtrl($scope, $log, $mdDialog, $mdSidenav, $state, $http, SimpleModalFactory, $timeout, mapService, newSquareService) {

        $scope.toggleRight = function(state) {
            $state.transitionTo(state, { param1 : 'something' }, { reload: true });
            sideBar();
        };

        $scope.isLogged = function() {
            return localStorage.getItem('token') !== null;
        };

        $scope.getLoggedUser = function() {
            return localStorage.getItem('user');
        };

        $scope.logout = function () {
            SimpleModalFactory.showModal('Info', 'Logged out successfully');
            $timeout(function () {
                localStorage.removeItem('token');
                localStorage.removeItem('user');
                localStorage.removeItem('onlyMyOwnMarks');
            }, 300);
        };

        $scope.openMenu = function($mdOpenMenu, ev) {
            $mdOpenMenu(ev);
        };

        $scope.showTopMarkersDialog = function () {
            $mdDialog.show({
                controller: 'TopMarkersCtrl',
                templateUrl: 'scripts/templates/topMarkersDialog.html',
                parent: angular.element(document.body),
                clickOutsideToClose: true
            });
        };

        $scope.showMapSettingsDialog = function (ev) {
            $mdDialog.show({
                controller: 'MapSettingsCtrl',
                templateUrl: 'scripts/templates/mapSettingsDialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true
            });
        };

        $scope.addNewSquare = function(ev) {
            newSquareService.showDialog(ev);
        };

        // cia yra current pos nustatymas
        var req = {
            method: 'POST',
            url: 'https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyBBmLH1JbsTdr8CeJYP8icbQqcymux3ffA'
            //data: { test: 'test' }
        };

        $http(req).then(function(data){
            window.currLocation = data.data.location;
            mapService.map.setCenter({lat: data.data.location.lat, lng: data.data.location.lng});
            //map.addMarker({
            //    lat: data.data.location.lat,
            //    lng: data.data.location.lng
            //});
        });

        function buildToggler(navID) {
            return function () {
                $mdSidenav(navID)
                    .toggle()
                    .then(function () {
                        $log.debug("toggle " + navID + " is done");
                    });
            };
        };

        var sideBar = buildToggler('right');
    }

    angular.module('markTheWorld').controller('AppCtrl', AppCtrl);
}());