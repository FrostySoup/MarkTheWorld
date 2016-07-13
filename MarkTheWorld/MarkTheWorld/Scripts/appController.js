/*global module */

    function appController($scope, accountService, claimSpotService, userService, $http) {
        accountService.appStartUpLoginCheck();
        $scope.temp = function (ev) {
            claimSpotService.showDialog(ev);
        };

        //$scope.$watch(function () {
        //    return userService.currentPosition;
        //}, function (newVal, oldVal) {
        //    if (newVal !== null) {
        //        $http.post('/api/dotCheck',
        //            {
        //                "token": userService.token,
        //                "lat": userService.currentPosition.lat,
        //                "lng": userService.currentPosition.lng,
        //                "message": message
        //            }).then(
        //            function (success) {
        //                console.log(success);
        //            },
        //            function (error) {
        //                console.log(error);
        //            }
        //        );
        //    }
        //});
    }


module.exports = ['$scope', 'accountService', 'claimSpotService', 'userService', '$http', appController];