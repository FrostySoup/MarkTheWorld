/*global module */
'use strict';

function myProfileColorContainerDirectiveController ($scope, $element, myProfileColorService, $timeout) {
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

module.exports = ['$scope', '$element', 'myProfileColorService', '$timeout', myProfileColorContainerDirectiveController];