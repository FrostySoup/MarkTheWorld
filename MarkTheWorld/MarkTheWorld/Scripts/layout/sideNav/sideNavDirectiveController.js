/* global module */
'use strict';

function sideNavDirectiveController ($mdSidenav, sideNavEventService) {
    var vm = this;

    sideNavEventService.listen(function (event, data) {
        vm.sideNavAction = data.action;
        vm.extraData = data.extraData;
        $mdSidenav('right_side_nav').open();
    });

    vm.openPanel = function (sideNavAction) {
        vm.sideNavAction = sideNavAction;
        sideNavEventService.emit({ action: sideNavAction });
    };

    vm.switchPanel = function (sideNavAction) {
        $mdSidenav('right_side_nav').close().then(function () {
            sideNavEventService.emit({ action: sideNavAction });
        });
    };
}

module.exports = ['$mdSidenav', 'sideNavEventService', sideNavDirectiveController];