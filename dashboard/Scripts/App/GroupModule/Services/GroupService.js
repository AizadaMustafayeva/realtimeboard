(function (myModule) {
    var groupService = (function () {
        function groupService($http) {
            this.http = $http;
        }

        //[HttpGet] Groups() 
        groupService.prototype.getGroups = function () {
            var self = this;
            var promice = self.http({
                url: '/group/groups',
                method: 'get'
            });
            return promice;
        }

        groupService.prototype.getAnotherGroups = function () {
            var self = this;
            var promice = self.http({
                url: '/group/AnotherGroups',
                method: 'get'
            });
            return promice;
        }

        groupService.prototype.getGroupUsers = function (groupId) {
            var self = this;
            var promice = self.http({
                url: '/group/groupUsers',
                method: 'post',
                data: { groupId: groupId }
            });
            return promice;
        }

        // post AddGroup(string title)
        groupService.prototype.addGroup = function (title) {
            var self = this;
            var promice = self.http({
                url: '/group/addGroup',
                method: 'post',
                data: { title:  title}
            });
            return promice;
        }

        //DeleteGroup(int groupId)
        groupService.prototype.deleteGroup = function (groupId) {
            var self = this;
            var promice = self.http({
                url: '/group/DeleteGroup',
                method: 'post',
                data: { groupId: groupId }
            });
            return promice;
        }

        // post AddUserInGroup(int groupId, string userId)
        groupService.prototype.addUserInGroup = function (groupId, userId) {
            var self = this;
            var promice = self.http({
                url: '/group/AddUserInGroup',
                method: 'post',
                data: { groupId : groupId, userId: userId }
            });
            return promice;
        }

        // post RemoveUserInGroup(int groupId, string userId)
        groupService.prototype.removeUserInGroup = function (groupId, userId) {
            var self = this;
            var promice = self.http({
                url: '/group/RemoveUserInGroup',
                method: 'post',
                data: { groupId: groupId, userId: userId }
            });
            return promice;
        }

        groupService.prototype.leaveGroup = function (groupId) {
            var self = this;
            var promice = self.http({
                url: '/group/leavegroup',
                method: 'post',
                data: { groupId: groupId }
            });
            return promice;
        }

        return groupService;
    })();

    myModule.service('groupService', ['$http', groupService]);
})(groupModule);