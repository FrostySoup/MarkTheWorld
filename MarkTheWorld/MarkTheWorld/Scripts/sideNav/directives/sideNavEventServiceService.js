/*global angular */
(function () {
    'use strict';

    function sideNavEventServiceService($rootScope) {
        return {
            emit: function (sideNavAction) { $rootScope.$emit("sidebar:open", sideNavAction); },
            listen: function (callback) { $rootScope.$on("sidebar:open", callback); }
        };
    }

    angular.module('sideNav').factory('sideNavEventServiceService', sideNavEventServiceService);
}());