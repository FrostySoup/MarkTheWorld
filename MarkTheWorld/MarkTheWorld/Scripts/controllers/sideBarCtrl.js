/*global angular */
/*global localStorage */

(function () {
    'use strict';

    function SidebarCtrl($scope, $mdSidenav, $log, $state, accountService, simpleModalService, countries) {
        //TODO: maybe separate library for routes unneeded
        $scope.go = function (route) {
            $state.transitionTo(route);
        };

        $scope.requesting = {
            login: false,
            register: false
        };

        $scope.requestError = {
            login: undefined,
            register: undefined
        };

        $scope.countries = countries;

        $scope.login = function (loginForm, loginData) {
            if (!loginForm.$valid) {
                return;
            }

            $scope.requesting.login = true;
            $scope.requestError.login = undefined;

            accountService.login(loginData).then(
                function (success) {
                    console.log('login success: ', success);
                    localStorage.setItem('username', loginData.username);
                    localStorage.setItem('token', success.data.Token);
                    $scope.close();
                },
                function (error) {
                    console.log('login error: ', error);
                    $scope.requestError.login = error.data;
                }
            ).finally(function () {
                $scope.requesting.login = false;
            });
        };

        $scope.register = function (registerForm, registerData) {
            if (!registerForm.$valid) {
                return;
            }

            $scope.requesting.register = true;
            $scope.requestError.register = undefined;

            accountService.register(registerData).then(
                function (success) {
                    console.log('register success: ', success);
                    simpleModalService.showModal('Success!', 'Welcome ' + registerData.username + '!');
                    localStorage.setItem('token', success.data.Token);
                    localStorage.setItem('user', registerData.username);
                    $scope.close();
                },
                function (error) {
                    console.log('register error: ', error);
                    $scope.requestError.register = error.data;
                }
            ).finally(function () {
                $scope.requesting.register = false;
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