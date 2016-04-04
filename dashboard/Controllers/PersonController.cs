using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dashboard.Controllers
{
    using Models.EF;
    using Repositories;
    using dashboard.Extensions;

    [Authorize]
    public class PersonController : Controller
    {
        private PersonRepo _repo;

        public PersonController() 
        {
            _repo = new PersonRepo();
        }

        // GET: Person
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserInfo()
        {
            var userName = User.Identity.Name;
            var userInfo = _repo.GetUser(userName);

            var vm = userInfo.Convert();

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ChangeName(string name) 
        {
            var userName = User.Identity.Name;
            _repo.ChangeName(userName, name);
        }

        [HttpGet]
        public ActionResult AllUsers() 
        {
            var selfName = User.Identity.Name;
            var data = _repo.GetUsers(selfName);
            var res = data.Convert();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
            {
                _repo.Dispose();
                _repo = null;
            }

            base.Dispose(disposing);
        }

    }
}