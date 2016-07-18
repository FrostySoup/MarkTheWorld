/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('dialogs.spotDetails', [])
        .controller('spotDetailsController', require('./spotDetailsController.js'))
        .factory('spotDetailsService', require('./spotDetailsService.js'))
        .name;