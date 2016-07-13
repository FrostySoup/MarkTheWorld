/* global document, module */
'use strict';

function googleMapsInitializerService($window, $q) {
    // maps loader deferred object
    var mapsDefer = $q.defer();

    // Google's url for async maps initialization accepting callback function
    var asyncUrl = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyAwKyMjlVSL0h2mO89h_t0rkVSUlSzoyVw&callback=';

    // async loader
    var asyncLoad = function (asyncUrl, callbackName) {
        var script = document.createElement('script');
        script.src = asyncUrl + callbackName;
        script.async = true;
        script.defer = true;
        document.body.appendChild(script);
    };

    // callback function - resolving promise after maps successfully loaded
    $window.googleMapsInitialized = mapsDefer.resolve;

    // loading google maps
    asyncLoad(asyncUrl, 'googleMapsInitialized');

    return {
        mapsInitialized : mapsDefer.promise
    };
}

module.exports = ['$window', '$q', googleMapsInitializerService];