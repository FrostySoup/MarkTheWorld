/*global angular */
/*global localStorage */
(function () {
    'use strict';

    function accountService($http, $q, userService, facebookLoginService) {
        function loginSuccess(user) {
            console.log('login success', user);
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

            logout: function () {
                var deferredObject = $q.defer();

                localStorage.removeItem('token');
                localStorage.removeItem('username');
                userService.isLogged = false;
                userService.username = '';
                userService.token = '';

                facebookLoginService.logout().then(function() {
                    deferredObject.resolve();
                });

                return deferredObject.promise;
            },

            appStartUpLoginCheck: function () {
                var token = localStorage.getItem('token');
                var username = localStorage.getItem('username');

                if (token !== null && username !== null) {
                    userService.isLogged = true;
                    userService.username = username;
                    userService.token = token;
                }
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
            }
        };
    }
    angular.module('account').factory('accountService', accountService);
}());