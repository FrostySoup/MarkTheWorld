/*global module */
'use strict';

function sideNavEventService($rootScope) {
    return {
        emit: function (sideNavAction) { $rootScope.$emit("sidebar:open", sideNavAction); },
        listen: function (callback) { $rootScope.$on("sidebar:open", callback); }
    };
}

module.exports = ['$rootScope', sideNavEventService];