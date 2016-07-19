/*global module */

    function appController(accountService) {
        accountService.appStartUpLoginCheck();
    }


module.exports = ['accountService', appController];