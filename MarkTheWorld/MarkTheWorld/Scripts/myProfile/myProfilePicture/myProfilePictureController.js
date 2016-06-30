/*global angular */
(function () {
    'use strict';

    function myProfilePictureController($scope, $mdDialog, Upload, userService) {
        var vm = this;

        vm.croppedFileUrl = '';

        vm.cancel = function () {
            $mdDialog.cancel();
        };

        // upload later on form submit or something similar
        vm.submit = function () {
            //if ($scope.form.file.$valid && vm.file) {
                vm.upload();
            //}
        };

        // upload on file select or drop
        vm.upload = function () {
            Upload.upload({
                url: 'api/uploading',
                data: {
                    file: Upload.dataUrltoBlob(vm.croppedFileUrl, vm.file.name),
                    token: userService.token
                }
            }).then(function (resp) {
                console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
            }, function (resp) {
                console.log('Error status: ' + resp.status);
            }, function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
            });
        };
    }

    angular.module('myProfilePicture').controller('myProfilePictureController', myProfilePictureController);
}());