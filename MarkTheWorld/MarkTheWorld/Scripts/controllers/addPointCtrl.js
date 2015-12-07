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
    }
})();