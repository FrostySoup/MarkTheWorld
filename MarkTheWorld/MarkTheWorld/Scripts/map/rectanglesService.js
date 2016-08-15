/*global module */
'use strict';

function rectanglesService(spotDetailsService) {
    var rectanglesArray = [];

    function removeUnneededRecsFromMap(unneededRecs) {
        var arrayLength = unneededRecs.length;
        for (var i = 0; i < arrayLength; i++) {
            unneededRecs[i].setMap(null);
        }
    }

    function addNewRecsToMap(recsToBeAdded, map) {
        angular.forEach(recsToBeAdded, function (value) {
            var rec = new google.maps.Rectangle({
                id: value.dotId,
                strokeColor: value.borderColors,
                strokeOpacity: 0.7,
                strokeWeight: 2,
                fillColor: value.colors,
                fillOpacity: 0.6,
                map: map,
                bounds: new google.maps.LatLngBounds(
                    new google.maps.LatLng(value.swY, value.swX),
                    new google.maps.LatLng(value.neY, value.neX)
                )
            });

            rec.addListener('click', function(e) {
                spotDetailsService.showDialog(rec.id);
            });

            rectanglesArray.push(rec);
        });
    }

    function findNewRecs(newRecs) {
        var recsToBeAdded = [];

        function rectanglesArrayContains(element) {
            var arrayLength = rectanglesArray.length;

            for (var i = 0; i < arrayLength; i++) {
                if (Math.round(rectanglesArray[i].getBounds().f.b * 1000) / 1000 === Math.round(element.neY * 1000) / 1000 &&
                    Math.round(rectanglesArray[i].getBounds().b.b * 1000) / 1000 === Math.round(element.swX * 1000) / 1000) {
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

    function findUnneededRecs(newRecs) {
        var recsToBeRemoved = [];

        function newRecsContains(element) {
            var arrayLength = newRecs.length;
            for (var i = 0; i < arrayLength; i++) {
                if (Math.round(element.getBounds().f.b * 1000) / 1000 === Math.round(newRecs[i].neY * 1000) / 1000 &&
                    Math.round(element.getBounds().b.b * 1000) / 1000 === Math.round(newRecs[i].swX * 1000) / 1000) {
                    return true;
                }
            }
            return false;
        }

        //updating main rectangles container
        for (var i = 0; i < rectanglesArray.length; i++) {
            if (!newRecsContains(rectanglesArray[i])) {
                recsToBeRemoved.push.apply(recsToBeRemoved, rectanglesArray.splice(i, 1));
                i--;
            }
        }
        return recsToBeRemoved;
    }

    function updateRecColors(recs) {
        var arrayLength = rectanglesArray.length;

        angular.forEach(recs, function (element) {
            for (var i = 0; i < arrayLength; i++) {
                if (Math.round(rectanglesArray[i].getBounds().f.b * 1000) / 1000 === Math.round(element.neY * 1000) / 1000 &&
                    Math.round(rectanglesArray[i].getBounds().b.b * 1000) / 1000 === Math.round(element.swX * 1000) / 1000 &&
                    rectanglesArray[i].fillColor !== element.colors) {
                    rectanglesArray[i].setOptions({
                        fillColor: element.colors,
                        strokeColor: element.borderColor
                    });
                }
            }
        });
    }

    return {
        handleRecs : function(data, map) {
            var recsToBeRemoved = findUnneededRecs(data);
            removeUnneededRecsFromMap(recsToBeRemoved);
            var recsToBeAdded = findNewRecs(data);
            addNewRecsToMap(recsToBeAdded, map);
        },
        removeAllRecs : function() {
            removeUnneededRecsFromMap(rectanglesArray);
            rectanglesArray.length = 0;
        },
        updateRecColors : updateRecColors
    };
}

module.exports = ['spotDetailsService', rectanglesService];