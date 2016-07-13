/* global module */
'use strict';

function loginSideNavDirective() {
    return {
        templateUrl: '/Scripts/layout/sideNav/login/loginSideNavDirective.html',
        scope: {
            switchPanel: "&"
        },
        bindToController: true,
        controllerAs: 'vm',
        restrict: 'E',
        controller: 'loginSideNavDirectiveController'
    };
}

module.exports = [loginSideNavDirective];