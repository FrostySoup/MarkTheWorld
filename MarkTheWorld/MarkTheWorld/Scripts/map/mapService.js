/*global angular */
(function () {
    'use strict';

    angular.module('map').factory('mapService', mapService);

    function mapService(SquareInfoFactory, $http) {
        var markersArray = [];
        var rectanglesArray = [];

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

        function removeUnneededRecsFromMap(unneededRecs) {
            var arrayLength = unneededRecs.length;
            for (var i = 0; i < arrayLength; i++) {
                unneededRecs[i].setMap(null);
            }
        }

        function addNewRecs(newRecs) {
            var recsToBeAdded = [];

            function rectanglesArrayContains(element) {
                var arrayLength = rectanglesArray.length;

                for (var i = 0; i < arrayLength; i++) {
                    if (Math.round(rectanglesArray[i].bounds.N.j * 1000) / 1000 === Math.round(element.neY * 1000) / 1000 &&
                        Math.round(rectanglesArray[i].bounds.j.j * 1000) / 1000 === Math.round(element.swX * 1000) / 1000) {
                        return true;
                    }
                }

                return false;
            }

            angular.forEach(newRecs, function (value) {
                if (!rectanglesArrayContains(value)) {
                    recsToBeAdded.push(value);
                }
            });

            return recsToBeAdded;
        }

        function removeUnneededRecs(newRecs) {
            var recsToBeRemoved = [];

            function newRecsContains(element) {
                var arrayLength = newRecs.length;
                for (var i = 0; i < arrayLength; i++) {
                    if (Math.round(element.bounds.N.j * 1000) / 1000 === Math.round(newRecs[i].neY * 1000) / 1000 &&
                        Math.round(element.bounds.j.j * 1000) / 1000 === Math.round(newRecs[i].swX * 1000) / 1000) {
                        return true;
                    }
                }
                return false;
            }

            for (var i = 0; i < rectanglesArray.length; i++) {
                if (!newRecsContains(rectanglesArray[i])) {
                    recsToBeRemoved.push.apply(recsToBeRemoved, rectanglesArray.splice(i, 1));
                    i--;
                }
            }
            return recsToBeRemoved;
        }

        return {
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
                    success(function(data) {
                        console.log(data);
                        removePointsFromMap(markersArray);
                        if (data) {
                            angular.forEach(data, function (value, key) {
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
                    ).
                    success(function (data) {
                        if (data) {
                            var recsToBeRemoved = removeUnneededRecs(data);
                            removeUnneededRecsFromMap(recsToBeRemoved);
                            var recsToBeAdded = addNewRecs(data);

                            angular.forEach(recsToBeAdded, function (value, key) {
                                rectanglesArray.push(
                                    map.drawRectangle({
                                        bounds: [[value.swY, value.swX], [value.neY, value.neX]],
                                        strokeColor: 'rgb(255,64,129)',
                                        fillColor: 'rgb(255,64,129)',
                                        strokeOpacity: 1,
                                        strokeWeight: 1,
                                        click: function (event) {
                                            if (localStorage.getItem('onlyMyOwnMarks') !== 'true') {
                                                SquareInfoFactory.showDialog(value.markers);
                                            }
                                        }
                                    })
                                );
                            });
                        }
                    });
            },
            clearMap: function() {
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
})();