using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.Repositories
{
    using Models.EF;

    public class PersonRepo : IDisposable
    {
        private localDBEntities _context;

        public PersonRepo() 
        {
            _context = new localDBEntities();
        }

        public AspNetUsers GetUser(string userMail)
        {
            var res = _context.AspNetUsers.FirstOrDefault(x => x.UserName == userMail);
            return res;
        }

        public void ChangeName(string userMail, string name) 
        {
            var user = GetUser(userMail);
            user.UserName = name;
            _context.SaveChanges();
        }

        public List<AspNetUsers> GetUsers(string selfName) 
        {
            var res = new List<AspNetUsers>();

            var users = _context.AspNetUsers.Where(x => x.UserName != selfName).ToList();

            res = users;

            return res;
        }

        public void Dispose()
        {
            _context.Dispose();
            _context = null;
        }
    }
}