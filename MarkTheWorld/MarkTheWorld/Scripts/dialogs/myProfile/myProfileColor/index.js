/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('dialogs.myProfile.myProfileColor', [])
        .controller('myProfileColorController', require('./myProfileColorController.js'))
        .factory('myProfileColorService', require('./myProfileColorService.js'))
        .directive('profileColorContainer', require('./myProfileColorContainerDirective.js'))
        .controller('myProfileColorContainerDirectiveController', require('./myProfileColorContainerDirectiveController.js'))
        .name;