/*global localStorage module*/
'use strict';

function userService() {
    return {
        isLogged: false,
        username: '',
        token: '',
        currentPosition: null,
        avatar: null
    };
}
module.exports = [userService];