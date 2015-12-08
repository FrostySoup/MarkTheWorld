(function () {
    'use strict';

    app.factory('MarkMapFactory', MarkMapFactory);

    function MarkMapFactory(SquareInfoFactory, $http, $q) {
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
        };

        return {
            markAllPoint: function () {
                var url = '/api/dotsInArea';
                if (localStorage.getItem('onlyMyOwnMarks') === 'true') {
                    console.log('paisom tik userio');
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
                    success(function (data) {
                        console.log(data);
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
                    console.log('paisom tik userio');
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
                        console.log(data);
                        if (data) {
                            angular.forEach(data, function (value, key) {
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