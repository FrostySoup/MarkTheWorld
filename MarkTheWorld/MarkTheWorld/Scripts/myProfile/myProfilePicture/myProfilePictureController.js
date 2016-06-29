/*global angular */
(function () {
    'use strict';

    function myProfilePictureController($scope, $mdDialog, Upload) {
        var vm = this;

        vm.cancel = function () {
            $mdDialog.cancel();
        };

        // upload later on form submit or something similar
        vm.submit = function() {
            if ($scope.form.file.$valid && vm.file) {
                vm.upload(vm.file);
            }
        };

        // upload on file select or drop
        vm.upload = function (file) {
            Upload.upload({
                url: 'api/uploading',
                data: {file: file, 'username': $scope.username}
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