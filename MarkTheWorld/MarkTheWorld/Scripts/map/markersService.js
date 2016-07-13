/*global module */
'use strict';

function markersService() {
    var markersArray = [];

    function findUnneededMarkers(newMarkers) {
        var markersToBeRemoved = [];

        function newRecsContains(element) {
            var arrayLength = newMarkers.length;
            for (var i = 0; i < arrayLength; i++) {

                if (Math.round(element.getPosition().lat() * 1000) / 1000 === Math.round(newMarkers[i].lat * 1000) / 1000 &&
                    Math.round(element.getPosition().lng() * 1000) / 1000 === Math.round(newMarkers[i].lon * 1000) / 1000 &&
                    newMarkers[i].count === element.count) {
                    return true;
                }
            }
            return false;
        }

        //updating main markers container
        for (var i = 0; i < markersArray.length; i++) {
            if (!newRecsContains(markersArray[i])) {
                markersToBeRemoved.push.apply(markersToBeRemoved, markersArray.splice(i, 1));
                i--;
            }
        }

        return markersToBeRemoved;
    }

    function findNewMarkers(newMarkers) {
        var markersToBeAdded = [];

        function markersArrayContains(element) {
            var arrayLength = markersArray.length;

            for (var i = 0; i < arrayLength; i++) {
                if (Math.round(markersArray[i].getPosition().lat() * 1000) / 1000 === Math.round(element.lat * 1000) / 1000 &&
                    Math.round(markersArray[i].getPosition().lng() * 1000) / 1000 === Math.round(element.lon * 1000) / 1000 &&
                    markersArray[i].count === element.count) {
                    return true;
                }
            }

            return false;
        }

        angular.forEach(newMarkers, function (value) {
            if (!markersArrayContains(value)) {
                markersToBeAdded.push(value);
            }
        });

        return markersToBeAdded;
    }

    function removeUnneededMarkersFromMap(unneededMarkers) {
        var arrayLength = unneededMarkers.length;
        for (var i = 0; i < arrayLength; i++) {
            unneededMarkers[i].setMap(null);
        }
    }

    function addNewMarkersToMap(markersToBeAdded, map) {
        angular.forEach(markersToBeAdded, function (value) {
            var size = 32 + value.count;
            if (size % 2 !== 0) {
                size = size + 1;
            }
            if (size > 64) {
                size = 64;
            }

            var image = 'data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" width="' + size + '" height="' + size + '" viewBox="0 0 38 38"><path fill="rgb(197,17,98)" d="M34.305 16.234c0 8.83-15.148 19.158-15.148 19.158S3.507 25.065 3.507 16.1c0-8.505 6.894-14.304 15.4-14.304 8.504 0 15.398 5.933 15.398 14.438z"/><text transform="translate(19 18.5)" fill="#fff" style="font-family: Roboto, \'Helvetica Neue\', sans-serif;font-weight:bold;text-align:center;" font-size="12" text-anchor="middle">' + value.count + '</text></svg>';

            var marker = new google.maps.Marker({
                count: value.count,
                position: {'lat': value.lat, 'lng': value.lon},
                icon: image,
                map: map
            });

            markersArray.push(marker);
        });
    }

    return {
        handleMarkers: function (data, map) {
            var markersToBeRemoved = findUnneededMarkers(data);
            removeUnneededMarkersFromMap(markersToBeRemoved);
            var markersToBeAdded = findNewMarkers(data);
            addNewMarkersToMap(markersToBeAdded, map);
        },
        removeAllMarkers: function () {
            removeUnneededMarkersFromMap(markersArray);
            markersArray.length = 0;
        }
    };
}

module.exports = [markersService];
