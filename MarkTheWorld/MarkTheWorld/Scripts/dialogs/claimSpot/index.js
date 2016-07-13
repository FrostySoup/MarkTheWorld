/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('dialogs.claimSpot', [])
        .controller('claimSpotController', require('./claimSpotController.js'))
        .factory('claimSpotService', require('./claimSpotService.js'))
        .name;