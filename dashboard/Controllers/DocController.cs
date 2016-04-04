using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dashboard.Controllers
{
    using Microsoft.AspNet.SignalR;
    using Repositories;
    using Extensions;
    using Models.EF;

    [Authorize]
    public class DocController : Controller
    {
        private PersonRepo _personRepo;
        private DocRepo _docRepo;
        private DocTypeRepo _typeRepo;

        public DocController() 
        {
            _personRepo = new PersonRepo();
            _docRepo = new DocRepo();
            _typeRepo = new DocTypeRepo();
        }

        // GET: Doc
        public ActionResult Index()
        {
            return View();
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: Doc
        public ActionResult Doc(int id) 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;

            // here get doc

            var doc = _docRepo.GetCurrentDoc(id, userId);

            var res = doc.Convert();

            ViewBag.docId = res.id;
            return View();
        }

        [HttpGet]
        public ActionResult DocVal(int id) 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;

            // here get doc

            var doc = _docRepo.GetCurrentDoc(id, userId);

            var res = doc.Convert();

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult MyDocs() 
        {
            var user = User.Identity.Name;

            var userId = _personRepo.GetUser(user).Id;

            var docs = _docRepo.GetDocs(userId);

            var res = docs.Convert();

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AnotherDocs() 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;

            var docs = _docRepo.GetAnotherDocs(userId);
            var res = docs.Convert();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DocType() 
        {
            var types = _typeRepo.GetDocTypes();
            var res = types.Convert();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Create(string title, int typeId, int? groupId) 
        {
            // logic adding
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;

            _docRepo.Add(userId, title, typeId, groupId);
        }

        [HttpPost]
        public void ChangeGroup(int docId, int grouId) 
        {
            _docRepo.ChangeGroup(docId, grouId);
        }

        [HttpPost]
        public void UpdateDoc(int docId, string val) 
        {
            _docRepo.Update(docId, val);

            var hub = GlobalHost.ConnectionManager.GetHubContext<Hubs.DocHub>();
            if (hub != null)
            {
                hub.Clients.Group(docId.ToString()).DocUpdated();
            }
        }

        [HttpPost]
        public void DeleteDoc(int docId)
        {
            _docRepo.RemoveDoc(docId);
        }

        [HttpGet]
        public ActionResult Coments(int id) 
        {
            var userName = User.Identity.Name;

            var comments = _docRepo.GetComments(id);
            var res = comments.Convert(userName);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void CommentDoc(int docId, string message) 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;

            _docRepo.CommentDoc(docId, message, userId);

            var hub = GlobalHost.ConnectionManager.GetHubContext<Hubs.DocHub>();
            if (hub != null)
            {
                hub.Clients.Group(docId.ToString()).DocCommented();
            }
        }

        [HttpPost]
        public void UserConnected(int id) 
        {
            var userName = User.Identity.Name;
            var user = _personRepo.GetUser(userName);
            var res = user.Convert();

            var hub = GlobalHost.ConnectionManager.GetHubContext<Hubs.DocHub>();
            if (hub != null) 
            {
                hub.Clients.Group(id.ToString()).UserConnected(res);
            }
        }

        [HttpPost]
        public void UserExit(int id)
        {
            var userName = User.Identity.Name;
            var user = _personRepo.GetUser(userName);
            var res = user.Convert();

            var hub = GlobalHost.ConnectionManager.GetHubContext<Hubs.DocHub>();
            if (hub != null)
            {
                hub.Clients.Group(id.ToString()).UserExit(res);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
            {
                _docRepo.Dispose();
                _docRepo = null;
                _typeRepo.Dispose();
                _typeRepo = null;
            }
            base.Dispose(disposing);
        }

    }
}