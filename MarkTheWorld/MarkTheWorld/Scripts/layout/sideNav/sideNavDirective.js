/* global module */
'use strict';

function sideNavDirective() {
    return {
        templateUrl: '/Scripts/layout/sideNav/sideNavDirective.html',
        bindToController: true,
        controllerAs: 'vm',
        scope: {},
        restrict: 'E',
        controller: 'sideNavDirectiveController'
    };
}
module.exports = [sideNavDirective];