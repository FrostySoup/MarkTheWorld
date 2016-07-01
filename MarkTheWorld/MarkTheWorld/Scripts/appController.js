/*global angular */
(function () {
    'use strict';

    function AppController(accountService) {
        accountService.appStartUpLoginCheck();
    }

    angular.module('markTheWorld').controller('AppController', AppController);
}());