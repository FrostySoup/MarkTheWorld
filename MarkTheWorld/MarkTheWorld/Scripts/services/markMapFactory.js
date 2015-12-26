(function () {
    'use strict';

    app.factory('MarkMapFactory', MarkMapFactory);

    function MarkMapFactory(SquareInfoFactory, $http) {
        var markersArray = [];
        var rectanglesArray = [];

        function addPoint(lat, lng, title) {
            return map.addMarker({
                title: title,
                lat: lat,
                lng: lng,
                infoWindow: {
                    content: title
                }
            });
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
                        if (data) {
                            angular.forEach(data, function (value, key) {
                                markersArray.push(addPoint(value.lat, value.lon, value.message));
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