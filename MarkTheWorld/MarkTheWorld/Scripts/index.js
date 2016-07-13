/*global require */

'use strict';
require('angular')
    .module('markTheWorld', [
        require('angular-material'),
        require('angular-animate'),
        require('angular-aria'),
        require('angular-messages'),
        require('./map'),
        require('./account'),
        require('./dialogs'),
        require('./layout'),
        require('./shared')
    ])
    .controller('appController', require('./appController.js'));