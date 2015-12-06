(function () {
    'use strict';

    app.factory('MarkMapFactory', MarkMapFactory);

    function MarkMapFactory (SquareInfoFactory) {
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
            }
        };
    }
})();