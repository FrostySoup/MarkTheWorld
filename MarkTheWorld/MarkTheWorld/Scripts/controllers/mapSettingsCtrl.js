(function () {
    'use strict';

    app.controller('MapSettingsCtrl', MapSettingsCtrl);

    function MapSettingsCtrl($scope, $mdDialog, AccountFactory, SimpleModalFactory, MarkMapFactory) {

        if (localStorage.getItem('onlyMyOwnMarks') === 'true') {
            $scope.selection = 'my';
        } else {
            $scope.selection = 'all';
        }
        

        $scope.cancel = function () {
            $mdDialog.hide();
        };

        $scope.confirm = function () {
            if ($scope.selection === 'my') {
                localStorage.setItem('onlyMyOwnMarks', true);
            } else if ($scope.selection === 'all') {
                localStorage.setItem('onlyMyOwnMarks', false);
            }

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