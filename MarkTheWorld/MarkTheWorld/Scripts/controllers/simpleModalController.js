(function () {
    'use strict';

    app.controller('SimpleModalController', SimpleModalController);

    function SimpleModalController($scope, $mdDialog, title, content) {
        $scope.title = title;
        $scope.content = content;
        $scope.cancel = function() {
            $mdDialog.cancel();
        };
    }
})();