/*global module */
'use strict';

function myProfileColorService($mdDialog, $q, $http, userService) {

    var palette = [
        ["rgb(255, 204, 204)","rgb(255, 230, 204)","rgb(255, 255, 204)","rgb(204, 255, 204)","rgb(204, 255, 230)","rgb(204, 255, 255)","rgb(204, 230, 255)","rgb(204, 204, 255)","rgb(230, 204, 255)","rgb(255, 204, 255)"],
        ["rgb(255, 153, 153)","rgb(255, 204, 153)","rgb(255, 255, 153)","rgb(153, 255, 153)","rgb(153, 255, 204)","rgb(153, 255, 255)","rgb(153, 204, 255)","rgb(153, 153, 255)","rgb(204, 153, 255)","rgb(255, 153, 255)"],
        ["rgb(255, 102, 102)","rgb(255, 179, 102)","rgb(255, 255, 102)","rgb(102, 255, 102)","rgb(102, 255, 179)","rgb(102, 255, 255)","rgb(102, 179, 255)","rgb(102, 102, 255)","rgb(179, 102, 255)","rgb(255, 102, 255)"],
        ["rgb(255, 51, 51)","rgb(255, 153, 51)","rgb(255, 255, 51)","rgb(51, 255, 51)","rgb(51, 255, 153)","rgb(51, 255, 255)","rgb(51, 153, 255)","rgb(51, 51, 255)","rgb(153, 51, 255)","rgb(255, 51, 255)"],
        ["rgb(255, 0, 0)","rgb(255, 128, 0)","rgb(255, 255, 0)","rgb(0, 255, 0)","rgb(0, 255, 128)","rgb(0, 255, 255)","rgb(0, 128, 255)","rgb(0, 0, 255)","rgb(128, 0, 255)","rgb(255, 0, 255)"],
        ["rgb(245, 0, 0)","rgb(245, 123, 0)","rgb(245, 245, 0)","rgb(0, 245, 0)","rgb(0, 245, 123)","rgb(0, 245, 245)","rgb(0, 123, 245)","rgb(0, 0, 245)","rgb(123, 0, 245)","rgb(245, 0, 245)"],
        ["rgb(214, 0, 0)","rgb(214, 108, 0)","rgb(214, 214, 0)","rgb(0, 214, 0)","rgb(0, 214, 108)","rgb(0, 214, 214)","rgb(0, 108, 214)","rgb(0, 0, 214)","rgb(108, 0, 214)","rgb(214, 0, 214)"],
        ["rgb(163, 0, 0)","rgb(163, 82, 0)","rgb(163, 163, 0)","rgb(0, 163, 0)","rgb(0, 163, 82)","rgb(0, 163, 163)","rgb(0, 82, 163)","rgb(0, 0, 163)","rgb(82, 0, 163)","rgb(163, 0, 163)"],
        ["rgb(92, 0, 0)","rgb(92, 46, 0)","rgb(92, 92, 0)","rgb(0, 92, 0)","rgb(0, 92, 46)","rgb(0, 92, 92)","rgb(0, 46, 92)","rgb(0, 0, 92)","rgb(46, 0, 92)","rgb(92, 0, 92)"],
        ["rgb(255, 255, 255)","rgb(205, 205, 205)","rgb(178, 178, 178)","rgb(153, 153, 153)","rgb(127, 127, 127)","rgb(102, 102, 102)","rgb(76, 76, 76)","rgb(51, 51, 51)","rgb(25, 25, 25)","rgb(0, 0, 0)"]
    ];

    function cellClickHandler(event, updateColor) {
        var rgb = event.target.style.backgroundColor;
        rgb = rgb.substring(4, rgb.length-1)
            .replace(/ /g, '')
            .split(',');

        updateColor(rgb);
    }

    return {
        showDialog: function (color) {
            return $mdDialog.show({
                controller: 'myProfileColorController',
                controllerAs: 'vm',
                templateUrl: '/Scripts/dialogs/myProfile/myProfileColor/myProfileColor.html',
                locals: {
                    color: color
                },
                bindToController: true,
                parent: angular.element(document.body),
                fullscreen: true,
                focusOnOpen: false,
                clickOutsideToClose: true
            });
        },

        createDOM: function (element, updateColor) {
            var paletteContainer = angular.element(element[0].querySelector('.color-picker-palette'));

            var paletteRow = angular.element('<div class="layout-row layout-align-space-between"></div>');
            var paletteCell = angular.element('<div class="flex-10 color-cell"></div>');

            angular.forEach(palette, function(value, key) {
                var row = paletteRow.clone();
                angular.forEach(value, function(color) {
                    var cell = paletteCell.clone();
                    cell.css({
                        backgroundColor: color
                    });

                    cell.bind('click', function( event ) {
                        cellClickHandler(event, updateColor);
                    });

                    row.append(cell);
                });

                paletteContainer.append(row);
            });
        },

        saveColor: function (color) {
            var deferredObject = $q.defer();
            var address = 'api/color/' + userService.username;
            $http.post(address,
                {
                    "red": color.red,
                    "green": color.green,
                    "blue": color.blue
                }).then(
                function (success) {
                    deferredObject.resolve();
                },
                function (error) {
                    deferredObject.reject(error);
                }
            );

            return deferredObject.promise;
        }
    };
}

module.exports = ['$mdDialog', '$q', '$http', 'userService', myProfileColorService];