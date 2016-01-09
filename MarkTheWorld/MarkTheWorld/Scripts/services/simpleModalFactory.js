/*global angular */
(function () {
    'use strict';

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
    angular.module('markTheWorld').factory('SimpleModalFactory', SimpleModalFactory);
}());