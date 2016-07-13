/* global module */
'use strict';

function completeSideNavDirective() {
    return {
        templateUrl: '/Scripts/layout/sideNav/completeFBRegistration/completeSideNavDirective.html',
        bindToController: true,
        controllerAs: 'vm',
        scope: {
            extraData: "="
        },
        restrict: 'E',
        controller: 'completeSideNavDirectiveController'
    };
}

module.exports = [completeSideNavDirective];