/*global angular */
/*global FB */
(function () {
    'use strict';

    function facebookLoginService(facebookLoginInitializerService, $http, $q, userService, sideNavEventServiceService) {
        function loginCallBack(response, deferredObject) {
            $http.post('/api/fblogin',
                {
                    "Id": response.authResponse.userID,
                    "Token": response.authResponse.accessToken
                }).then(
                function (success) {
                    if (success.data.newUser === true) {
                        sideNavEventServiceService.emit({ action: 'complete', extraData: { fbResponse: response, apiResponse: success.data }});
                        deferredObject.reject();
                    } else {
                        loginSuccess({username: success.data.username, Token: success.data.longToken });
                        deferredObject.resolve(success.data);
                    }
                },
                function (error) {
                    //TODO: [preRelease] error api call toasts
                    deferredObject.reject(error);
                    console.log('error', error);
                }
            );
        }

        function loginSuccess(user) {
            localStorage.setItem('username', user.username);
            localStorage.setItem('token', user.Token);
            userService.isLogged = true;
            userService.username = user.username;
            userService.token = user.Token;
        }

        return {
            logout: function () {
                var deferredObject = $q.defer();

                facebookLoginInitializerService.facebookInitialized.then(function () {
                    FB.getLoginStatus(function (response) {
                        if (response.status === 'connected') {
                            FB.logout(function() {
                                deferredObject.resolve();
                            });
                        } else {
                            deferredObject.resolve();
                        }
                    });
                });

                return deferredObject.promise;
            },

            login: function () {
                var deferredObject = $q.defer();

                facebookLoginInitializerService.facebookInitialized.then(function () {
                    FB.login(function (response) {
                        if (response.status === 'connected') {
                            loginCallBack(response, deferredObject);
                        }
                    });
                });

                return deferredObject.promise;
            },

            completeRegistration: function (registerData, fbResponse) {
                var deferredObject = $q.defer();

                $http.post('/api/fbRegister',
                    {
                        "userID": fbResponse.userID,
                        "token": fbResponse.accessToken,
                        "countryCode": registerData.countryCode,
                        "userName": registerData.username
                    }).then(
                    function (success) {
                        loginSuccess({ username: registerData.username, Token: success.data });
                        deferredObject.resolve(success);
                    },
                    function (error) {
                        deferredObject.reject(error);
                    }
                );

                return deferredObject.promise;
            }
        };
    }
    angular.module('account').factory('facebookLoginService', facebookLoginService);
}());