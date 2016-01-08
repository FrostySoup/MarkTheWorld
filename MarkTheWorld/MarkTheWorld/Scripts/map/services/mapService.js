/*global angular */
/*global GMaps */
(function () {
    'use strict';

    function mapService($http, rectanglesService) {
        var markersArray = [];
        var myStyles2 = [{"featureType":"landscape","stylers":[{"saturation":-100},{"lightness":65},{"visibility":"on"}]},{"featureType":"poi","stylers":[{"saturation":-100},{"lightness":51},{"visibility":"simplified"}]},{"featureType":"road.highway","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"road.arterial","stylers":[{"saturation":-100},{"lightness":30},{"visibility":"on"}]},{"featureType":"road.local","stylers":[{"saturation":-100},{"lightness":40},{"visibility":"on"}]},{"featureType":"transit","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"administrative.province","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"labels","stylers":[{"visibility":"on"},{"lightness":-25},{"saturation":-100}]},{"featureType":"water","elementType":"geometry","stylers":[{"hue":"#ffff00"},{"lightness":-25},{"saturation":-97}]}];
        var marker;

        var map = new GMaps({
            div: '#map',
            lat: -12.043333,
            lng: -77.028333,
            width: '100%',
            height: 'calc(100% - 64px)',
            streetViewControl: false,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            styles: myStyles2,
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
                window.lat = e.latLng.lat();
                window.lng = e.latLng.lng();
                console.log(JSON.stringify(e.latLng), 'zoom:', map.getZoom());
            }
        });

        function addPoint(lat, lng, count) {
            var size = 32 + count;
            if (size % 2 !== 0) {
                size = size + 1;
            }
            if (size > 64) {
                size = 64;
            }

            var image = 'data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" width="' + size + '" height="' + size + '" viewBox="0 0 38 38"><path fill="rgb(197,17,98)" d="M34.305 16.234c0 8.83-15.148 19.158-15.148 19.158S3.507 25.065 3.507 16.1c0-8.505 6.894-14.304 15.4-14.304 8.504 0 15.398 5.933 15.398 14.438z"/><text transform="translate(19 18.5)" fill="#fff" style="font-family: Roboto, \'Helvetica Neue\', sans-serif;font-weight:bold;text-align:center;" font-size="12" text-anchor="middle">' + count + '</text></svg>';

            console.log(lat, lng);

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

        return {
            map: map,
            markAllPoint: function () {
                var url = '/api/dotsInArea';
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
                        angular.forEach(data.data, function (value, key) {
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
            },
            clearMap: function () {
                for (var i = 0; i < markersArray.length; i++) {
                    map.removeMarker(markersArray[i]);
                }
                for (var i = 0; i < rectanglesArray.length; i++) {
                    rectanglesArray[i].setMap(null);
                }

                rectanglesArray.length = 0;
                markersArray.length = 0;
            }
        };
    }

    angular.module('map').factory('mapService', mapService);
})();