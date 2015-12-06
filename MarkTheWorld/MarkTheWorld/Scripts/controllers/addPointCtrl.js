(function () {
    'use strict';

    app.controller('AddPointCtrl', AddPointCtrl);

    function AddPointCtrl($scope, $mdDialog, AccountFactory, SimpleModalFactory, lat, lng) {
        $scope.cancel = function() {
            $mdDialog.cancel();
        };

        $scope.confirm = function (message) {
            AccountFactory.addPoint(message, lat, lng).then(function (data) {
                if (data.success === true) {
                    $mdDialog.hide().then(function() {
                        SimpleModalFactory.showModal('Congrats!', 'You marked a spot!');
                    });
                } else {
                    SimpleModalFactory.showModal('Error!', data.message);
                }
            });
        };

            //map.drawRectangle({
            //    bounds: [[window.currLocation.lat, window.currLocation.lng], [window.currLocation.lat+0.01, window.currLocation.lng+0.01]],
            //    strokeColor: '#131540',
            //    strokeOpacity: 1,
            //    strokeWeight: 1
            //});
    }
})();