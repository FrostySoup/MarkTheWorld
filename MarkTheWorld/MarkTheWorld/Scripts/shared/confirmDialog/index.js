/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('shared.confirmDialog', [])
        .factory('confirmDialogService', require('./confirmDialogService.js'))
        .name;