/*global angular */
/*global localStorage */
(function () {
    'use strict';

    function userService() {
        return {
            isLogged: false,
            username: '',
            token: ''
        };
    }
    angular.module('account').factory('userService', userService);
}());