using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.Repositories
{
    using Models.EF;

    public class DocTypeRepo : IDisposable
    {
        private localDBEntities _context;

        public DocTypeRepo() 
        {
            _context = new localDBEntities();
        }

        public List<DocType> GetDocTypes() 
        {
            var res = _context.DocType.ToList();
            return res;
        }

        public void AddType(string title) 
        {
            var maxId = 1;

            if (_context.DocType.Any()) 
            {
                maxId = _context.DocType.Max(x => x.Id) + 1;
            }

            _context.DocType.Add(new DocType() 
            {
                Id = maxId,
                Title = title
            });
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            _context = null;
        }
    }
}