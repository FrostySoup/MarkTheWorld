/*global angular */
(function () {
    'use strict';

    function myProfileService($mdDialog, $http, $q) {
        return {
            showDialog: function (ev) {
                $mdDialog.show({
                    controller: 'myProfileController',
                    controllerAs: 'vm',
                    templateUrl: 'scripts/myProfile/myProfile.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    fullscreen: true,
                    clickOutsideToClose: true
                });
            },
            getProfileData: function () {
                var deferredObject = $q.defer();
                var username = localStorage.getItem('username');

                if (username === null) {
                    deferredObject.reject('Username isn\'t set');
                }

                $http.get('/api/profile/Login' + username).then(
                    function (success) {
                        deferredObject.resolve(success);
                    },
                    function (error) {
                        deferredObject.reject(error);
                    }
                );

                return deferredObject.promise;
            },
            getFormattedTime: function (t) {
                var days, hours, minutes, seconds;
                days = Math.floor(t / 86400);
                t -= days * 86400;
                hours = Math.floor(t / 3600) % 24;
                t -= hours * 3600;
                minutes = Math.floor(t / 60) % 60;
                t -= minutes * 60;
                seconds = t % 60;
                return [
                    days + 'd',
                    hours + 'h',
                    minutes + 'm',
                    seconds + 's'
                ].join(' ');
            }
        };
    }

    angular.module('myProfile').factory('myProfileService', myProfileService);
}());