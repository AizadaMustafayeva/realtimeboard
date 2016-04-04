using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dashboard.Controllers
{
    using Repositories;
    using dashboard.Extensions;

    [Authorize]
    public class GroupController : Controller
    {
        private PersonRepo _personRepo;
        private GroupRepository _groupRepo;

        public GroupController() 
        {
            _personRepo = new PersonRepo();
            _groupRepo = new GroupRepository();
        }

        // GET: Group
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Groups() 
        {
            var userName = User.Identity.Name;
            var currentUser = _personRepo.GetUser(userName);

            var groups = _groupRepo.GetGroups(currentUser.Id);

            var groupsVM = groups.Convert();

            return Json(groupsVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AnotherGroups() 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;

            var groups = _groupRepo.GetAnotherGroups(userId);
            var res = groups.Convert();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddGroup(string title) 
        {
            var userName = User.Identity.Name;
            var currentUser = _personRepo.GetUser(userName);
            
            _groupRepo.AddGroup(currentUser.Id, title);
        }

        [HttpPost]
        public void DeleteGroup(int groupId) 
        {
            _groupRepo.DeleteGroup(groupId);
        }

        [HttpPost]
        public void AddUserInGroup(int groupId, string userId) 
        {
            _groupRepo.AddPersonInGroup(groupId, userId);
        }

        [HttpPost]
        public void RemoveUserInGroup(int groupId, string userId)
        {
            _groupRepo.RemovePersonInGroup(groupId, userId);
        }

        [HttpPost]
        public ActionResult GroupUsers(int groupId)
        {
            var currentUser = User.Identity.Name;

            var users = _groupRepo.GetUsersIntoGroup(groupId, currentUser);

            var res = users.Convert();

            return Json(res);
        }

        [HttpPost]
        public void LeaveGroup(int groupId) 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;
            _groupRepo.LeaveGroup(groupId, userId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
            {
                _personRepo.Dispose();
                _groupRepo.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}