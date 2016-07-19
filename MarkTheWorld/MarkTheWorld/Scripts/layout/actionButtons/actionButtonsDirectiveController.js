/* global module */
'use strict';

function actionButtonsDirectiveController (claimSpotService, filterMapEventService, confirmDialogService, mapService) {
    var vm = this;
    vm.mapFiltered = false;

    vm.claimSpot = function (ev) {
        claimSpotService.showDialog(ev);
    };

    vm.clearFilter = function (ev) {
        confirmDialogService.showConfirmDialog(
        {
            title: 'Clear map filter?',
            message: 'Do you want to clear the filter and display everyone\'s spots?',
            ariaLabel: 'Clear map filter?',
            ev: ev,
            okText: 'Clear',
            cancelText: 'Cancel'
        }).then(function() {
                mapService.mapFilter.filter = null;
                filterMapEventService.emit(false);
                mapService.updateMap();
        });
    };

    filterMapEventService.listen(function(event, filterValue) {
        vm.mapFiltered = filterValue;
    });

    //$scope.$watch(function () {
    //    return userService.currentPosition;
    //}, function (newVal, oldVal) {
    //    if (newVal !== null) {
    //        $http.post('/api/dotCheck',
    //            {
    //                "token": userService.token,
    //                "lat": userService.currentPosition.lat,
    //                "lng": userService.currentPosition.lng,
    //                "message": message
    //            }).then(
    //            function (success) {
    //                console.log(success);
    //            },
    //            function (error) {
    //                console.log(error);
    //            }
    //        );
    //    }
    //});
}

module.exports = ['claimSpotService', 'filterMapEventService', 'confirmDialogService', 'mapService', actionButtonsDirectiveController];