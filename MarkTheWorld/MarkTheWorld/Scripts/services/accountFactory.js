(function () {
    'use strict';

    app.factory('AccountFactory', AccountFactory);

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
            }
        };
    }
})();