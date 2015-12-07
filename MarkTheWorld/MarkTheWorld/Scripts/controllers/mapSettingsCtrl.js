(function () {
    'use strict';

    app.controller('MapSettingsCtrl', MapSettingsCtrl);

    function MapSettingsCtrl($scope, $mdDialog, AccountFactory, SimpleModalFactory) {

        $scope.switch = false;

        $scope.getSwitchState = function() {
            return $scope.switch ? 'on' : 'off';
        }

        $scope.cancel = function () {
            console.log('cancel');
            $mdDialog.hide();
        };

        $scope.confirm = function () {
            console.log('confirm');
            SimpleModalFactory.showModal('Congrats!', 'You marked a spot!');
            $mdDialog.cancel();
        };
    }
})();