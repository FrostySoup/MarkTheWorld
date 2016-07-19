/*global module */
'use strict';

function filterMapController($mdDialog, $q, $http, mapService, userService, filterMapEventService) {
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

    vm.showUserSpots = function (username) {
        vm.pending = true;
        vm.usernameError = false;

        $http.get('api/username/' + username)
            .then(function (response) {
                if (response.data !== true) {
                    mapService.mapFilter.filter = username;
                    mapService.updateMap();
                    filterMapEventService.emit(true);
                    vm.cancel();
                }
                else {
                    vm.usernameError = true;
                }
            })
            .finally(function () {
                vm.pending = false;
            });
    };

    vm.querySearch = function (query) {
        if (query.length === 0) {
            return [];
        }

        var deferredObject = $q.defer();

        $http.get('/api/autocomplete/' + query + '/5').then(
            function (success) {
                deferredObject.resolve(success.data);
            },
            function (error) {
                deferredObject.reject(error);
            }
        );

        return deferredObject.promise;
    };

    vm.showMySpots = function () {
        mapService.mapFilter.filter = userService.username;
        filterMapEventService.emit(true);
        mapService.updateMap();
        vm.cancel();
    };

    vm.clearMapFilter = function () {
        mapService.mapFilter.filter = null;
        filterMapEventService.emit(false);
        mapService.updateMap();
        vm.cancel();
    };
}

module.exports = ['$mdDialog', '$q', '$http', 'mapService', 'userService', 'filterMapEventService', filterMapController];