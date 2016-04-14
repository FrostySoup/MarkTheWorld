/*global angular */
(function () {
    'use strict';

    angular.module('markTheWorld', ['ngMaterial', 'ui.router', 'account', 'map', 'squareDetails',
        'newSquare', 'mapSettings', 'myProfile', 'topMarkers', 'shared', 'ngMessages'])
        .config(['$stateProvider', function ($stateProvider) {
            $stateProvider
                .state('login', {
                    name: 'login',
                    templateUrl: 'scripts/templates/login.html',
                    controller: 'SidebarCtrl'
                })
                .state('register', {
                    name: 'register',
                    templateUrl: 'scripts/templates/register.html',
                    controller: 'SidebarCtrl'
                });
        }]);
}());