/* global require, module */

'use strict';
module.exports =
    require('angular')
        .module('layout.actionButtons', [])
        .directive('actionButtons', require('./actionButtonsDirective.js'))
        .controller('actionButtonsDirectiveController', require('./actionButtonsDirectiveController.js'))
        .name;