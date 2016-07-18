/*global localStorage, module */
'use strict';

function accountService($http, $q, userService, facebookLoginService) {
    function loginSuccess(user) {
        console.log('login success', user);
        localStorage.setItem('username', user.username);
        localStorage.setItem('token', user.Token);
        localStorage.setItem('avatar', user.photo);
        userService.isLogged = true;
        userService.username = user.username;
        userService.token = user.Token;
        userService.avatar = user.photo;
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
            localStorage.removeItem('avatar');
            userService.isLogged = false;
            userService.username = '';
            userService.token = '';
            userService.currentPosition = null;
            userService.avatar = null;

            facebookLoginService.logout().then(function() {
                deferredObject.resolve();
            });

            return deferredObject.promise;
        },

        appStartUpLoginCheck: function () {
            var token = localStorage.getItem('token');
            var username = localStorage.getItem('username');
            var avatar = localStorage.getItem('avatar');

            if (token !== null && username !== null) {
                userService.isLogged = true;
                userService.username = username;
                userService.token = token;
                if (avatar !== null) {
                    userService.avatar = avatar;
                }
            }
        },

        updateAvatar: function (avatarAddress) {
            localStorage.setItem('avatar', avatarAddress);
            userService.avatar = avatarAddress;
        }
    };
}
module.exports = ['$http', '$q', 'userService', 'facebookLoginService', accountService];

