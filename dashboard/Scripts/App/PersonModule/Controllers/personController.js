(function (module) {
    var personController = (function () {
        function personController(personService, $rootScope) {
            this.personService = personService;
            this.person = null;
            this.rootScope = $rootScope;
        }

        personController.prototype.init = function () {
            var self = this;
            self.getInfo();
        }

        personController.prototype.getInfo = function () {
            var self = this;
            self.personService.getInfo().then(function (responce) {
                var result = responce.data;
                self.person = result;
                self.rootScope.$broadcast('personInfoLoaded', true);
            })
        }

        personController.prototype.saveName = function () {
            var self = this;
            /*
            self.personService.changeName(self.person.name).then(function (responce) {
                self.getInfo();
            });
            */
        }

        return personController;
    })();

    module.controller('personController', ['personService', '$rootScope', personController]);
})(personModule);