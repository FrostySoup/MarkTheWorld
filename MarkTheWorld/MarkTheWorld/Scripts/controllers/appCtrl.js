/*global angular */
(function () {
    'use strict';

    function AppCtrl($scope, $log, $mdDialog, $mdToast, $mdSidenav, $state, $http, mapService, newSquareService, mapSettingsService, accountService, toastService) {

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

        $scope.mapSettings = function (ev) {
            mapSettingsService.showDialog(ev);
        };

        $scope.addNewSquare = function (ev) {
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