/*global angular */
(function () {
    'use strict';

    function mapSettingsService($mdDialog) {
        return {
            showDialog : function (ev) {
                $mdDialog.show({
                    controller: 'mapSettingsController',
                    templateUrl: 'scripts/mapSettings/mapSettings.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true
                });
            }
        };
    }

    angular.module('mapSettings').factory('mapSettingsService', mapSettingsService);
}());