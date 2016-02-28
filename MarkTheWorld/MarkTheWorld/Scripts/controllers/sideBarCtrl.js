/*global angular */
/*global localStorage */

(function () {
    'use strict';

    function SidebarCtrl($scope, $mdSidenav, $log, $state, accountService, simpleModalService) {
        //TODO: maybe separate library for routes unneeded
        $scope.go = function (route) {
            $state.transitionTo(route);
        };

        $scope.login = function (loginForm, loginData) {
            if (!loginForm.$valid) {
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

        $scope.register = function (registerForm, registerData) {
            if (!registerForm.$valid) {
                return;
            }

            accountService.register(
                {
                    "UserName": registerData.username,
                    "PasswordHash": registerData.password
                }
            ).then(function (data) {
                if (data.success === true) {
                    $scope.close();
                    simpleModalService.showModal('Success!', 'Welcome ' + registerData.username + '!');
                    localStorage.setItem('token', data.Token);
                    localStorage.setItem('user', registerData.username);
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