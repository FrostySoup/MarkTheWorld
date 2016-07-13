/* global module */
'use strict';

function registerSideNavDirective() {
    return {
        templateUrl: '/Scripts/layout/sideNav/register/registerSideNavDirective.html',
        bindToController: true,
        controllerAs: 'vm',
        scope: {},
        restrict: 'E',
        controller: 'registerSideNavDirectiveController'
    };
}

module.exports = [registerSideNavDirective];