/*global module */
'use strict';

function bonusPointsDirective() {
    return {
        templateUrl: '/Scripts/dialogs/myProfile/bonusPointsDirective.html',
        scope: {
            profileData: "="
        },
        bindToController: true,
        controllerAs: 'vm',
        restrict: 'E',
        controller: 'bonusPointsDirectiveController'
    };
}

module.exports = [bonusPointsDirective];