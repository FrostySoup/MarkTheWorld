/*global angular */
(function () {
    'use strict';

    angular.module('map', [])
        .run(function (mapService) {
            google.maps.event.addListener(mapService.map.map, 'center_changed', mapService.centerZoomChangedHandler);
            google.maps.event.addListener(mapService.map.map, 'zoom_changed', mapService.centerZoomChangedHandler);
        });
}());