(function () {
    'use strict';

    app.controller('AppCtrl', AppCtrl);

    function AppCtrl($scope, $log, $mdDialog, $mdSidenav, $state, $http, MarkMapFactory, SimpleModalFactory, $timeout) {



        //MarkMapFactory.drawRectangles(
        //    [
        //        {
        //            nw: { lat: 54.944843785753164, lng: 23.87951374053955 },
        //            se: { lat: 54.94232950298698, lng: 23.883891105651855 },
        //            markers:  [
        //                {
        //                    date: "2015-02-03",
        //                    username: "Staska",
        //                    message: "Yay, I love this place"
        //                },
        //                {
        //                    date: "2014-02-03",
        //                    username: "Petras",
        //                    message: "Hate to be second :("
        //                },
        //                {
        //                    date: "2013-02-03",
        //                    username: "Zigmas",
        //                    message: "I drank tons of beer here"
        //                }
        //            ]
        //        },
        //        {
        //            nw: { lat: 54.93939597435294, lng: 23.887968063354492 },
        //            se: { lat: 54.936634810706686, lng: 23.892087936401367 },
        //            markers:  [
        //                {
        //                    date: "2015-02-03",
        //                    username: "Staska",
        //                    message: "Yay, I love this place"
        //                },
        //                {
        //                    date: "2014-02-03",
        //                    username: "Petras",
        //                    message: "Hate to be second :("
        //                },
        //                {
        //                    date: "2013-02-03",
        //                    username: "Zigmas",
        //                    message: "I drank tons of beer here"
        //                }
        //            ]
        //        }
        //    ]
        //);

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
            }, 300);
        }

        $scope.openMenu = function($mdOpenMenu, ev) {
            $mdOpenMenu(ev);
        };

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

        var req = {
            method: 'POST',
            url: 'https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyBBmLH1JbsTdr8CeJYP8icbQqcymux3ffA'
            //data: { test: 'test' }
        };

        $http(req).then(function(data){
            window.currLocation = data.data.location;
            map.setCenter({lat: data.data.location.lat, lng: data.data.location.lng});
            map.addMarker({
                lat: data.data.location.lat,
                lng: data.data.location.lng
            });
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
            MarkMapFactory.markAllPoint();
        }, 250);

        google.maps.event.addListener(map.map, 'center_changed', centerChangedHandler);
    }
})();