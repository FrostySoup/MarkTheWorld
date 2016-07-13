/*global module */
'use strict';

function toastController($scope, $mdToast, message) {
    $scope.message = message;

    $scope.closeToast = function () {
        $mdToast.hide();
    };
}

module.exports = ['$scope', '$mdToast', 'message', toastController];