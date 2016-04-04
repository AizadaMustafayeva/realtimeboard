(function (myModule) {
    var addDocController = (function () {
        function addDocController(docService, groupService, personService, $window) {
            // services
            this.docService = docService;
            this.groupService = groupService;
            this.personService = personService;
            this.windows = $window;
            // persons
            this.allUsers = [];
            // docTypes
            this.docTypes = [];
            this.selectedDocType = null;
            // groups
            this.groups = [];
            this.selectedGroup = null;
            this.users = [];
            // doc
            this.currentDoc = {
                title: '',
            };
        }

        addDocController.prototype.init = function () {
            var self = this;
            self.getDocTypes();
            self.getGroups();
            self.getAllUsers();
        }

        addDocController.prototype.getAllUsers = function () {
            var self = this;
            self.personService.getUsers().then(function (responce) {
                self.allUsers = responce.data;
            });
        }

        addDocController.prototype.getDocTypes = function () {
            var self = this;
            self.docService.getDocTypes().then(function (responce) {
                self.docTypes = responce.data;
                self.docTypeSelected(responce.data[0]);
            });
        }

        addDocController.prototype.docTypeSelected = function (type) {
            var self = this;
            self.selectedDocType = type;
        }

        addDocController.prototype.getGroups = function () {
            var self = this;
            self.groupService.getGroups().then(function (responce) {
                self.groups = responce.data;
            });
        }

        addDocController.prototype.groupSelect = function (group) {
            var self = this;
            self.selectedGroup = group;
            self.groupService.getGroupUsers(self.selectedGroup.id).then(function (responce) {
                self.users = responce.data;
            });
        }

        addDocController.prototype.create = function () {
            var self = this;
            self.docService.create(self.currentDoc.title, self.selectedDocType.id, self.selectedGroup.id).then(function (responce) {
                // need redirect
                alert('added!');
            });
        }

        return addDocController;
    })();

    myModule.controller('addDocController', ['docService', 'groupService', 'personService', '$window', addDocController]);
})(docModule)