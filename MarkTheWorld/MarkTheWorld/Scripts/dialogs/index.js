/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('dialogs', [
            require('ng-file-upload'),
            require('./claimSpot'),
            require('./myProfile')
        ])
        .name;