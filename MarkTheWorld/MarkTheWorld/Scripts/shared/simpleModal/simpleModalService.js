/*global angular */
(function () {
    'use strict';

    function simpleModalService($mdDialog) {
        return {
            showModal: function (title, content) {
                $mdDialog.show({
                    controller: 'simpleModalController',
                    templateUrl: 'scripts/shared/simpleModal/simpleModal.html',
                    parent: angular.element(document.body),
                    locals: { "title": title, "content" : content },
                    bindToController: true,
                    clickOutsideToClose: true
                });
            }
        };
    }
    angular.module('simpleModal').factory('simpleModalService', simpleModalService);
}());