(function () {
    'use strict';

    app.controller('MapSettingsCtrl', MapSettingsCtrl);

    function MapSettingsCtrl($scope, $mdDialog, AccountFactory, SimpleModalFactory) {

        $scope.switch = false;

        $scope.getSwitchState = function() {
            return $scope.switch ? 'on' : 'off';
        }

        $scope.cancel = function () {
            $mdDialog.hide();
        };

        $scope.confirm = function () {
            localStorage.setItem('onlyMyOwnMarks', $scope.switch);
            $mdDialog.cancel();
        };
    }
})();