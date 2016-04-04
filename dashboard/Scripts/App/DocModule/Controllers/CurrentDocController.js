var docHubProxy = $.connection.docHub;
(function (myModule) {
    
    
    
    var currentDocController = (function () {
        function currentDocController($scope, docService, $window) {
            this.scope = $scope;
            this.service = docService;
            this.window = $window;
            this.users = [];
            this.comments = [];
            this.currentDoc = "";
            this.commentText = "";
            this.title = "";
            this.docId = 0;
        }

        currentDocController.prototype.init = function () {
            var self = this;
            self.window.onbeforeunload = self.exit;
            
            self.docId = docId;

            docHubProxy.client.DocCommented = function () {
                console.log('comm');
                self.getComments();
            };

            docHubProxy.client.userConnected = function (user) {
                var items = self.users;
                
                var res = new Array();

                for (var i = 0; i < items.length; i++) {
                    res.push(items[i]);
                }

                res.push(user);

                self.users = res;
            };

            docHubProxy.client.userExit = function (user) {
                console.log('logout some one');
                var res = new Array();
                var items = self.users;
                for (var i = 0; i < items.length; i++) {
                    if (items[i].id == user.id) {
                        continue;
                    }
                    res.push(items[i]);
                }
                self.users = res;
            };

            docHubProxy.client.docUpdated = function () {
                self.getDoc(self.docId);
                
            };

            $.connection.hub.start().done(function () {
                console.log('connection done');
                console.log(docHubProxy);
                docHubProxy.server.register(self.docId);
                
                

                self.service.userConnected(self.docId).then(function () {
                    self.getDoc(self.docId);
                });
            });

            

        }

        currentDocController.prototype.getDoc = function (docId) {
            var self = this;
            self.service.getDocVal(self.docId).then(function (responce) {
                self.currentDoc = responce.data.value;
                self.title = responce.data.title;
                self.getComments();
            });
        }

        currentDocController.prototype.userConnected = function (user) {
            var self = this;
            
        }

        currentDocController.prototype.userGone = function (user) {
            var self = this;

        }

        currentDocController.prototype.submit = function () {
            var self = this;
            self.service.updateDoc(self.docId, self.currentDoc).then(function (responce) {

            });
        }

        // method for send to server that you will close form
        currentDocController.prototype.exit = function () {
            var self = this;
            console.log('hererererererer!!!!!!');
            self.service.userExit(self.docId).then(function () {

            });
        }

        currentDocController.prototype.getComments = function () {
            var self = this;
            self.service.getComments(self.docId).then(function (responce) {
                self.comments = responce.data;
            });
        }

        currentDocController.prototype.setCommit = function () {
            var self = this;
            self.service.commentDoc(self.docId, self.commentText).then(function(responce){
                self.commentText = "";
                //self.getComments();
            });

        }

        return currentDocController;
    })();

    myModule.controller('currentDocController', ['$scope', 'docService', '$window', currentDocController]);
})(docModule)