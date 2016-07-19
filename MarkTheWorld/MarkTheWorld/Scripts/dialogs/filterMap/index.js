/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('dialogs.filterMap', [])
        .controller('filterMapController', require('./filterMapController.js'))
        .factory('filterMapService', require('./filterMapService.js'))
        .factory('filterMapEventService', require('./filterMapEventService.js'))
        .name;