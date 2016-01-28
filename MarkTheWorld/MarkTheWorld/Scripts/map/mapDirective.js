/*global angular */
(function () {
    'use strict';

    function mapDirective(GoogleMapsInitializerService, mapService, $http) {
        return {
            restrict: 'E',
            replace: true,
            template: "<div></div>",
            link: function (scope, el, attr) {
                var mapStyle = [{"featureType":"landscape","stylers":[{"saturation":-100},{"lightness":65},{"visibility":"on"}]},{"featureType":"poi","stylers":[{"saturation":-100},{"lightness":51},{"visibility":"simplified"}]},{"featureType":"road.highway","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"road.arterial","stylers":[{"saturation":-100},{"lightness":30},{"visibility":"on"}]},{"featureType":"road.local","stylers":[{"saturation":-100},{"lightness":40},{"visibility":"on"}]},{"featureType":"transit","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"administrative.province","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"labels","stylers":[{"visibility":"on"},{"lightness":-25},{"saturation":-100}]},{"featureType":"water","elementType":"geometry","stylers":[{"hue":"#ffff00"},{"lightness":-25},{"saturation":-97}]}];
                var marker;
                var map;

                GoogleMapsInitializerService.mapsInitialized.then(function() {
                    map = new google.maps.Map(el[0], {
                        center: { lat: -34.397, lng: 150.644 },
                        mapTypeId: google.maps.MapTypeId.ROADMAP,
                        styles: mapStyle,
                        zoom: 12,
                        zoomControlOptions: {
                            position: google.maps.ControlPosition.LEFT_TOP
                        },
                        streetViewControl: false
                    });

                    // This is only for testing purposes
                    map.addListener('click', function(e) {
                        if (marker) {
                            marker.setMap(null);
                        }
                        marker = new google.maps.Marker({
                            map: map,
                            animation: google.maps.Animation.DROP,
                            position: { lat: e.latLng.lat(), lng: e.latLng.lng() }
                        });
                        mapService.setClickedPosition({ 'lat' : e.latLng.lat(), 'lng' : e.latLng.lng() });
                        console.log('position:', 'lat:' + marker.position.lat(), 'lng:' + marker.position.lng(), 'zoom:', map.getZoom());
                    });

                    mapService.setMap(map);

                    var req = {
                        method: 'POST',
                        url: 'https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyBBmLH1JbsTdr8CeJYP8icbQqcymux3ffA'
                    };

                    $http(req).then(function(data) {
                        map.setCenter({lat: data.data.location.lat, lng: data.data.location.lng});
                    });

                    map.addListener('center_changed', mapService.positionChangeHandler);
                    map.addListener('zoom_changed', mapService.positionChangeHandler);
                });
            }
        };
    }

    angular.module('map').directive('gameMap', mapDirective);
}());