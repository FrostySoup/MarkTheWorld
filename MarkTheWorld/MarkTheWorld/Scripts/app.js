/*global angular */
(function () {
    'use strict';

    angular.module('markTheWorld', ['ngMaterial', 'ui.router', 'map', 'squareDetails', 'newSquare', 'mapSettings', 'topMarkers', 'shared'])
        .config(['$stateProvider', function ($stateProvider) {
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
}());