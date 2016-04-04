using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dashboard.Controllers
{
    using Extensions;
    using Microsoft.AspNet.SignalR;
    using Repositories;

    [Authorize]
    public class BoardController : Controller
    {
        private PersonRepo _personRepo;
        private BoardRepo _boardRepo;

        public BoardController() 
        {
            _personRepo = new PersonRepo();
            _boardRepo = new BoardRepo();
        }

        // GET: Board
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Board(int id) 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;

            var res = _boardRepo.CanReadBoard(userId, id);

            var board = _boardRepo.GetBoardDetail(id);

            if (res == false) 
            {
                throw new Exception("Нет прав на документ");
            }

            ViewBag.Title = board.Title;
            ViewBag.boardId = id;
            return View();
        }

        [HttpPost]
        public ActionResult GetMyBoards() 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;

            var data = _boardRepo.GetMyBoards(userId);
            var res = data.Convert();

            return Json(res);
        }

        [HttpPost]
        public ActionResult GetAnotherBoards() 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;

            var data = _boardRepo.GetBoards(userId);
            var res = data.Convert();

            return Json(res);

        }

        [HttpPost]
        public void CreateBoard(string title) 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;
            _boardRepo.CreateBoard(title, userId);
        }

        [HttpGet]
        public ActionResult GetUsersInBoard(int id) 
        {
            var res = _boardRepo.GetUsersInDoc(id);
            var resVM = res.Convert();
            return Json(resVM, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetBoardDetail(int id)
        {
            var board = _boardRepo.GetBoardDetail(id);
            var res = board.Convert();
            return Json(res);
        }
        [HttpPost]
        public ActionResult GetBoardItems(int id)
        {
            var item = _boardRepo.GetBoardItems(id);
            var res = item.Convert();
            return Json(res);
        }

        [HttpPost]
        public void AddUser(int boardId, string userId) 
        {
            _boardRepo.AddUser(boardId, userId);
        }
        [HttpPost]
        public void RemoveUser(int boardId, string userId) 
        {
            _boardRepo.RemoveUser(boardId, userId);
        }
        [HttpPost]
        public void AddItem(int boardId, double posX, double posY, string title, int typeId, string value, int zIndex) 
        {
            var userName = User.Identity.Name;
            var userId = _personRepo.GetUser(userName).Id;
            _boardRepo.AddItem(boardId, new Models.EF.BoardItem() 
            {
                Date = DateTime.Now,
                AuthorId = userId,
                PositionX = posX,
                PositionY = posY,
                Title = title,
                TypeId = typeId,
                Value = value,
                ZIndex = zIndex
            });


            var hub = GlobalHost.ConnectionManager.GetHubContext<Hubs.BoardHub>();
            if (hub != null)
            {
                hub.Clients.Group(boardId.ToString()).Updated();
            }
        }
        [HttpPost]
        public void RemoveItem(int id, int boardId) 
        {
            _boardRepo.RemoveItem(id);

            var hub = GlobalHost.ConnectionManager.GetHubContext<Hubs.BoardHub>();
            if (hub != null)
            {
                hub.Clients.Group(boardId.ToString()).Updated();
            }
        }
        [HttpPost]
        public void UpdateItem(int itemId, double posX, double posY, string title, string value, int zIndex) 
        {
            _boardRepo.UpdateItem(itemId, new Models.EF.BoardItem()
            {
                PositionX = posX,
                PositionY = posY,
                Title = title,
                Value = value,
                ZIndex = zIndex
            });

            var boardId = _boardRepo.GetBoardId(itemId);

            var hub = GlobalHost.ConnectionManager.GetHubContext<Hubs.BoardHub>();
            if (hub != null)
            {
                hub.Clients.Group(boardId.ToString()).Updated();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
            {
                _personRepo.Dispose();
                _personRepo = null;
                _boardRepo.Dispose();
                _boardRepo = null;
            }

            base.Dispose(disposing);
        }

    }
}