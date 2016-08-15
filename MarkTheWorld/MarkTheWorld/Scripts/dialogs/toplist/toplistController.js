/*global module */
'use strict';

function toplistController($mdDialog, countries, $q, $http) {
    var vm = this;
    vm.countries = countries;

    vm.cancel = function () {
        $mdDialog.cancel();
    };

    getToplist();

    function getToplist () {
        var address = '/api/topList?countryCode=&number=5&startingPage=1';

        $http.post(address).then(
            function (success) {
                console.log(success);
            },
            function (error) {
                console.log('error', error);
            }
        );
    }

}

module.exports = ['$mdDialog', 'countries', '$q', '$http', toplistController];