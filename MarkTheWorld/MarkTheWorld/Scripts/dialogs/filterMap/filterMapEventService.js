/*global module */
'use strict';

function filterMapEventService($rootScope) {
    return {
        emit: function (filterValue) { $rootScope.$emit("map:filter", filterValue); },
        listen: function (callback) { $rootScope.$on("map:filter", callback); }
    };
}

module.exports = ['$rootScope', filterMapEventService];