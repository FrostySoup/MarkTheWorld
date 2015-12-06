(function () {
    'use strict';

    app.controller('SidebarCtrl', SidebarCtrl);

    function SidebarCtrl ($scope, $mdSidenav, $log, $state) {
        $scope.go = function(route) {
            $state.transitionTo(route, { param1 : 'something' }, { reload: true });
        };

        $scope.close = function () {
            $mdSidenav('right').close()
                .then(function () {
                    $log.debug("close RIGHT is done");
                });
        };
    }
})();