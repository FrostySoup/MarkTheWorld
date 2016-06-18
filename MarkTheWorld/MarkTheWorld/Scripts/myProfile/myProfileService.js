/*global angular */
(function () {
    'use strict';

    function myProfileService($mdDialog, $http, $q, $interval, accountService) {
        return {
            showDialog: function (ev) {
                var self = this;
                $mdDialog.show({
                    controller: 'myProfileController',
                    controllerAs: 'vm',
                    templateUrl: 'scripts/myProfile/myProfile.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    fullscreen: true,
                    clickOutsideToClose: true
                }).finally(function () {
                    self.cancelInterval();
                });
            },

            getProfileData: function () {
                var deferredObject = $q.defer();
                var username = accountService.getLoggedUser();

                if (username === null) {
                    deferredObject.reject('Username isn\'t set');
                }

                $http.get('/api/profile/' + username).then(
                    function (success) {
                        deferredObject.resolve(success);
                    },
                    function (error) {
                        deferredObject.reject(error);
                    }
                );

                return deferredObject.promise;
            },

            takePoints: function () {
                var deferredObject = $q.defer();
                var username = accountService.getLoggedUser();

                if (username === null) {
                    deferredObject.reject('Username isn\'t set');
                }

                $http.get('/api/daily/' + username).then(
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
                var hours, minutes, seconds;
                hours = Math.floor(t / 3600);
                t -= hours * 3600;
                minutes = Math.floor(t / 60);
                t -= minutes * 60;
                seconds = t;
                return [
                    hours + 'h',
                    minutes + 'm',
                    seconds + 's'
                ].join(' ');
            },

            cancelInterval: function () {
                if (angular.isDefined(this.interval)) {
                    $interval.cancel(this.interval);
                }
            },

            interval: undefined
        };
    }

    angular.module('myProfile').factory('myProfileService', myProfileService);
}());