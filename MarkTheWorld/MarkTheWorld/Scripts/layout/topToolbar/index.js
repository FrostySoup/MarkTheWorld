/* global require, module */

'use strict';
module.exports =
    require('angular')
        .module('topToolbar', [])
        .directive('topToolbar', require('./topToolbarDirective.js'))
        .controller('topToolbarDirectiveController', require('./topToolbarDirectiveController.js'))
        .name;