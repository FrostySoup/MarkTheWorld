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
            drawRectangles : function(recs) {
                angular.forEach(recs, function(rec, key) {
                    map.drawRectangle({
                        bounds: [[rec.nw.lat, rec.nw.lng], [rec.se.lat, rec.se.lng]],
                        strokeColor: '#131540',
                        strokeOpacity: 1,
                        strokeWeight: 1,
                        click: function() { SquareInfoFactory.showDialog(rec.markers); }
                    });
                });
            },
            markAllPoint: function () {
                $http.post(
                        '/api/dotsInArea', {
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
            markRectangles: function() {
                $http.post(
                        '/api/squaresInArea', {
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
                                console.log('viduj', value.neX, value.neY, value.swX, value.swY);
                                rectanglesArray.push(
                                    map.drawRectangle({
                                        bounds: [[value.swY, value.swX], [value.neY, value.neX]],
                                        strokeColor: 'rgb(255,64,129)',
                                        fillColor: 'rgb(255,64,129)',
                                        strokeOpacity: 1,
                                        strokeWeight: 1
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