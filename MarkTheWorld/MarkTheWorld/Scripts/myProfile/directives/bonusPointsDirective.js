/*global angular */
(function () {
    'use strict';

    function bonusPointsDirective() {
        return {
            templateUrl: '/Scripts/myProfile/directives/bonusPointsDirective.html',
            scope: {
                profileData: "="
            },
            bindToController: true,
            controllerAs: 'vm',
            restrict: 'E',
            controller: function (myProfileService, toastService, $interval, $scope) {
                var vm = this;
                vm.takingPoints = false;
                vm.situation = 'timer';

                $scope.$watch('vm.profileData', function(profileData) {
                    if (angular.isDefined(profileData)) {
                        init(profileData.dailies.timeLeft, profileData.dailies.points);
                    }
                });

                vm.takePoints = function () {
                    vm.takingPoints = true;
                    myProfileService.takePoints().then(
                        function (success) {
                            var data = success.data;
                            vm.profileData.points = data.totalPoints;
                            vm.situation = 'timer';
                            init(data.timeLeft, data.received);
                            toastService.showToast('You have received ' + data.received + (data.received > 1 ? ' points' : ' point') + '!', 5000);
                        },
                        function (error) {
                            console.log(error);
                            toastService.showToast('Server error', 5000);
                        }
                    ).finally(function () {
                            vm.takingPoints = false;
                        });
                };

                function init (timeLeftInSeconds, pointsBonus) {
                    vm.pointsBonus = pointsBonus;

                    if (timeLeftInSeconds < 1) {
                        vm.situation = 'pointsAvailable';
                        return;
                    }

                    vm.formattedTimeLeft = myProfileService.getFormattedTime(timeLeftInSeconds);
                    startTimer(--timeLeftInSeconds);
                }

                function startTimer (timeLeftInSeconds) {
                    myProfileService.interval = $interval(function () {
                        if (timeLeftInSeconds < 1) {
                            vm.situation = 'pointsAvailable';
                            myProfileService.cancelInterval();
                        }
                        vm.formattedTimeLeft = myProfileService.getFormattedTime(timeLeftInSeconds);
                        timeLeftInSeconds--;
                    }, 1000);
                }
            }
        };
    }

    angular.module('myProfile').directive('bonusPoints', bonusPointsDirective);
}());