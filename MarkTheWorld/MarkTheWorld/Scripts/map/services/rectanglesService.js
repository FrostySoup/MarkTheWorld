/*global angular */
(function () {
    'use strict';
    var rectanglesArray = [];

    function rectanglesService(squareDetailsService) {

        function removeUnneededRecsFromMap(unneededRecs) {
            var arrayLength = unneededRecs.length;
            for (var i = 0; i < arrayLength; i++) {
                unneededRecs[i].setMap(null);
            }
        }

        function addNewRecsToMap(recsToBeAdded, map) {
            angular.forEach(recsToBeAdded, function (value) {
                var rec = new google.maps.Rectangle({
                    strokeColor: 'rgb(255,64,129)',
                    strokeOpacity: 1,
                    strokeWeight: 1,
                    fillColor: 'rgb(255,64,129)',
                    map: map,
                    bounds: new google.maps.LatLngBounds(
                        new google.maps.LatLng(value.swY, value.swX),
                        new google.maps.LatLng(value.neY, value.neX)
                    )
                });
                //TODO: should use accountService
                //rec.addListener('click', function(e) {
                //    if (localStorage.getItem('onlyMyOwnMarks') !== 'true') {
                //        squareDetailsService.showDialog(value.markers);
                //    }
                //});
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
            }
        };
    }

    angular.module('map').factory('rectanglesService', rectanglesService);
}());