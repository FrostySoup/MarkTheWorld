/*global angular */
/*global Image */
(function () {
    'use strict';

    function myProfilePictureController($mdDialog, Upload, userService, $q) {
        var vm = this;

        vm.croppedFileUrl = '';
        vm.cropFileSource = '';

        vm.cancel = function () {
            $mdDialog.cancel();
        };

        vm.fileSelected = false;
        vm.file = null;
        vm.webImageUrl = '';
        vm.fileError = '';
        vm.pending = false;

        function formImage(src) {
            var deferred = $q.defer();
            var image = new Image();

            if (src.substring(0, 4).toLowerCase() === 'http') {
                image.crossOrigin = 'anonymous';
            }

            image.onerror = function () {
                deferred.reject();
            };

            image.onload = function () {
                deferred.resolve(image);
            };
            image.src = src;

            return deferred.promise;
        }

        vm.handleSelectedFile = function (fileForm) {
            vm.pending = true;
            if (fileForm.$valid && vm.file) {
                Upload.base64DataUrl(vm.file).then(
                    function (url) {
                        vm.cropFileSource = url;
                        vm.fileSelected = true;
                        vm.pending = false;
                    }
                );
            } else {
                var error = Object.keys(fileForm.$error)[0];
                switch (error) {
                case 'pattern':
                    vm.fileError = 'Only images are allowed';
                    break;
                case 'minHeight':
                    vm.fileError = 'Image dimensions should be at least 100x100';
                    break;
                case 'minWidth':
                    vm.fileError = 'Image dimensions should be at least 100x100';
                    break;
                case 'maxSize':
                    vm.fileError = 'Image should be smaller than 20MB';
                    break;
                }
            }
        };

        vm.handleEnteredURL = function (url) {
            vm.pending = true;

            formImage(url).then(
                function (image) {
                    if (image.width < 100 || image.height < 100) {
                        vm.fileError = 'Image dimensions should be at least 100x100';
                        vm.webImageUrl = '';
                        return;
                    }
                    vm.cropFileSource = image.src;
                    vm.fileSelected = true;
                },
                function () {
                    vm.fileError = 'Couldn\'t load the image';
                    vm.webImageUrl = '';
                }
            ).finally(function () {
                vm.pending = false;
            });
        };

        vm.cancelImageCrop = function () {
            vm.fileSelected = false;
            vm.file = null;
            vm.webImageUrl = '';
            vm.fileError = '';
            vm.pending = false;
        };

        vm.upload = function () {
            vm.pending = true;
            var fileName = vm.file ? vm.file.name.substr(0, vm.file.name.lastIndexOf(".")) + ".png" : 'web-image.png';

            Upload.upload({
                url: 'api/uploading',
                data: {
                    file: Upload.dataUrltoBlob(vm.croppedFileUrl, fileName),
                    token: userService.token
                }
            }).then(
                function (success) {
                    console.log(success);
                    vm.cancel();
                },
                function (error) {
                    console.log(error);
                }
            ).finally(function () {
                vm.pending = false;
            });
        };
    }

    angular.module('myProfilePicture').controller('myProfilePictureController', myProfilePictureController);
}());