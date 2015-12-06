(function () {
    'use strict';

    app.factory('MarkMapFactory', MarkMapFactory);

    function MarkMapFactory(SquareInfoFactory, $http, $q) {
        var markersArray = [];
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
                var deferredObject = $q.defer();

                $http.post(
                    '/api/dotsInArea', {
                        "nwX": map.getBounds().getNorthEast().lat(),
                        "nwY": map.getBounds().getNorthEast().lng(),
                        "seX": map.getBounds().getSouthWest().lat(),
                        "seY": map.getBounds().getSouthWest().lng()
                    }
                ).
                success(function (data) {
                    if (data) {
                        console.log(data);
                        deferredObject.resolve(data);
                    } else {
                        deferredObject.resolve(false);
                    }
                }).
                error(function () {
                    deferredObject.resolve(false);
                });

                return deferredObject.promise;
            },
            clearMap: function() {
                for (var i = 0; i < markersArray.length; i++ ) {
                    markersArray[i].setMap(null);
                }
                markersArray.length = 0;
            }
        };
    }
})();