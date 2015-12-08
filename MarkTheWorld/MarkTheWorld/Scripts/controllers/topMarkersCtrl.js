(function () {
    'use strict';

    app.controller('TopMarkersCtrl', TopMarkersCtrl);

    function TopMarkersCtrl($scope, $mdDialog, $http) {

        $scope.toplist = {};

        $http.get('/api/topList/').
            success(function (data) {
                if (data) {
                    console.log(data);
                    $scope.toplist = data;
                }
            });

        $scope.cancel = function () {
            $mdDialog.hide();
        };
    }
})();