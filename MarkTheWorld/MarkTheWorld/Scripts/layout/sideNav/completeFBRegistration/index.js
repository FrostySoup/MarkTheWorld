/* global require, module */

'use strict';
module.exports =
    require('angular')
        .module('sideNav.completeFBRegistration', [])
        .directive('completeSideNav', require('./completeSideNavDirective.js'))
        .controller('completeSideNavDirectiveController', require('./completeSideNavDirectiveController.js'))
        .name;