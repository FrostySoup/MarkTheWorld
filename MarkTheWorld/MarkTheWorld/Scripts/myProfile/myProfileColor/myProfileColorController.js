/*global angular */
(function () {
    'use strict';

    function myProfileColorController($mdDialog, myProfileColorService, toastService) {
        var vm = this;

        vm.cancel = function () {
            $mdDialog.cancel();
        };

        vm.requesting = false;

        vm.saveColor = function () {
            vm.requesting = true;
            myProfileColorService.saveColor(vm.color).then(
                function () {
                    toastService.showToast('Color changed', 5000);
                    vm.cancel();
                },
                function (error) {
                    console.log(error);
                }
            ).finally(function () {
                vm.requesting = false;
            });
        };
    }

    angular.module('myProfileColor').controller('myProfileColorController', myProfileColorController);
}());