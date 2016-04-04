(function (myModule) {
    var boardListController = (function () {
        function controller(boardService, personService) {
            this.boardService = boardService;
            this.personService = personService;
            this.myBoards = new Array();
            this.anotherBoards = new Array();
            this.myBoardSeleted = null;
            this.anotherBoardSeleted = null;
            this.allUsers = new Array();
            this.myBoardUsers = new Array();
            this.anotherBoardUsers = new Array();
            // properties
            this.newBoardTitle = "";
        }

        controller.prototype.init = function () {
            var self = this;
            self.getBoards();
            self.getAllUsers();
        }

        controller.prototype.getAllUsers = function () {
            var self = this;
            self.personService.getUsers().then(function (responce) {
                var data = responce.data;
                self.allUsers = data;
            });
        }

        controller.prototype.getBoards = function () {
            var self = this;
            self.boardService.getMyBoards().then(function (responce) {
                var data = responce.data;
                self.myBoards = data;
                self.myBoardSeleted = null;
                self.myBoardUsers = new Array();
            });
            self.boardService.getAnotherBoards().then(function (responce) {
                var data = responce.data;
                self.anotherBoards = data;
                self.anotherBoardSeleted = null;
                self.anotherBoardUsers = new Array();
            });
        }

        controller.prototype.createBoard = function () {
            var self = this;

            if (self.newBoardTitle == "") {
                return;
            }

            self.boardService.createBoard(self.newBoardTitle).then(function (responce) {
                self.newBoardTitle = "";
                self.getBoards();
            });
        }

        controller.prototype.myBoardSelect = function (item) {
            var self = this;
            self.myBoardSeleted = item;
            self.getUsersFromMyBoard(item.id);
        }

        controller.prototype.getUsersFromMyBoard = function (boardId) {
            var self = this;
            self.boardService.getUserInBoard(boardId).then(function (responce) {
                var data = responce.data;
                self.myBoardUsers = data;
            });
        }

        controller.prototype.getUsersFromAnotherBoard = function (boardId) {
            var self = this;
            self.boardService.getUserInBoard(boardId).then(function (responce) {
                var data = responce.data;
                self.anotherBoardUsers = data;
            });
        }

        controller.prototype.addUser = function (user) {
            var self = this;
            self.boardService.addUser(self.myBoardSeleted.id, user.id).then(function (responce) {
                self.myBoardSelect(self.myBoardSeleted);
            });
        }

        controller.prototype.removeUser = function (user) {
            var self = this;
            self.boardService.removeUser(self.myBoardSeleted.id, user.id).then(function (responce) {
                self.myBoardSelect(self.myBoardSeleted);
            });
        }

        controller.prototype.anotherBoardSelect = function (item) {
            var self = this;
            self.anotherBoardSeleted = item;
            self.getUsersFromAnotherBoard(item.id);
        }

        return controller;
    })();

    myModule.controller('boardListController', ['boardService', 'personService', boardListController]);
})(boardModule)