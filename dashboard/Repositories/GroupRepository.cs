using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.Repositories
{
    using Models.EF;

    public class GroupRepository : IDisposable
    {
        private localDBEntities _context;

        public GroupRepository() 
        {
            _context = new localDBEntities();
        }

        public List<Group> GetAnotherGroups(string userId) 
        {
            var res = new List<Group>();

            var groups = _context.GroupUsers.Where(x => x.UserId == userId).Select(x => x.Group).Where(x => x.IsDeleted == null).ToList();

            foreach (var currentGr in groups) 
            {
                if (!res.Any(x => x.Id == currentGr.Id)&& currentGr.OwnerId != userId) 
                {
                    res.Add(currentGr);
                }
            }

            return res;
        }

        public List<Group> GetGroups(string userId) 
        {
            var res = new List<Group>();

            var groups = _context.Group.Include("AspNetUsers").Include("GroupUsers").Where(x => x.OwnerId == userId && !x.IsDeleted.HasValue).ToList();

            res = groups;

            return res;
        }

        public int AddGroup(string ownerId, string title)
        {
            var maxId = GetMaxId();

            _context.Group.Add(new Group() 
            {
                OwnerId = ownerId,
                Title = title,
                Id = maxId,
            });

            _context.SaveChanges();

            return maxId;
        }

        public void AddPersonInGroup(int groupId, string personId) 
        {
            var item = _context.Group.FirstOrDefault(x => x.Id == groupId);

            var maxId = 1;

            if (_context.GroupUsers.Any()) 
            {
                maxId = _context.GroupUsers.Max(x => x.Id) + 1;
            }

            if (item.GroupUsers.Any(x => x.UserId == personId)) 
            {
                return;
            }

            item.GroupUsers.Add(new GroupUsers() 
            {
                UserId = personId,
                GroupId = groupId,
                Id = maxId
            });

            _context.SaveChanges();
        }

        public void RemovePersonInGroup(int groupId, string personId) 
        {
            var group = _context.GroupUsers.Where(x => x.GroupId == groupId && x.UserId == personId);

            foreach(var item in group)
            {
                _context.GroupUsers.Remove(item);
            }
            _context.SaveChanges();
        }

        public void DeleteGroup(int groupId) 
        {
            var item = _context.Group.FirstOrDefault(x => x.Id == groupId);
            item.IsDeleted = 1;
            _context.SaveChanges();
        }

        public List<AspNetUsers> GetUsersIntoGroup(int groupId, string currentUserName)
        {
            var res = new List<AspNetUsers>();

            var group = _context.Group.Include("GroupUsers").Include("AspNetUsers").FirstOrDefault(x => x.Id == groupId);

            var users = group.GroupUsers.Select(x => x.AspNetUsers).ToList();

            foreach (var item in users)
            {
                if (item.UserName == currentUserName)
                    continue;

                res.Add(item);
            }

            return res;
        }

        public void LeaveGroup(int groupId, string userId) 
        {
            var group = _context.Group.FirstOrDefault(x => x.Id == groupId);

            var position = group.GroupUsers.FirstOrDefault(x => x.UserId == userId);

            _context.GroupUsers.Remove(position);
            _context.SaveChanges();
        }

        private int GetMaxId()
        {
            var maxId = 1;

            if(_context.Group.Any())
            {
                maxId = _context.Group.Max(x => x.Id) + 1;
            }

            return maxId;
        }

        public void Dispose()
        {
            _context.Dispose();
            _context = null;
        }
    }
}