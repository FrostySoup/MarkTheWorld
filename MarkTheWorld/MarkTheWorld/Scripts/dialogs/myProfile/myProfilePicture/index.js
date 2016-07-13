/* global module, require */

'use strict';

module.exports =
    require('angular')
        .module('dialogs.myProfile.myProfilePicture', [
            require('ng-img-crop-npm')
        ])
        .controller('myProfilePictureController', require('./myProfilePictureController.js'))
        .factory('myProfilePictureService', require('./myProfilePictureService.js'))
        .directive('httpPrefix', require('./httpPrefixDirective.js'))
        .name;