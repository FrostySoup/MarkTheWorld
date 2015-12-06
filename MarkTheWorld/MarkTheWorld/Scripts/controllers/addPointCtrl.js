(function () {
    'use strict';

    app.controller('AddPointCtrl', AddPointCtrl);

    function AddPointCtrl ($scope, $mdDialog) {
        $scope.cancel = function() {
            $mdDialog.cancel();
        };

        $scope.confirm = function() {
            console.log(window.currLocation.lat+0.01, window.currLocation.lat);
            map.drawRectangle({
                bounds: [[window.currLocation.lat, window.currLocation.lng], [window.currLocation.lat+0.01, window.currLocation.lng+0.01]],
                strokeColor: '#131540',
                strokeOpacity: 1,
                strokeWeight: 1
            });
            $mdDialog.hide().then(function() {
                $mdDialog.show({
                    controller: 'AddPointCtrl',
                    templateUrl: 'scripts/templates/pointAddedDialog.html',
                    parent: angular.element(document.body),
                    clickOutsideToClose: true
                });
            });
        }
    }
})();