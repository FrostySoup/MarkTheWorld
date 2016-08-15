/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('dialogs.toplist', [])
        .controller('toplistController', require('./toplistController.js'))
        .factory('toplistService', require('./toplistService.js'))
        .name;