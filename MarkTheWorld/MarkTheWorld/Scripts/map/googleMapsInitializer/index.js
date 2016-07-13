/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('googleMapsInitializer', [])
        .factory('googleMapsInitializerService', require('./googleMapsInitializerService.js'))
        .name;