(function () {
    'use strict';

    app.factory('SimpleModalFactory', SimpleModalFactory);

    function SimpleModalFactory($mdDialog) {
        return {
            showModal: function (title, content) {
                $mdDialog.show({
                    controller: 'SimpleModalController',
                    templateUrl: 'scripts/templates/simpleDialog.html',
                    parent: angular.element(document.body),
                    locals: { "title": title, "content" : content },
                    bindToController: true,
                    clickOutsideToClose: true
                });
            }
        };
    }
})();