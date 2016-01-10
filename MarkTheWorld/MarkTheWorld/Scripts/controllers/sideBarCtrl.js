/*global angular */
(function () {
    'use strict';

    function SidebarCtrl($scope, $mdSidenav, $log, $state, accountService, simpleModalService) {
        $scope.go = function (route) {
            $state.transitionTo(route);
        };

        $scope.login = function (username, password) {
            if (!username || !password) {
                return;
            }

            accountService.login(
                {
                    "UserName": username,
                    "PasswordHash": password
                }
            ).then(function (data) {
                if (data.success === true) {
                    $scope.close();
                    simpleModalService.showModal('Success!', 'Welcome back ' + username + '!');
                    localStorage.setItem('token', data.Token);
                    localStorage.setItem('user', username);
                } else {
                    SimpleModalFactory.showModal('Error!', data.message);
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