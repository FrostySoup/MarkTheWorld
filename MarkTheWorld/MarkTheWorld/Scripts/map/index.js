/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('map', [
            require('./googleMapsInitializer')
        ])
        .factory('mapService', require('./mapService.js'))
        .factory('markersService', require('./markersService.js'))
        .factory('rectanglesService', require('./rectanglesService.js'))
        .directive('gameMap', require('./mapDirective.js'))
        .name;