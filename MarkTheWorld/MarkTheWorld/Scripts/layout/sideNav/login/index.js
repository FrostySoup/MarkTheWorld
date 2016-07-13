/* global require, module */

'use strict';
module.exports =
    require('angular')
        .module('sideNav.login', [])
        .directive('loginSideNav', require('./loginSideNavDirective.js'))
        .controller('loginSideNavDirectiveController', require('./loginSideNavDirectiveController.js'))
        .name;