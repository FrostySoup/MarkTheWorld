/*global angular */
/*global localStorage */

(function () {
    'use strict';

    function SidebarCtrl($scope, $mdSidenav, $log, $state, accountService, simpleModalService) {
        //TODO: maybe separate library for routes unneeded
        $scope.go = function (route) {
            $state.transitionTo(route);
        };

        $scope.login = function (loginData) {
            if (!loginData.username || !loginData.password) {
                return;
            }

            accountService.login(
                {
                    "UserName": loginData.username,
                    "PasswordHash": loginData.password
                }
            ).then(function (data) {
                if (data.success === true) {
                    $scope.close();
                    simpleModalService.showModal('Success!', 'Welcome back ' + loginData.username + '!');
                    localStorage.setItem('token', data.Token);
                    localStorage.setItem('user', loginData.username);
                } else {
                    simpleModalService.showModal('Error!', data.message);
                }
            });
        };

        $scope.register = function (username, password) {
            if (!username || !password) {
                return;
            }

            accountService.register(
                {
                    "UserName": username,
                    "PasswordHash": password
                }
            ).then(function (data) {
                if (data.success === true) {
                    $scope.close();
                    simpleModalService.showModal('Success!', 'Welcome ' + username + '!');
                    localStorage.setItem('token', data.Token);
                    localStorage.setItem('user', username);
                } else {
                    simpleModalService.showModal('Error!', data.message);
                }
            });
        };

        $scope.close = function () {
            $mdSidenav('right').close()
                .then(function () {
                    $log.debug("close RIGHT is done");
                });
        };
    }
    angular.module('markTheWorld').controller('SidebarCtrl', SidebarCtrl);
}());