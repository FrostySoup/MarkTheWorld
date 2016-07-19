/* global require, module */

'use strict';
module.exports =
    require('angular')
        .module('layout', [
            require('./sideNav'),
            require('./topToolbar'),
            require('./actionButtons')
        ])
        .name;