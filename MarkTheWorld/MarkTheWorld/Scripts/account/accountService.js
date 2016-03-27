/*global angular */
/*global localStorage */
(function () {
    'use strict';

    function accountService($http, $q, $timeout, confirmDialogService) {
        return {
            login: function (loginData) {
                console.log('tyring to login');

                var deferredObject = $q.defer();

                $http.post('/api/User/Login',
                    {
                        "UserName": loginData.username,
                        "PasswordHash": loginData.password
                    }).then(
                    function (success) {
                        deferredObject.resolve(success);
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
                        deferredObject.resolve(success);
                    },
                    function (error) {
                        deferredObject.reject(error);
                    }
                );

                return deferredObject.promise;
            },

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

            isLogged: function () {
                return localStorage.getItem('token') !== null;
            },

            getMapUser: function () {
                return localStorage.getItem('mapUser');
            },

            setMapUser: function (mapUser) {
                return localStorage.setItem('mapUser', mapUser);
            },

            getLoggedUser: function () {
                return localStorage.getItem('user');
            },

            logout: function (ev) {
                confirmDialogService.showConfirmDialog({
                    title: 'Logging out',
                    message: 'Are you sure you want to log out?',
                    ariaLabel: 'Logging out',
                    ev: ev,
                    okText: 'Yes',
                    cancelText: 'No'
                }).then(function () {
                    localStorage.removeItem('token');
                    localStorage.removeItem('user');
                    localStorage.removeItem('onlyMyOwnMarks');
                });
            }
        };
    }
    angular.module('account').factory('accountService', accountService);
}());