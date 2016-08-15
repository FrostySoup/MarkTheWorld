/*global module */
'use strict';

function toplistService($mdDialog) {
    return {
        showDialog: function (ev) {
            $mdDialog.show({
                controller: 'toplistController',
                controllerAs: 'vm',
                templateUrl: '/Scripts/dialogs/toplist/toplist.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                fullscreen: true,
                focusOnOpen: false,
                clickOutsideToClose: true
            });
        }
    };
}

module.exports = ['$mdDialog', toplistService];