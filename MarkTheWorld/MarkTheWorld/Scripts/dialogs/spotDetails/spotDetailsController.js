/*global module */
'use strict';

function spotDetailsController($mdDialog, $http, mapService, filterMapEventService) {
    var vm = this;

    vm.cancel = function () {
        $mdDialog.cancel();
    };

    vm.userMap = function (username) {
        mapService.mapFilter.filter = username;
        mapService.updateMap();
        filterMapEventService.emit(true);
        vm.cancel();
    };

    $http.get('/api/square/' + vm.id).then(
        function (success) {
            vm.spotDetails = success.data;
        },
        function (error) {
            console.log('error', error);
        }
    );
}

module.exports = ['$mdDialog', '$http', 'mapService', 'filterMapEventService', spotDetailsController];