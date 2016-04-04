(function (myModule) {

    var groupController = (function () {
        function groupController(groupService, personService) {
            this.groupService = groupService;
            this.personService = personService;
            this.groups = [];
            this.groupSelected = null;
            this.title = "";
            this.personSearchName = "";
            // persons
            this.allPersons = [];
            this.groupUsers = [];
            // second groups
            this.secondGroups = [];
            this.secondGroupSelected = null;
            this.secondGroupUsers = [];
        }

        groupController.prototype.loadAnotherGroups = function () {
            var self = this;
            self.groupService.getAnotherGroups().then(function (responce) {
                self.secondGroups = responce.data;
            });
        }

        groupController.prototype.selectSecondGroup = function (person) {
            var self = this;
            this.secondGroupSelected = person;
            self.groupService.getGroupUsers(self.secondGroupSelected.id).then(function (responce) {
                self.secondGroupUsers = responce.data;
            });
        }

        groupController.prototype.leaveAnotherGroup = function (group) {
            var self = this;
            self.groupService.leaveGroup(group.id).then(function (responce) {
                self.loadAnotherGroups();
            });
        }

        groupController.prototype.getGroupSecondUsers = function () {

        }

        groupController.prototype.init = function () {
            var self = this;
            self.getGroups();
            self.getUsersAll();
        }

        groupController.prototype.getGroups = function () {
            var self = this;
            self.groupService.getGroups().then(function (responce) {
                self.groups = responce.data;
            });

        }

        groupController.prototype.addIntoGroup = function (person) {
            var self = this;
            self.groupService.addUserInGroup(self.groupSelected.id, person.id).then(function (responce) {
                self.getUsersInGroup();
            });
            // add logic adding into group
        }

        groupController.prototype.removeIntoGroup = function (person) {
            var self = this;
            self.groupService.removeUserInGroup(self.groupSelected.id, person.id).then(function (responce) {
                self.getUsersInGroup();
            });
        }

        groupController.prototype.getUsersInGroup = function () {
            var self = this;
            self.groupService.getGroupUsers(self.groupSelected.id).then(function (responce) {
                self.groupUsers = responce.data;
            });
        }

        groupController.prototype.getUsersAll = function () {
            var self = this;
            
            self.personService.getUsers().then(function (responce) {
                self.allPersons = responce.data;
            });
        }

        groupController.prototype.addGroup = function () {
            var self = this;
            self.groupService.addGroup(self.title).then(function (responce) {
                self.title = "";
                self.groupSelected = null;
                self.getGroups();
            });
        }

        groupController.prototype.removeGroup = function () {
            var self = this;
            self.groupService.deleteGroup(self.groupSelected.id).then(function () {
                self.selectGroup(null);
                self.getGroups();
            });

            // delete selectGroup
        }

        groupController.prototype.selectGroup = function (item) {
            var self = this;
            self.groupSelected = item;
            if (item != null) {
                self.getUsersInGroup();
                // download persons in group
            }
        }

        return groupController;
    })();

    myModule.controller("groupController", ['groupService', 'personService', groupController])
})(groupModule);