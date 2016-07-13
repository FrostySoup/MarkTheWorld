/* global require, module */

'use strict';
module.exports =
    require('angular')
        .module('sideNav.register', [])
        .directive('registerSideNav', require('./registerSideNavDirective.js'))
        .controller('registerSideNavDirectiveController', require('./registerSideNavDirectiveController.js'))
        .name;