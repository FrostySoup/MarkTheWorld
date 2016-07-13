/*global FB, module, document */
/*global document */

'use strict';

function facebookLoginInitializerService($window, $q) {
    var deferObject = $q.defer();

    $window.fbAsyncInit = function () {
        FB.init({
            appId: '211779402536909',
            version: 'v2.6',
            cookie: true
        });

        deferObject.resolve();
    };

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {return;}
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

    return {
        facebookInitialized: deferObject.promise
    };
}
module.exports = ['$window', '$q', facebookLoginInitializerService];