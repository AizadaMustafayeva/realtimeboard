(function (myModule) {
    var docService = (function () {
        function docService($http) {
            this.http = $http;
        }

        docService.prototype.getMyDocs = function () {
            var self = this;
            var promice = self.http({
                url: '/doc/mydocs',
                method: 'get'
            });
            return promice;
        }

        docService.prototype.getAnotherDocs = function () {
            var self = this;
            var promice = self.http({
                url: '/doc/anotherdocs',
                method: 'get'
            });
            return promice;
        }

        docService.prototype.getDocTypes = function () {
            var self = this;
            var promice = self.http({
                url: '/doc/doctype',
                method: 'get'
            });
            return promice;
        }

        docService.prototype.create = function (title, typeId, groupId) {
            var self = this;
            var promice = self.http({
                url: '/doc/create',
                method: 'post',
                data: { title: title, typeId: typeId, groupId: groupId }
            });
            return promice;
        }

        docService.prototype.changeGroup = function (docId, groupId) {
            var self = this;
            var promice = self.http({
                url: '/doc/changeGroup',
                method: 'post',
                data: { docId: docId, groupId: groupId }
            });
            return promice;
        }

        docService.prototype.updateDoc = function (docId, val) {
            var self = this;
            var promice = self.http({
                url: '/doc/updateDoc',
                method: 'post',
                data: { docId: docId, val: val }
            });
            return promice;
        }

        docService.prototype.deleteDoc = function (docId) {
            var self = this;
            var promice = self.http({
                url: '/doc/deleteDoc',
                method: 'post',
                data: { docId: docId }
            });
            return promice;
        }

        docService.prototype.getComments = function (docId) {
            var self = this;
            var promice = self.http({
                url: '/doc/Coments/' + docId,
                method: 'get'
            });
            return promice;
        }

        docService.prototype.commentDoc = function (docId, message) {
            var self = this;
            var promice = self.http({
                url: '/doc/commentDoc',
                method: 'post',
                data: { docId: docId, message: message }
            });
            return promice;
        }

        docService.prototype.getDocVal = function (docId) {
            var self = this;
            var promice = self.http({
                url: '/doc/docval/' + docId,
                method: 'get'
            });
            return promice;
        }

        docService.prototype.userConnected = function (docId) {
            var self = this;
            var promice = self.http({
                url: '/doc/UserConnected/' + docId,
                method: 'post'
            });
            return promice;
        }

        docService.prototype.userExit = function (docId) {
            var self = this;
            var promice = self.http({
                url: '/doc/UserExit/' + docId,
                method: 'post'
            });
            return promice;
        }

        return docService;
    })();

    myModule.service('docService', ['$http', docService]);
})(docModule);