/*global module */
'use strict';

function myProfileColorContainerDirective() {
    return {
        templateUrl: '/Scripts/dialogs/myProfile/myProfileColor/myProfileColorContainerDirective.html',
        bindToController: true,
        controllerAs: 'vm',
        scope: {
            color: "="
        },
        restrict: 'E',
        controller: 'myProfileColorContainerDirectiveController'
    };
}

module.exports = [myProfileColorContainerDirective];