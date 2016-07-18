/*global module */
'use strict';

function filterMapController($mdDialog, $q, $http, $timeout, mapService, userService) {
    var vm = this;

    vm.cancel = function () {
        $mdDialog.cancel();
    };

    vm.message = '';
    vm.filtered = false;
    vm.onlyMyOwn = false;
    vm.pending = false;
    vm.usernameError = false;

    if (mapService.mapFilter.filter === userService.username) {
        vm.message = 'only your own spots';
        vm.filtered = true;
        vm.onlyMyOwn = true;
    }
    else if (mapService.mapFilter.filter === null) {
        vm.message = 'all spots';
    }
    else {
        vm.message = 'only ' + mapService.mapFilter.filter + ' spots';
        vm.filtered = true;
    }

    vm.showUserSpots = function(username) {
        console.log('showUserSpots');
        var deferredObject = $q.defer();
        vm.pending = true;
        vm.usernameError = false;

        $http.get('api/username/' + username)
            .then(function (response) {
                $timeout(function () {
                    if (response.data !== true) {
                        mapService.mapFilter.filter = username;
                        mapService.updateMap();
                        vm.cancel();
                        deferredObject.resolve();
                    }
                    else {
                        deferredObject.reject();
                        vm.usernameError = true;
                    }
                    vm.pending = false;
                }, 2000);
            },
            function () {
                deferredObject.reject();
            })
            .finally(function() {
                //vm.pending = false;
            });

        return deferredObject.promise;
    };

    vm.querySearch = function(query) {
        if (query.length === 0) {
            return [];
        }

        var deferredObject = $q.defer();

        $http.get('/api/autocomplete/' + query + '/5').then(
            function (success) {
                $timeout(function () {
                    if (success.data.indexOf(query) !== -1) {
                        deferredObject.resolve([]);
                    }
                    else {
                        deferredObject.resolve(success.data);
                    }
                }, 1000, false);

            },
            function (error) {
                deferredObject.reject(error);
            }
        );
        return deferredObject.promise;
    };

    vm.showMySpots = function() {
        mapService.mapFilter.filter = userService.username;
        mapService.updateMap();
        vm.cancel();
    };

    vm.clearMapFilter = function() {
        mapService.mapFilter.filter = null;
        mapService.updateMap();
        vm.cancel();
    };
}

module.exports = ['$mdDialog', '$q', '$http', '$timeout', 'mapService', 'userService', filterMapController];