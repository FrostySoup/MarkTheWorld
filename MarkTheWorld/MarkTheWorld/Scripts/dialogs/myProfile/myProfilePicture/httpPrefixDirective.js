/*global module */
'use strict';

function httpPrefixDirective() {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, controller) {
            function ensureHttpPrefix(value) {
                if (value && !/^(https?):\/\//i.test(value)
                        && 'http://'.indexOf(value) !== 0 && 'https://'.indexOf(value) !== 0) {
                    return 'http://' + value;
                }

                return value;
            }

            controller.$parsers.splice(0, 0, ensureHttpPrefix);
        }
    };
}

module.exports = [httpPrefixDirective];