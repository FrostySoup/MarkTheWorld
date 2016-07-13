/*global module */
'use strict';

function mapDirective(googleMapsInitializerService, mapService) {
    return {
        restrict: 'E',
        replace: true,
        template: "<div></div>",
        link: function (scope, el) {
            var mapStyle = [{
                "featureType": "landscape",
                "stylers": [{"saturation": -100}, {"lightness": 65}, {"visibility": "on"}]
            }, {
                "featureType": "poi",
                "stylers": [{"saturation": -100}, {"lightness": 51}, {"visibility": "simplified"}]
            }, {
                "featureType": "road.highway",
                "stylers": [{"saturation": -100}, {"visibility": "simplified"}]
            }, {
                "featureType": "road.arterial",
                "stylers": [{"saturation": -100}, {"lightness": 30}, {"visibility": "on"}]
            }, {
                "featureType": "road.local",
                "stylers": [{"saturation": -100}, {"lightness": 40}, {"visibility": "on"}]
            }, {
                "featureType": "transit",
                "stylers": [{"saturation": -100}, {"visibility": "simplified"}]
            }, {
                "featureType": "administrative.province",
                "stylers": [{"visibility": "off"}]
            }, {
                "featureType": "water",
                "elementType": "labels",
                "stylers": [{"visibility": "on"}, {"lightness": -25}, {"saturation": -100}]
            }, {
                "featureType": "water",
                "elementType": "geometry",
                "stylers": [{"hue": "#ffff00"}, {"lightness": -25}, {"saturation": -97}]
            }];

            googleMapsInitializerService.mapsInitialized.then(function () {
                var map = new google.maps.Map(el[0], {
                    center: {lat: -34.397, lng: 150.644},
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    styles: mapStyle,
                    zoom: 12,
                    zoomControlOptions: {
                        position: google.maps.ControlPosition.LEFT_TOP
                    },
                    streetViewControl: false
                });

                mapService.init(map);
            });
        }
    };
}

module.exports = ['googleMapsInitializerService', 'mapService', mapDirective];