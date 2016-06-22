/*global angular */
(function () {
    'use strict';

    function myProfileColorService($mdDialog, $q, $http, userService) {

        return {
            showDialog: function (color) {
                return $mdDialog.show({
                    controller: 'myProfileColorController',
                    controllerAs: 'vm',
                    templateUrl: 'scripts/myProfile/myProfileColor/myProfileColor.html',
                    locals: {
                        color: color
                    },
                    bindToController: true,
                    parent: angular.element(document.body),
                    fullscreen: true,
                    focusOnOpen: false,
                    clickOutsideToClose: true
                });
            },
            saveColor: function (color) {
                var deferredObject = $q.defer();
                var address = 'api/color/' + userService.username;
                $http.post(address,
                    {
                        "red": color.red,
                        "green": color.green,
                        "blue": color.blue
                    }).then(
                    function (success) {
                        console.log(success);
                        deferredObject.resolve();
                    },
                    function (error) {
                        deferredObject.reject(error);
                    }
                );

                return deferredObject.promise;
            }
        };
    }

    angular.module('myProfileColor').factory('myProfileColorService', myProfileColorService);
}());