/*global angular */
(function () {
    'use strict';

    angular.module('map').factory('mapService', mapService);

    function mapService(SquareInfoFactory, $http) {
        var markersArray = [];
        var rectanglesArray = [];

        function addPoint(lat, lng, count) {
            var image = 'data:image/svg+xml,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20width%3D%2238%22%20height%3D%2238%22%20viewBox%3D%220%200%2038%2038%22%3E%3Cpath%20fill%3D%22%23808080%22%20stroke%3D%22%23ccc%22%20stroke-width%3D%22.5%22%20d%3D%22M34.305%2016.234c0%208.83-15.148%2019.158-15.148%2019.158S3.507%2025.065%203.507%2016.1c0-8.505%206.894-14.304%2015.4-14.304%208.504%200%2015.398%205.933%2015.398%2014.438z%22%2F%3E%3Ctext%20transform%3D%22translate%2819%2018.5%29%22%20fill%3D%22%23fff%22%20style%3D%22font-family%3A%20Arial%2C%20sans-serif%3Bfont-weight%3Abold%3Btext-align%3Acenter%3B%22%20font-size%3D%2212%22%20text-anchor%3D%22middle%22%3E' + count + '%3C%2Ftext%3E%3C%2Fsvg%3E';

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