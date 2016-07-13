/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('shared', [
            require('./toast')
        ])
        .name;