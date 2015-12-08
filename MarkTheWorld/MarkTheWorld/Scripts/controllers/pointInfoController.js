(function () {
    'use strict';

    app.controller('PointInfoController', PointInfoController);

    function PointInfoController($scope, $mdDialog, markers) {
        angular.forEach(markers, function (value, key) {
            value.date = new RegExp("[0-9-]*").exec(value.date)[0];
        });

        $scope.markers = markers;
        $scope.cancel = function() {
            $mdDialog.cancel();
        };
    }
})();