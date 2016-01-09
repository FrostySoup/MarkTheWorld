/*global angular */
/*global GMaps */
(function () {
    'use strict';

    function mapService($http, rectanglesService) {
        var markersArray = [];
        var mapStyle = [{"featureType":"landscape","stylers":[{"saturation":-100},{"lightness":65},{"visibility":"on"}]},{"featureType":"poi","stylers":[{"saturation":-100},{"lightness":51},{"visibility":"simplified"}]},{"featureType":"road.highway","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"road.arterial","stylers":[{"saturation":-100},{"lightness":30},{"visibility":"on"}]},{"featureType":"road.local","stylers":[{"saturation":-100},{"lightness":40},{"visibility":"on"}]},{"featureType":"transit","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"administrative.province","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"labels","stylers":[{"visibility":"on"},{"lightness":-25},{"saturation":-100}]},{"featureType":"water","elementType":"geometry","stylers":[{"hue":"#ffff00"},{"lightness":-25},{"saturation":-97}]}];
        var marker;
        var clickedPosition = {};

        var map = new GMaps({
            div: '#map',
            lat: -12.043333,
            lng: -77.028333,
            width: '100%',
            height: 'calc(100% - 64px)',
            streetViewControl: false,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            styles: mapStyle,
            click: function(e) {
                if (marker) {
                    marker.setMap(null);
                }
                marker = new google.maps.Marker({
                    map: map.map,
                    draggable: true,
                    animation: google.maps.Animation.DROP,
                    position: { lat: e.latLng.lat(), lng: e.latLng.lng() }
                });
                clickedPosition.lat = e.latLng.lat();
                clickedPosition.lng = e.latLng.lng();
                console.log(JSON.stringify(clickedPosition), 'zoom:', map.getZoom());
            }
        });

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

        function addPoint(lat, lng, count) {
            var size = 32 + count;
            if (size % 2 !== 0) {
                size = size + 1;
            }
            if (size > 64) {
                size = 64;
            }

            var image = 'data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" width="' + size + '" height="' + size + '" viewBox="0 0 38 38"><path fill="rgb(197,17,98)" d="M34.305 16.234c0 8.83-15.148 19.158-15.148 19.158S3.507 25.065 3.507 16.1c0-8.505 6.894-14.304 15.4-14.304 8.504 0 15.398 5.933 15.398 14.438z"/><text transform="translate(19 18.5)" fill="#fff" style="font-family: Roboto, \'Helvetica Neue\', sans-serif;font-weight:bold;text-align:center;" font-size="12" text-anchor="middle">' + count + '</text></svg>';

            return map.addMarker({
                //title: title,
                lat: lat,
                icon: image,
                lng: lng
                //infoWindow: {
                //    content: title
                //}
            });
        }

        function removePointsFromMap(markersArray) {
            for (var i = 0; i < markersArray.length; i++) {
                map.removeMarker(markersArray[i]);
            }

            markersArray.length = 0;
        }

        var returnObject = {
            map: map,

            centerZoomChangedHandler : debounce((function () {
                if (map.getZoom() < 11) {
                    rectanglesService.removeAllRecs();
                    returnObject.markClusters();
                }
                else {
                    removePointsFromMap(markersArray);
                    returnObject.markRectangles();
                }
            }), 250),

            getClickedPosition: function() {
              return clickedPosition;
            },

            markClusters: function () {
                var url = '/api/dotsInArea/' + map.getZoom();
                if (localStorage.getItem('onlyMyOwnMarks') === 'true') {
                    url = '/api/getUserDots/' + localStorage.getItem('token');
                }
                $http.post(
                    url, {
                        "neX": map.getBounds().getNorthEast().lng(),
                        "neY": map.getBounds().getNorthEast().lat(),
                        "swX": map.getBounds().getSouthWest().lng(),
                        "swY": map.getBounds().getSouthWest().lat()
                    }
                ).
                then(function(data) {
                    removePointsFromMap(markersArray);
                    if (data) {
                        angular.forEach(data.data, function (value) {
                            markersArray.push(addPoint(value.sTemp, value.nTemp, value.count));
                        });
                    }
                });

            },

            markRectangles: function () {
                var url = '/api/squaresInArea';
                if (localStorage.getItem('onlyMyOwnMarks') === 'true') {
                    url = '/api/getUserSquares/' + localStorage.getItem('token');
                }
                $http.post(
                    url, {
                        "neX": map.getBounds().getNorthEast().lng(),
                        "neY": map.getBounds().getNorthEast().lat(),
                        "swX": map.getBounds().getSouthWest().lng(),
                        "swY": map.getBounds().getSouthWest().lat()
                    }
                ).then(function successCallback(response) {
                    rectanglesService.handleRecs(response.data, map);
                }, function errorCallback(response) {
                        console.log('Error: ', JSON.stringify(response));
                });
            }
        };

        return returnObject;
    }

    angular.module('map').factory('mapService', mapService);
})();