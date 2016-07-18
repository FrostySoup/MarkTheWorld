/*global module */
'use strict';

function filterMapService($mdDialog) {
    return {
        showDialog: function (ev) {
            $mdDialog.show({
                controller: 'filterMapController',
                controllerAs: 'vm',
                templateUrl: '/Scripts/dialogs/filterMap/filterMap.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                fullscreen: true,
                focusOnOpen: false,
                clickOutsideToClose: true
            });
        }
    };
}

module.exports = ['$mdDialog', filterMapService];