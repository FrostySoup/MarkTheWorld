﻿/*global angular */
(function () {
    'use strict';

    function toastService($mdToast, $document) {
        return {
            showToast: function (message, hideDelay) {
                hideDelay = hideDelay || 0;
                $mdToast.show({
                    controller: 'toastController',
                    templateUrl: 'scripts/shared/toast/toast.html',
                    parent : $document[0].querySelector('#toastBounds'),
                    hideDelay: hideDelay,
                    locals: { 'message' : message },
                    position: 'top right'
                });
            }
        };
    }
    angular.module('toast').factory('toastService', toastService);
}());