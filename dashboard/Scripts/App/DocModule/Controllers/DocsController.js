(function (myModule) {
    var docsController = (function () {
        function docsController(docService, groupService, personService) {
            //services
            this.docService = docService;
            this.groupService = groupService;
            this.personService = personService;
            // docs
            this.docs = [];
            this.anotherDocs = [];
            this.docSelected = null;
            this.myDoc = false;
            // group  
            this.currentGroup = null;
            this.allUsers = [];
            this.groupUsers = [];
        }

        docsController.prototype.init = function () {
            var self = this;
            self.getAllUser();
            self.getDocs();
            self.getAnotherDocs();
        }
        
        docsController.prototype.getDocs = function () {
            var self = this;
            self.docService.getMyDocs().then(function (responce) {
                self.docs = responce.data;
            });
        }

        docsController.prototype.selectDoc = function (doc) {
            var self = this;
            self.docSelected = doc;
            self.myDoc = true;
            if (doc != null) {
                // load group info
                self.getUsersInGroup();
            }
            
        }

        docsController.prototype.getAnotherDocs = function () {
            var self = this;
            self.docService.getAnotherDocs().then(function (responce) {
                self.anotherDocs = responce.data;
            });
        }

        docsController.prototype.selectAnotherDoc = function (doc) {
            var self = this;
            self.docSelected = doc;
            self.myDoc = false;
            if (doc != null) {
                // load group info
                self.getUsersInGroup();
            }
        }

        docsController.prototype.getAllUser = function () {
            var self = this;
            console.log(1);
            self.personService.getUsers().then(function (responce) {
                self.allUsers = responce.data;
                console.log(self.allUsers);
            });
        }

        docsController.prototype.getUsersInGroup = function () {
            var self = this;
            self.groupService.getGroupUsers(self.docSelected.group.id).then(function (responce) {
                self.groupUsers = responce.data;
            });
        }

        docsController.prototype.addPersonInGroup = function (person) {
            var self = this;
            self.groupService.addUserInGroup(self.docSelected.group.id, person.id).then(function (responce) {
                self.getUsersInGroup();
            });
        }

        docsController.prototype.removePersonInGroup = function (person) {
            var self = this;
            self.groupService.removeUserInGroup(self.docSelected.group.id, person.id).then(function (responce) {
                self.getUsersInGroup();
            });
        }

        return docsController;
    })();

    myModule.controller('docsController', ['docService', 'groupService', 'personService', docsController]);
})(docModule);