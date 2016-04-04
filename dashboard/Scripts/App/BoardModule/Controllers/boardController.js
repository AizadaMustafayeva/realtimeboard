var boardHubProxy = $.connection.boardHub;

(function (myModule) {
    var boardController = (function () {
        function controller($scope, boardService) {
            this.$scope = $scope;
            this.boardService = boardService;
            this.boardId = null;
            this.stickers = new Array();
            this.comments = new Array();
            this.maxZIndex = 1;
            var self = this;
            this.$scope.$on('updateItem', function (event, data) {
                self.submitItem(data);
            });
        }

        controller.prototype.init = function () {
            var self = this;
            self.boardId = boardId;

            boardHubProxy.client.Updated = function () {
                self.getItems();
            }

            $.connection.hub.start().done(function () {
                console.log('connection done');
                console.log(boardHubProxy);
                boardHubProxy.server.register(self.boardId);
                self.getItems();
            });
        }

        controller.prototype.getzIndexPanel = function () {
            return this.maxZIndex + 1;
        }

        controller.prototype.getItems = function () {
            var self = this;
            self.boardService.getBoardItems(self.boardId).then(function (responce) {
                var items = responce.data;
                //self.parseItems(data);
                self.comments = new Array();
                self.stickers = new Array();
                for (var i = 0; i < items.length; i++) {
                    var currentItem = items[i];
                    if (currentItem.zIndex > self.maxZIndex) {
                        self.maxZIndex = currentItem.zIndex;
                    }

                    if (currentItem.typeId == 1) {
                        self.comments.push(currentItem);
                        console.log(self.comments);
                    }
                    else {
                        self.stickers.push(currentItem);
                        console.log(self.stickers);
                    }
                }
            });
        }

        controller.prototype.parseItems = function (items) {
            var self = this;
            for (var i = 0; i < items.length; i++) {
                var currentItem = items[i];
                if (currentItem.zIndex > self.maxZIndex) {
                    self.maxZIndex = currentItem.zIndex;
                }

                if (currentItem.typeId == 1) {
                    self.comments.push(currentItem);
                    console.log(self.comments);
                }
                else {
                    self.stickers.push(currentItem);
                    console.log(self.stickers);
                }
            }
        }

        controller.prototype.submit = function () {
            var self = this;
        }

        controller.prototype.newStick = function () {
            var self = this;
            self.boardService.addItem(self.boardId, 400, 200, "new sticker", 2, "text...", self.getZIndex());
        }

        controller.prototype.newComment = function () {
            var self = this;
            self.boardService.addItem(self.boardId, 200, 200, "", 1, "My comment", self.getZIndex()).then(function (responce) {
                self.getItems();
            });
        }

        controller.prototype.removeItem = function (item) {
            var self = this;
            console.log(self.boardId);
            self.boardService.removeItem(item.id, self.boardId).then(function (responce) {
                self.getItems();
            });
        }

        controller.prototype.submitItem = function (item) {
            var self = this;
            self.boardService.updateItem(item.id, item.positionX, item.positionY, item.title, item.value, item.zIndex).then(function (responce) {
                self.getItems();
            });
        }

        controller.prototype.getZIndex = function() {
            this.maxZIndex++;
            return this.maxZIndex;
        }

        return controller;
    })();

    myModule.controller('boardController', ['$scope', 'boardService', boardController]);
})(boardModule)