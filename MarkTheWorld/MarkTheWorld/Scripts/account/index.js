/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('account', [])
        .factory('accountService', require('./accountService.js'))
        .factory('facebookLoginInitializerService', require('./facebookLoginInitializerService.js'))
        .factory('facebookLoginService', require('./facebookLoginService.js'))
        .factory('userService', require('./userService.js'))
        .name;