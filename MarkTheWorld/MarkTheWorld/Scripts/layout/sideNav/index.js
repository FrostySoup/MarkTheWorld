/* global require, module */

'use strict';
module.exports =
    require('angular')
        .module('layout.sideNav', [
            require('./completeFBRegistration'),
            require('./login'),
            require('./register')
        ])
        .value('countries', require('./countriesValue.js'))
        .value('statesUS', require('./statesUSValue.js'))
        .factory('sideNavEventService', require('./sideNavEventService.js'))
        .directive('sideNav', require('./sideNavDirective.js'))
        .controller('sideNavDirectiveController', require('./sideNavDirectiveController.js'))
        .directive('uniqueUsername', require('./uniqueUsernameDirective.js'))
        .name;