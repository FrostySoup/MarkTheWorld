/*global angular */
/*global localStorage */
(function () {
    'use strict';

    function userService() {
        return {
            isLogged: false,
            username: '',
            token: '',
            currentPosition: null
        };
    }
    angular.module('account').factory('userService', userService);
}());