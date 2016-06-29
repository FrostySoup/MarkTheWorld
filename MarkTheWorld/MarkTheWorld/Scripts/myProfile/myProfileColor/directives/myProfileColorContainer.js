/*global angular */
(function () {
    'use strict';

    function myProfileColorContainer(myProfileColorService, $timeout) {
        return {
            templateUrl: '/Scripts/myProfile/myProfileColor/directives/myProfileColorContainer.html',
            bindToController: true,
            controllerAs: 'vm',
            scope: {
                color: "="
            },
            restrict: 'E',
            controller: function ($scope, $element) {
                var vm = this;
                vm.updateColor = function (rgb) {
                    $scope.$apply(function () {
                        vm.color.red = +rgb[0];
                        vm.color.green = +rgb[1];
                        vm.color.blue = +rgb[2];
                    });
                };
                //TODO: [preRelease] gets called everytime user opens the dialog
                //TODO: [polishing] probably not the greatest way to wait for DOM ready
                $timeout(function () { myProfileColorService.createDOM($element, vm.updateColor); });
            }
        };
    }

    angular.module('myProfileColor').directive('profileColorContainer', myProfileColorContainer);
}());