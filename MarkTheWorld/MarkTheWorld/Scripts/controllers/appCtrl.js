(function () {
    'use strict';

    app.controller('AppCtrl', AppCtrl);

    function AppCtrl($scope, $log, $mdDialog, $mdSidenav, $state, $http, MarkMapFactory, SimpleModalFactory, $timeout) {

        $scope.toggleRight = function(state) {
            $state.transitionTo(state, { param1 : 'something' }, { reload: true });
            sideBar();
        };

        $scope.isLogged = function() {
            return localStorage.getItem('token') !== null;
        }

        $scope.getLoggedUser = function() {
            return localStorage.getItem('user');
        }

        $scope.logout = function () {
            SimpleModalFactory.showModal('Info', 'Logged out successfully');
            $timeout(function () {
                localStorage.removeItem('token');
                localStorage.removeItem('user');
                localStorage.removeItem('onlyMyOwnMarks');
            }, 300);
        }

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
        }

        $scope.showMapSettingsDialog = function (ev) {
            $mdDialog.show({
                controller: 'MapSettingsCtrl',
                templateUrl: 'scripts/templates/mapSettingsDialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true
            });
        }

        $scope.showDialog = function(ev) {
            $mdDialog.show({
                controller: 'AddPointCtrl',
                templateUrl: 'scripts/templates/addPointDialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                locals: { "lat": window.lat, "lng": window.lng },
                bindToController: true,
                clickOutsideToClose: true
            });
        };

        // cia yra current pos nustatymas
        var req = {
            method: 'POST',
            url: 'https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyBBmLH1JbsTdr8CeJYP8icbQqcymux3ffA'
            //data: { test: 'test' }
        };

        $http(req).then(function(data){
            window.currLocation = data.data.location;
            map.setCenter({lat: data.data.location.lat, lng: data.data.location.lng});
            //map.addMarker({
            //    lat: data.data.location.lat,
            //    lng: data.data.location.lng
            //});
        });

        function buildToggler(navID) {
            return function() {
                $mdSidenav(navID)
                    .toggle()
                    .then(function () {
                        $log.debug("toggle " + navID + " is done");
                    });
            }
        }

        var sideBar = buildToggler('right');

        function debounce(func, wait, immediate) {
            var timeout;
            return function () {
                var context = this, args = arguments;
                var later = function () {
                    timeout = null;
                    if (!immediate) func.apply(context, args);
                };
                var callNow = immediate && !timeout;
                clearTimeout(timeout);
                timeout = setTimeout(later, wait);
                if (callNow) func.apply(context, args);
            };
        };

        var centerChangedHandler = debounce(function () {
            MarkMapFactory.clearMap();
            if (map.getZoom() < 12) {
                MarkMapFactory.markAllPoint();
            }
            else {
                MarkMapFactory.markRectangles();
            }
        }, 250);

        google.maps.event.addListener(map.map, 'center_changed', centerChangedHandler);
    }
})();