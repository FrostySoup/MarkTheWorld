/*global angular */
/*global FB */
(function () {
    'use strict';

    function facebookLoginService(facebookLoginInitializerService, $http, $q, sideNavEventServiceService) {
        var loginCallBack = function (response) {
            $http.post('/api/fblogin',
                {
                    "Id": response.authResponse.userID,
                    "Token": response.authResponse.accessToken
                }).then(
                function (success) {
                    console.log('success', success);
                    if (success.data.newUser === true) {
                        sideNavEventServiceService.emit({ action: 'complete', extraData: { fbResponse: response, apiResponse: success.data }});
                    }
                },
                function (error) {
                    //TODO: [preRelease] error api call toasts
                    console.log('error', error);
                }
            );
        };

        function loginSuccess(user) {
            localStorage.setItem('username', user.username);
            localStorage.setItem('token', user.Token);
            userService.isLogged = true;
            userService.username = user.username;
            userService.token = user.Token;
        }

        return {
            login: function () {
                facebookLoginInitializerService.facebookInitialized.then(function () {
                    FB.login(function (response) {
                        if (response.status === 'connected' && angular.isDefined(response.authResponse)) {
                            loginCallBack(response);
                        }
                    });
                });
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
                        loginSuccess({ username: registerData.username, token: success.Token });
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