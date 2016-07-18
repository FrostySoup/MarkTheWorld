/*global module */
'use strict';

function spotDetailsService($mdDialog) {
    return {
        showDialog: function (ev, id) {
            $mdDialog.show({
                controller: 'spotDetailsController',
                controllerAs: 'vm',
                templateUrl: '/Scripts/dialogs/spotDetails/spotDetails.html',
                locals: {
                    id: id
                },
                bindToController: true,
                parent: angular.element(document.body),
                targetEvent: ev,
                fullscreen: true,
                focusOnOpen: false,
                clickOutsideToClose: true
            });
        }
    };
}

module.exports = ['$mdDialog', spotDetailsService];