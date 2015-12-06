(function () {
    'use strict';

    app.controller('PointInfoController', PointInfoController);

    function PointInfoController ($scope, $mdDialog, markers) {
        $scope.markers = markers;
        $scope.cancel = function() {
            $mdDialog.cancel();
        };
    }
})();