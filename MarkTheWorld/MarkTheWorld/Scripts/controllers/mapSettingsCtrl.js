(function () {
    'use strict';

    app.controller('MapSettingsCtrl', MapSettingsCtrl);

    function MapSettingsCtrl($scope, $mdDialog, AccountFactory, SimpleModalFactory, MarkMapFactory) {

        if (localStorage.getItem('onlyMyOwnMarks') === 'true') {
            $scope.switch = true;
        } else {
            $scope.switch = false;
        }
        

        $scope.getSwitchState = function() {
            return $scope.switch ? 'on' : 'off';
        }

        $scope.cancel = function () {
            $mdDialog.hide();
        };

        $scope.confirm = function () {
            localStorage.setItem('onlyMyOwnMarks', $scope.switch);
            MarkMapFactory.clearMap();
            if (map.getZoom() < 12) {
                MarkMapFactory.markAllPoint();
            }
            else {
                MarkMapFactory.markRectangles();
            }
            $mdDialog.cancel();
        };
    }
})();