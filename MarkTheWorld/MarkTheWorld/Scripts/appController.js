/*global angular */
(function () {
    'use strict';

    function AppController(accountService, myProfilePictureService) {
        accountService.appStartUpLoginCheck();
        myProfilePictureService.showDialog()
    }

    angular.module('markTheWorld').controller('AppController', AppController);
}());