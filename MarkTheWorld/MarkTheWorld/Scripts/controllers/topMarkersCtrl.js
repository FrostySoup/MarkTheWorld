(function () {
    'use strict';

    app.controller('TopMarkersCtrl', TopMarkersCtrl);

    function TopMarkersCtrl($scope, $mdDialog) {
        $scope.cancel = function () {
            $mdDialog.hide();
        };
    }
})();