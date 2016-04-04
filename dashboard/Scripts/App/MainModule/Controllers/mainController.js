(function (module) {
    var mainController = (function () {
        function mainController($rootScope) {
            this.size = 0;
            this.rootScope = $rootScope;
        }

        mainController.prototype.init = function () {
            var self = this;
            self.rootScope.$on('personInfoLoaded', function (event, data) {
                if (data == true) {
                    self.size = 4;
                }
                else {
                    self.size = 0;
                }
            });
        }

        return mainController;
    })();

    module.controller('mainController', ['$rootScope', mainController]);
})(mainModule);