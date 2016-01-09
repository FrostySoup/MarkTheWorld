/*global angular */
(function () {
    'use strict';

    function AccountFactory($http, $q) {
        return {
            login: function (user) {
                var deferredObject = $q.defer();

                $http.post(
                    '/api/getUser', {
                        "UserName": user.UserName,
                        "PasswordHash": user.PasswordHash
                    }
                ).
                success(function (data) {
                    if (data) {
                        console.log(data);
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

            register: function (user) {
                var deferredObject = $q.defer();

                $http.post(
                    '/api/addUser', {
                        "UserName": user.UserName,
                        "PasswordHash": user.PasswordHash
                    }
                ).
                success(function (data) {
                    if (data) {
                        console.log(data);
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

            addPoint: function (message, lat, lng) {
                var deferredObject = $q.defer();

                $http.post(
                    '/api/User', {
                        "username": localStorage.getItem('token'),
                        "lat": lat,
                        "lng": lng,
                        "message": message
                    }
                ).
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
    angular.module('markTheWorld').factory('AccountFactory', AccountFactory);
}());