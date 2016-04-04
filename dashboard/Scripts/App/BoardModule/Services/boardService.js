(function (myModule) {
    var boardService = (function () {
        function boardService($http) {
            this.$http = $http;
        }

        boardService.prototype.init = function () {

        }

        boardService.prototype.getMyBoards = function () {
            var promose = this.$http({
                url: '/board/getmyboards',
                method: 'post'
            });
            return promose;
        }

        boardService.prototype.getAnotherBoards = function () {
            var promose = this.$http({
                url: '/board/getAnotherBoards',
                method: 'post'
            });
            return promose;
        }

        // void
        boardService.prototype.createBoard = function (title) {
            var promice = this.$http({
                url: '/board/createboard',
                method: 'post',
                data: { title: title }
            });
            return promice;
        }

        // list of persons
        boardService.prototype.getUserInBoard = function (boardId) {
            var promice = this.$http({
                url: '/board/GetUsersInBoard/' + boardId,
                method: 'get',
            });
            return promice;
        }

        boardService.prototype.getBoardDetail = function (boardId) {
            var promice = this.$http({
                url: '/board/getboarddetail',
                method: 'post',
                data: { id: boardId }
            });
            return promice;
        }

        // list of items
        boardService.prototype.getBoardItems = function (boardId) {
            var promice = this.$http({
                url: '/board/getboarditems',
                method: 'post',
                data: { id: boardId }
            });
            return promice;
        }

        boardService.prototype.addUser = function (boardId, userId) {
            var promice = this.$http({
                url: '/board/adduser',
                method: 'post',
                data: { boardId: boardId, userId: userId }
            });
            return promice;
        }

        boardService.prototype.removeUser = function (boardId, userId) {
            var promice = this.$http({
                url: '/board/removeUser',
                method: 'post',
                data: { boardId: boardId, userId: userId }
            });
            return promice;
        }

        boardService.prototype.addItem = function (boardId, posX, posY, title, typeId, value, zIndex) {
            var promice = this.$http({
                url: '/board/addItem',
                method: 'post',
                data: {
                    boardId: boardId,
                    posX: posX,
                    posY: posY,
                    title: title,
                    typeId: typeId,
                    value: value,
                    zIndex: zIndex
                }
            });
            return promice;
        }

        boardService.prototype.removeItem = function (itemId, boardId) {
            var promice = this.$http({
                url: '/board/removeitem',
                method: 'post',
                data: { id: itemId, boardId: boardId }
            });
            return promice;
        }

        boardService.prototype.updateItem = function (itemId, posX, posY, title, value, zIndex) {
            var promice = this.$http({
                url: '/board/updateItem',
                method: 'post',
                data: {
                    itemId: itemId,
                    posX: posX,
                    posY: posY,
                    title: title,
                    value: value,
                    zIndex: zIndex
                }
            });
            return promice;
        }

        return boardService;
    })();

    myModule.service('boardService', ['$http', boardService]);
})(boardModule);