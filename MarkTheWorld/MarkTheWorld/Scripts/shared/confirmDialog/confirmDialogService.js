/*global module */
'use strict';

function confirmDialogService($mdDialog) {
    return {
        showConfirmDialog: function (settings) {
            var confirm = $mdDialog.confirm({
                title: settings.title,
                textContent: settings.message,
                ariaLabel: settings.ariaLabel,
                targetEvent: settings.ev,
                ok: settings.okText,
                cancel: settings.cancelText,
                focusOnOpen: false,
                clickOutsideToClose: true
            });

            return $mdDialog.show(confirm);
        }
    };
}

module.exports = ['$mdDialog', confirmDialogService];