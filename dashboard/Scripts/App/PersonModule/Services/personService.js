(function (module) {
    var personService = (function () {
        function personService($http, $rootScope) {
            this.http = $http;
            this.rootScope = $rootScope;
            this.userMail = null;
        }

        personService.prototype.getInfo = function () {
            var self = this;

            var promice = self.http({
                url: '/person/UserInfo',
                method: 'get',
            });
            return promice;
        }

        personService.prototype.saveUserMail = function (mail) {
            var self = this;
            self.userMail = mail;
        }

        personService.prototype.getUsers = function () {
            var self = this;
            var promice = self.http({
                url: '/person/allusers',
                method: 'get'
            });
            return promice;
        }

        personService.prototype.changeName = function (newName) {
            var self = this;
            var promice = self.http({
                url: '/Person/ChangeName',
                method: 'post',
                data: { name: newName }
            });
            return promice;
        }

        return personService;
    })();

    module.service('personService', ['$http', '$rootScope', personService]);
})(personModule);