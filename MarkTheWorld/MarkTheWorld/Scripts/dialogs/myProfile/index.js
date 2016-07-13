/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('dialogs.myProfile', [
            require('./myProfileColor'),
            require('./myProfilePicture')
        ])
        .controller('myProfileController', require('./myProfileController.js'))
        .controller('bonusPointsDirectiveController', require('./bonusPointsDirectiveController.js'))
        .factory('myProfileService', require('./myProfileService.js'))
        .directive('bonusPoints', require('./bonusPointsDirective.js'))
        .name;