/*global angular */
/*global google */
(function () {
    'use strict';

    function mapService($http, rectanglesService, markersService, accountService) {
        var clickedPosition = {};
        var map;

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
            if (map.getZoom() < 11) {
                rectanglesService.removeAllRecs();
                markClusters();
            }
            else {
                markersService.removeAllMarkers();
                markRectangles();
            }
        }

        function markClusters() {
            var url = '/api/dotsInArea/' + map.getZoom();

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
                    console.log('Error: ', JSON.stringify(response));
                });
        }

        function markRectangles() {
            var url = '/api/squaresInArea';
            if (accountService.getMapUser() !== null && accountService.getMapUser() !== 'all') {
                url = '/api/getUserSquaresByName/' + accountService.getMapUser();
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
                    console.log('Error: ', JSON.stringify(response));
                });
        }

        return {
            positionChangeHandler : debounce(updateMap, 250),

            updateMap : updateMap,

            getClickedPosition: function() {
              return clickedPosition;
            },
            setClickedPosition: function(clickedP) {
                clickedPosition = clickedP;
            },
            setMap: function(m) {
                map = m;
            }
        };
    }

    angular.module('map').factory('mapService', mapService);
}());