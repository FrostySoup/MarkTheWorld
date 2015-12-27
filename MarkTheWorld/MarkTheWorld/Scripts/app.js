/**
 * Created by Andy on 2015.10.25.
 */
var app = angular.module('markTheWorld', ['ngMaterial', 'ui.router', 'map']);

app.config(['$stateProvider', function ($stateProvider) {
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

var myStyles2 = [{"featureType":"landscape","stylers":[{"saturation":-100},{"lightness":65},{"visibility":"on"}]},{"featureType":"poi","stylers":[{"saturation":-100},{"lightness":51},{"visibility":"simplified"}]},{"featureType":"road.highway","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"road.arterial","stylers":[{"saturation":-100},{"lightness":30},{"visibility":"on"}]},{"featureType":"road.local","stylers":[{"saturation":-100},{"lightness":40},{"visibility":"on"}]},{"featureType":"transit","stylers":[{"saturation":-100},{"visibility":"simplified"}]},{"featureType":"administrative.province","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"labels","stylers":[{"visibility":"on"},{"lightness":-25},{"saturation":-100}]},{"featureType":"water","elementType":"geometry","stylers":[{"hue":"#ffff00"},{"lightness":-25},{"saturation":-97}]}];
var marker;

map = new GMaps({
    div: '#map',
    lat: -12.043333,
    lng: -77.028333,
    width: '100%',
    height: 'calc(100% - 64px)',
    streetViewControl: false,
    mapTypeId: google.maps.MapTypeId.ROADMAP,
    styles: myStyles2,
    //markerClusterer: function(map) {
    //    return new MarkerClusterer(map);
    //},
    click: function(e) {
        //var bounds = getSquareCoordinates(e.latLng.lat(), e.latLng.lng(), 300);
        if (marker) {
            marker.setMap(null);
        }
        marker = new google.maps.Marker({
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