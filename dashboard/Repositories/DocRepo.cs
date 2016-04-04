using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.Repositories
{
    using Models.EF;

    public class DocRepo : IDisposable
    {
        private localDBEntities _context;

        public DocRepo() 
        {
            _context = new localDBEntities();
        }

        public List<Doc> GetAnotherDocs(string userId) 
        {
            var res = new List<Doc>();

            var groups = _context.GroupUsers.Where(x => x.UserId == userId).Select(x => x.Group).Where(x => x.IsDeleted == null).ToList();

            foreach(var currentGr in groups)
            {
                var docs = currentGr.Doc.ToList();
                foreach (var currentDc in docs) 
                {
                    if (!res.Any(x => x.Id == currentDc.Id) && currentDc.OwnerId != userId && currentDc.IsDeleted == null) 
                    {
                        res.Add(currentDc);
                    }
                }
            }

            return res;
        }

        public List<Doc> GetDocs(string userId) 
        {
            var data = _context.Doc.Where(x => x.OwnerId == userId && x.IsDeleted == null).ToList();
            return data;
        }

        public List<Doc> GetGroupDocs(int groupId)
        {
            var data = _context.Doc.Where(x => x.GroupId == groupId && x.IsDeleted == null).ToList();
            return data;
        }

        public void Add(string userId, string title, int type, int? groupId) 
        {
            var maxId = 1;

            if (_context.Doc.Any()) 
            {
                maxId = _context.Doc.Max(x => x.Id) + 1;
            }

            _context.Doc.Add(new Doc() 
            {
                Id = maxId,
                OwnerId = userId,
                Title = title,
                TypeId = type,
                GroupId = groupId,
                Date = DateTime.Now
            });
            _context.SaveChanges();
        }

        public void Update(int docId, string value)
        {
            var doc = _context.Doc.FirstOrDefault(x => x.Id == docId);
            doc.Value = value;
            _context.SaveChanges();
        }

        public void RemoveDoc(int docId) 
        {
            var doc = _context.Doc.FirstOrDefault(x => x.Id == docId);
            doc.IsDeleted = 1;
            _context.SaveChanges();
        }

        public void ChangeGroup(int docId, int groupId) 
        {
            var doc = _context.Doc.FirstOrDefault(x => x.Id == docId);
            doc.GroupId = groupId;
            _context.SaveChanges();
        }

        public List<DocComment> GetComments(int docId) 
        {
            var res = _context.DocComment.Where(x => x.DocId == docId).ToList();
            return res;
        }

        public void CommentDoc(int docId, string message, string authorId) 
        {
            var maxId = 1;

            if (_context.DocComment.Any()) 
            {
                maxId = _context.DocComment.Max(x => x.Id) + 1;
            }

            _context.DocComment.Add(new DocComment() 
            {
                Id = maxId,
                DocId = docId,
                Date = DateTime.Now,
                CommentOwner = authorId,
                Message = message
            });
            _context.SaveChanges();
        }

        public Doc GetCurrentDoc(int docId, string userId) 
        {
            var user = _context.AspNetUsers.First(x => x.Id == userId);
            var doc = _context.Doc.FirstOrDefault(x => x.Id == docId);
            if (doc.OwnerId == userId) 
            {
                return doc;
            }
            var users = doc.Group.GroupUsers.Any(x => x.UserId == userId);
            if (!users) 
            {
                throw new Exception("У вас нет доступа к данному документу");
            }
            return doc;
        }

        public void Dispose()
        {
            _context.Dispose();
            _context = null;
        }
    }
}