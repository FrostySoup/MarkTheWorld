/*global angular */
/*global FB */
/*global document */

(function () {
    'use strict';

    angular.module('markTheWorld', ['ngMaterial', 'account', 'map', 'squareDetails', 'sideNav', 'topToolbar',
        'newSquare', 'mapSettings', 'myProfile', 'topMarkers', 'shared', 'ngMessages'])
        .run(function ($window) {
            $window.fbAsyncInit = function () {
                FB.init({
                    appId: '1176412742402979',
                    version: 'v2.6',
                    cookie: true,
                    status: true
                });

                FB.Event.subscribe('auth.statusChange', function (response) {
                    console.log('auth.statusChange', response);
                });
            };

            (function (d, s, id) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) {return;}
                js = d.createElement(s); js.id = id;
                js.src = "//connect.facebook.net/en_US/sdk.js";
                fjs.parentNode.insertBefore(js, fjs);
            }(document, 'script', 'facebook-jssdk'));
        });
}());