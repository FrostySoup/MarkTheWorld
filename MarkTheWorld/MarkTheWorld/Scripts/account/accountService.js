/*global angular */
/*global localStorage */
(function () {
    'use strict';

    function accountService($http, $q, $timeout, simpleModalService) {
        return {
            login: function (loginData) {
                console.log('tyring to login');

                var deferredObject = $q.defer();

                $http.post('/api/User/Login',
                    {
                        "UserName": loginData.UserName,
                        "PasswordHash": loginData.PasswordHash
                    }).
                    then(
                        function (success) {
                            deferredObject.resolve(success);
                        },
                        function (error) {
                            deferredObject.reject(error);
                        }
                    );

                return deferredObject.promise;
            },

            register: function (user) {
                var deferredObject = $q.defer();

                $http.post('/api/User',
                    {
                        "UserName": user.UserName,
                        "PasswordHash": user.PasswordHash
                    }).
                    then(
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

            logout: function () {
                simpleModalService.showModal('Info', 'Logged out successfully');
                $timeout(function () {
                    localStorage.removeItem('token');
                    localStorage.removeItem('user');
                    localStorage.removeItem('onlyMyOwnMarks');
                }, 300);
            }
        };
    }
    angular.module('account').factory('accountService', accountService);
}());