/* global module */

'use strict';

function mapService($http, rectanglesService, markersService, userService) {
        var clickedPosition = {};
        var map;
        var recStartingZoom = 11;
        var mapFilter = {
            filter: null
        };

        function init(m) {
            map = m;
            var marker;

            // only for testing purposes
            map.addListener('click', function(e) {
                if (marker) {
                    marker.setMap(null);
                }
                marker = new google.maps.Marker({
                    map: map,
                    animation: google.maps.Animation.DROP,
                    draggable: true,
                    position: { lat: e.latLng.lat(), lng: e.latLng.lng() }
                });

                marker.addListener('dragend', function (e) {
                    clickedPosition = { 'lat' : e.latLng.lat(), 'lng' : e.latLng.lng() };
                    userService.currentPosition = { lat: e.latLng.lat(), lng: e.latLng.lng() };
                    console.log('position:', 'lat:' + marker.position.lat(), 'lng:' + marker.position.lng(), 'zoom:', map.getZoom());

                    $http.post('/api/dotCheck',
                        {
                            "token": userService.token,
                            "lat": e.latLng.lat(),
                            "lng": e.latLng.lng()
                        }).then(
                        function (success) {
                            var cityCircle = new google.maps.Circle({
                                //strokeColor: '#FF0000',
                                strokeOpacity: 0,
                                strokeWeight: 0,
                                fillColor: '#FF0000',
                                fillOpacity: 0.35,
                                map: map,
                                center: {'lat': success.data.Lat, 'lng': success.data.Lon},
                                radius: 100
                            });
                            //var marker = new google.maps.Marker({
                            //    position: {'lat': success.data.Lat, 'lng': success.data.Lon},
                            //    icon: '~/../Content/img/icons/target.svg',
                            //    map: map
                            //});
                            console.log(success);
                        },
                        function (error) {
                            console.log(error);
                        }
                    );
                });

                clickedPosition = { 'lat' : e.latLng.lat(), 'lng' : e.latLng.lng() };

                userService.currentPosition = { lat: e.latLng.lat(), lng: e.latLng.lng() };

                $http.post('/api/dotCheck',
                    {
                        "token": userService.token,
                        "lat": e.latLng.lat(),
                        "lng": e.latLng.lng()
                    }).then(
                    function (success) {
                        var cityCircle = new google.maps.Circle({
                            //strokeColor: '#FF0000',
                            strokeOpacity: 0,
                            strokeWeight: 0,
                            fillColor: '#FF0000',
                            fillOpacity: 0.35,
                            map: map,
                            center: {'lat': success.data.Lat, 'lng': success.data.Lon},
                            radius: 100
                        });

                        //var marker = new google.maps.Marker({
                        //    position: {'lat': success.data.Lat, 'lng': success.data.Lon},
                        //    icon: '~/../Content/img/icons/target.svg',
                        //    map: map
                        //});
                        console.log(success);
                    },
                    function (error) {
                        console.log(error);
                    }
                );

                console.log('position:', 'lat:' + marker.position.lat(), 'lng:' + marker.position.lng(), 'zoom:', map.getZoom());
            });

            map.addListener('center_changed', debounce(updateMap, 250));
            map.addListener('zoom_changed', debounce(updateMap, 250));

            setCurrentPosition();
        }

        function setCurrentPosition() {
            var req = {
                method: 'POST',
                url: 'https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyBBmLH1JbsTdr8CeJYP8icbQqcymux3ffA'
            };

            $http(req).then(function(data) {
                map.setCenter({lat: data.data.location.lat, lng: data.data.location.lng});
                userService.currentPosition = { lat: data.data.location.lat, lng: data.data.location.lng };
            });
        }

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
        }

        function updateMap() {
            if (map.getZoom() < recStartingZoom) {
                rectanglesService.removeAllRecs();
                drawMarkers();
            }
            else {
                markersService.removeAllMarkers();
                drawRectangles();
            }
        }

        function updateRecColors() {
            if (map.getZoom() >= recStartingZoom) {
                var url = '/api/Squares';

                $http.post(
                    url, {
                        "neX": map.getBounds().getNorthEast().lng(),
                        "neY": map.getBounds().getNorthEast().lat(),
                        "swX": map.getBounds().getSouthWest().lng(),
                        "swY": map.getBounds().getSouthWest().lat()
                    }
                ).then(function (response) {
                        rectanglesService.updateRecColors(response.data);
                    }, function (response) {
                        //TODO: [preRelease] There should be a toast in such cases
                        console.log('Error: ', response.statusText);
                    });
            }
        }

        function drawMarkers() {
            var url = '/api/Dots/' + map.getZoom();

            $http.post(
                url, {
                    "neX": map.getBounds().getNorthEast().lng(),
                    "neY": map.getBounds().getNorthEast().lat(),
                    "swX": map.getBounds().getSouthWest().lng(),
                    "swY": map.getBounds().getSouthWest().lat()
                }
            ).then(function (response) {
                    markersService.handleMarkers(response.data, map);
                }, function (response) {
                    console.log('Error: ', response.statusText);
                });
        }

        function drawRectangles() {
            var url = '/api/Squares';

            if (mapFilter.filter !== null) {
                url += '?name=' + mapFilter.filter;
            }

            $http.post(
                url, {
                    "neX": map.getBounds().getNorthEast().lng(),
                    "neY": map.getBounds().getNorthEast().lat(),
                    "swX": map.getBounds().getSouthWest().lng(),
                    "swY": map.getBounds().getSouthWest().lat()
                }
            ).then(function (response) {
                    rectanglesService.handleRecs(response.data, map);
                }, function (response) {
                    //TODO: [preRelease] There should be a toast in such cases
                    console.log('Error: ', response.statusText);
                });
        }

        return {
            positionChangeHandler : debounce(updateMap, 250),

            updateMap : updateMap,

            updateRecColors: updateRecColors,

            mapFilter: mapFilter,

            init: init
        };
    }

module.exports = ['$http', 'rectanglesService', 'markersService', 'userService', mapService];