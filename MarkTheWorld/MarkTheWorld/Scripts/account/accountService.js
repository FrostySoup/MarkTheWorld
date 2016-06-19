/*global angular */
/*global localStorage */
(function () {
    'use strict';

    function accountService($http, $q, userService) {
        function loginSuccess(user) {
            localStorage.setItem('username', user.username);
            localStorage.setItem('token', user.Token);
            userService.isLogged = true;
            userService.username = user.username;
            userService.token = user.Token;
        }

        return {
            login: function (loginData) {
                var deferredObject = $q.defer();

                $http.post('/api/User/Login',
                    {
                        "UserName": loginData.username,
                        "PasswordHash": loginData.password
                    }).then(
                    function (success) {
                        loginSuccess(success.data);
                        deferredObject.resolve();
                    },
                    function (error) {
                        deferredObject.reject(error);
                    }
                );

                return deferredObject.promise;
            },

            register: function (registerData) {
                var deferredObject = $q.defer();

                $http.post('/api/User',
                    {
                        "UserName": registerData.username,
                        "PasswordHash": registerData.password,
                        "CountryCode": registerData.countryCode
                    }).then(
                    function (success) {
                        loginSuccess(success.data);
                        deferredObject.resolve();
                    },
                    function (error) {
                        deferredObject.reject(error);
                    }
                );

                return deferredObject.promise;
            },

            //todo: should be moved elsewhere
            addPoint: function (message, lat, lng) {
                var deferredObject = $q.defer();

                $http.post('/api/Dot',
                    {
                        "username": localStorage.getItem('token'),
                        "lat": lat,
                        "lng": lng,
                        "message": message
                    }).
                    success(function (data) {
                        if (data) {
                            console.log('addPoint success', data);
                            deferredObject.resolve(data);
                        } else {
                            deferredObject.resolve(false);
                        }
                    }).
                    error(function () {
                        deferredObject.resolve(false);
                    });

                return deferredObject.promise;
            },

            logout: function () {
                localStorage.removeItem('token');
                localStorage.removeItem('user');
                userService.isLogged = false;
                userService.username = '';
                userService.token = '';
            }
        };
    }
    angular.module('account').factory('accountService', accountService);
}());