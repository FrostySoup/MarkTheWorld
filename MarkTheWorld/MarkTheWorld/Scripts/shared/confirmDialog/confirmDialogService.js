/*global angular */
(function () {
    'use strict';

    function confirmDialogService($mdDialog) {
        return {
            showConfirmDialog: function (settings) {
                var confirm = $mdDialog.confirm()
                    .title(settings.title)
                    .textContent(settings.message)
                    .ariaLabel(settings.ariaLabel)
                    .targetEvent(settings.ev)
                    .ok(settings.okText)
                    .cancel(settings.cancelText);

                return $mdDialog.show(confirm);
            }
        };
    }
    angular.module('confirmDialog').factory('confirmDialogService', confirmDialogService);
}());