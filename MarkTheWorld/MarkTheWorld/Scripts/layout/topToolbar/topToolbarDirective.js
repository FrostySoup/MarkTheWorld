/* global FB, module, require */
'use strict';

function topToolbarDirective() {
    return {
        templateUrl: '/Scripts/layout/topToolbar/topToolbarDirective.html',
        bindToController: true,
        controllerAs: 'vm',
        scope: {},
        restrict: 'E',
        replace: true,
        controller: 'topToolbarDirectiveController'
    };
}

module.exports = [topToolbarDirective];