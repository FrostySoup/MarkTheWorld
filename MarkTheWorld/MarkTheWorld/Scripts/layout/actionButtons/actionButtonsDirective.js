/* global module, require */
'use strict';

function actionButtonsDirective() {
    return {
        templateUrl: '/Scripts/layout/actionButtons/actionButtonsDirective.html',
        controllerAs: 'vm',
        scope: {},
        restrict: 'E',
        controller: 'actionButtonsDirectiveController'
    };
}

module.exports = [actionButtonsDirective];