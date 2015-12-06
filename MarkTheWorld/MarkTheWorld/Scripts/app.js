/**
 * Created by Andy on 2015.10.25.
 */
var app = angular.module('MarkApp', ['ngMaterial', 'ui.router']);

app.config(['$stateProvider', function($stateProvider) {
        $stateProvider
            .state('login', {
                name: 'login',
                templateUrl: 'scripts/templates/login.html'
            })
            .state('register', {
                name: 'register',
                templateUrl: 'scripts/templates/register.html'
            });
    }]);

function makeBounds( nw, metersEast, metersSouth ) {
    var ne = google.maps.geometry.spherical.computeOffset(
        nw, metersEast, 90
    );
    var sw = google.maps.geometry.spherical.computeOffset(
        nw, metersSouth, 180
    );
    return new google.maps.LatLngBounds( sw, ne );
}

function drawGrid() {
    var mapViewWidth = Math.abs(map.getBounds().getSouthWest().lat() - map.getBounds().getNorthEast().lat());
    var mapViewHeight = Math.abs(map.getBounds().getSouthWest().lng() - map.getBounds().getNorthEast().lng());
    var cellWidth = mapViewWidth / 6;
    var cellHeight = mapViewHeight / 6;

    for (var i = map.getBounds().getSouthWest().lat(); i < map.getBounds().getNorthEast().lat(); i = i + cellWidth) {
        for (var j = map.getBounds().getSouthWest().lng(); j < map.getBounds().getNorthEast().lng(); j = j + cellWidth) {
            map.drawRectangle({
                mouseover: function(e) {
                    this.setOptions(
                        {
                            fillOpacity: 0.5
                        }
                    )
                },
                mouseout: function(e) {
                    this.setOptions(
                        {
                            fillOpacity: 0
                        }
                    )
                },
                strokePosition: google.maps.StrokePosition.OUTSIDE,
                bounds: [[i, j], [i + cellWidth, j + cellWidth]],
                strokeColor: '#0D47A1',
                fillColor: '#1565C0',
                fillOpacity: 0,
                strokeOpacity: 0.5,
                strokeWeight: 1
            });
        }
    }

    console.log(mapViewWidth);
};

function getSquareCoordinates(lat, lng, distance) {
    var latDiff = (360 / 40075) * (distance / 1000);
    var lonDiff = (360 / 23903.297) * (distance / 1000);
    return {
        'lat_min' : lat - latDiff,
        'lat_max' : lat + latDiff,
        'lon_min' : lng - lonDiff,
        'lon_max' : lng + lonDiff
    };
}

var myStyles2 = [{"featureType":"landscape","stylers":[{"saturation":-100},{"lightness":65},{"visibility":"on"}]},{"featureType":"poi","stylers":[{"saturation":-100},{"lightness":51},{"visibility":"simplified"}]},{"featureType":"road.highway","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"road.arterial","stylers":[{"saturation":-100},{"lightness":30},{"visibility":"on"}]},{"featureType":"road.local","stylers":[{"saturation":-100},{"lightness":40},{"visibility":"on"}]},{"featureType":"transit","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"administrative.province","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"labels","stylers":[{"visibility":"on"},{"lightness":-25},{"saturation":-100}]},{"featureType":"water","elementType":"geometry","stylers":[{"hue":"#ffff00"},{"lightness":-25},{"saturation":-97}]}];
map = new GMaps({
    div: '#map',
    lat: -12.043333,
    lng: -77.028333,
    width: '100%',
    height: 'calc(100% - 64px)',
    streetViewControl: false,
    mapTypeId: google.maps.MapTypeId.ROADMAP,
    styles: myStyles2,
    markerClusterer: function(map) {
        return new MarkerClusterer(map);
    },
    click: function(e) {
        //var bounds = getSquareCoordinates(e.latLng.lat(), e.latLng.lng(), 300);
        var marker = new google.maps.Marker({
            map: map.map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: { lat: e.latLng.lat(), lng: e.latLng.lng() }
        });
        //map.addMarker({
        //    lat: e.latLng.lat(),
        //    lng: e.latLng.lng()
        //});
        window.lat = e.latLng.lat();
        window.lng = e.latLng.lng();
        //map.drawRectangle({
        //    bounds: [[bounds.lat_min, bounds.lon_min], [bounds.lat_max, bounds.lon_max]],
        //    strokeColor: '#131540',
        //    strokeOpacity: 1,
        //    strokeWeight: 1
        //});
        //drawGrid();
        console.log(JSON.stringify(e.latLng));
        //console.log([[map.getBounds().getSouthWest().lat(), map.getBounds().getSouthWest().lng()], [map.getBounds().getNorthEast().lat(), map.getBounds().getNorthEast().lng()]]);
    }
});