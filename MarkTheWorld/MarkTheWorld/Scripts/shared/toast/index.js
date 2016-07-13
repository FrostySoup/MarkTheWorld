/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('shared.toast', [])
        .controller('toastController', require('./toastController.js'))
        .factory('toastService', require('./toastService.js'))
        .name;