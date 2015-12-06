(function () {
    'use strict';

    app.factory('SquareInfoFactory', SquareInfoFactory);

    function SquareInfoFactory ($mdDialog) {
        return {
            showDialog : function(markers) {
                $mdDialog.show({
                    controller: 'PointInfoController',
                    templateUrl: 'scripts/templates/pointMarkersDialog.html',
                    parent: angular.element(document.body),
                    locals: { "markers": markers },
                    bindToController: true,
                    clickOutsideToClose: true
                });
            }
        };
    }
})();