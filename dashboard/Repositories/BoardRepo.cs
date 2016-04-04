using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.Repositories
{
    using Models.EF;

    public class BoardRepo : IDisposable
    {
        private localDBEntities _context;

        public BoardRepo() 
        {
            _context = new localDBEntities();
        }

        public int GetBoardId(int itemId) 
        {
            var item = _context.BoardItem.FirstOrDefault(x => x.Id == itemId).BoardId.Value;
            return item;
        }


        public List<BoardItemType> GetItemTypes() 
        {
            var res = new List<BoardItemType>();

            if (!_context.BoardItemType.Any()) 
            {
                _context.BoardItemType.Add(new BoardItemType() 
                {
                    Id = 1, Title = "Комментарии"
                });
                _context.BoardItemType.Add(new BoardItemType()
                {
                    Id = 2,
                    Title = "Стикер"
                });
                _context.BoardItemType.Add(new BoardItemType()
                {
                    Id = 3,
                    Title = "Рисунок"
                });
                _context.SaveChanges();
            }

            var data = _context.BoardItemType.ToList();

            res = data;

            return res;
        }


        public List<Board> GetMyBoards(string userId) 
        {
            var res = new List<Board>();

            var data = _context.Board.Where(x => x.OwnerId == userId).ToList();

            res = data;

            return res;
        }

        public List<Board> GetBoards(string userId) 
        {
            var res = new List<Board>();

            var data = _context.BoardGroup.Where(x => x.UserId == userId).Select(x => x.Board).Distinct().ToList();

            res = data;

            return res;
        }

        public bool CanReadBoard(string userId, int boardId) 
        {
            var res = false;

            var doc = _context.Board.FirstOrDefault(x => x.Id == boardId && x.OwnerId == userId);

            if (doc != null) 
            {
                res = true;
                return res;
            }

            var data = _context.BoardGroup.Any(x => x.BoardId == boardId && x.UserId == userId);

            res = data;

            return res;
        }


        public Board GetBoardDetail(int boardId) 
        {
            return _context.Board.First(x => x.Id == boardId);
        }

        public List<BoardItem> GetBoardItems(int boardId) 
        {
            var res = _context.BoardItem.Where(x => x.BoardId == boardId).ToList();
            return res;
        }

        public void CreateBoard(string title, string ownerId) 
        {
            var maxId = 1;

            if (_context.Board.Any()) 
            {
                maxId = _context.Board.Max(x => x.Id) + 1;
            }

            _context.Board.Add(new Board() 
            {
                Id = maxId,
                OwnerId = ownerId,
                Title = title
            });
            _context.SaveChanges();
        }

        public List<AspNetUsers> GetUsersInDoc(int boardId) 
        {
            var res = new List<AspNetUsers>();

            var users = _context.BoardGroup.Where(x => x.BoardId == boardId).Select(x => x.AspNetUsers).ToList();

            res = users;

            return res;
        }

        public void AddUser(int boardId, string userId) 
        {
            if (_context.BoardGroup.Any(x => x.BoardId == boardId && x.UserId == userId)) 
            {
                return;
            }

            var maxId = 1;
            if (_context.BoardGroup.Any()) 
            {
                maxId = _context.BoardGroup.Max(x => x.Id) + 1;
            }
            _context.BoardGroup.Add(new BoardGroup() 
            {
                Id = maxId,
                BoardId = boardId,
                UserId = userId
            });
            _context.SaveChanges();
        }

        public void RemoveUser(int boardId, string userId)
        {
            var item = _context.BoardGroup.FirstOrDefault(x => x.BoardId == boardId && x.UserId == userId);

            if (item == null) 
            {
                return;
            }

            _context.BoardGroup.Remove(item);
            _context.SaveChanges();
        }

        public void AddItem(int boardId, BoardItem item) 
        {
            var maxId = 1;

            if (_context.BoardItem.Any())
            {
                maxId = _context.BoardItem.Max(x => x.Id) + 1;
            }

            _context.BoardItem.Add(new BoardItem() 
            {
                Id = maxId,
                AuthorId = item.AuthorId,
                BoardId = boardId,
                Date = item.Date,
                PositionX = item.PositionX,
                PositionY = item.PositionY,
                Title = item.Title,
                TypeId = item.TypeId,
                Value = item.Value,
                ZIndex = item.ZIndex
            });
            _context.SaveChanges();
        }

        public void RemoveItem(int itemId) 
        {
            var item = _context.BoardItem.FirstOrDefault(x => x.Id == itemId);
            _context.BoardItem.Remove(item);
            _context.SaveChanges();
        }

        public void UpdateItem(int itemId, BoardItem item) 
        {
            var currentItem = _context.BoardItem.First(x => x.Id == itemId);
            currentItem.PositionX = item.PositionX;
            currentItem.PositionY = item.PositionY;
            currentItem.Title = item.Title;
            currentItem.Value = item.Value;
            currentItem.ZIndex = item.ZIndex;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null) 
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}