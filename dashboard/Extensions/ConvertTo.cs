using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.Extensions
{
    using ViewModels;
    using Models.EF;

    public static class ConvertTo
    {
        public static PersonVM Convert(this AspNetUsers user) 
        {
            return new PersonVM() 
            {
                id = user.Id,
                name = user.UserName,
                mail = user.Email
            };
        }

        public static List<PersonVM> Convert(this List<AspNetUsers> users)
        {
            
            if (users == null)
            {
                return null;
            }

            if (!users.Any())
            {
                return null;
            }

            var res = new List<PersonVM>();

            foreach(var item in users)
            {
                res.Add(item.Convert());
            }

            return res;
        }

        public static GroupVM Convert(this Group group) 
        {
            var users = group.GroupUsers.Select(x => x.AspNetUsers).ToList();

            return new GroupVM() 
            {
                id = group.Id,
                title = group.Title,
                Users = users.Convert(),
                Owner = group.AspNetUsers.Convert()
            };

            //group.Id
            //group.AspNetUsers
            //group.GroupDocs
            //group.GroupUsers
            //group.Title
        }

        public static List<GroupVM> Convert(this List<Group> groups) 
        {
            if (groups == null)
                return null;

            if (!groups.Any())
                return null;

            var res = new List<GroupVM>();

            foreach(var item in groups)
            {
                res.Add(item.Convert());
            }

            return res;
        }

        public static DocTypeVM Convert(this DocType type) 
        {
            return new DocTypeVM()
            {
                id = type.Id,
                title = type.Title
            };
        }

        public static List<DocTypeVM> Convert(this List<DocType> types) 
        {
            var res = new List<DocTypeVM>();

            foreach(var item in types)
            {
                res.Add(item.Convert());
            }

            return res;
        }

        public static DocVM Convert(this Doc doc) 
        {
            return new DocVM() 
            {
                id = doc.Id,
                title = doc.Title,
                date = doc.Date.ToShortDateString(),
                owner = doc.AspNetUsers.Convert(),
                group = doc.Group.Convert(),
                value = doc.Value,
                type = doc.DocType.Convert()
            };
        }

        public static List<DocVM> Convert(this List<Doc> docs)
        {
            var res = new List<DocVM>();

            foreach(var item in docs)
            {
                res.Add(item.Convert());
            }

            return res;
        }

        public static DocCommentVM Convert(this DocComment comment, string ownerName) 
        {
            return new DocCommentVM() 
            {
                id = comment.Id, 
                date = comment.Date.ToShortDateString(),
                time = comment.Date.ToShortTimeString(),
                message = comment.Message,
                owner = comment.AspNetUsers.Convert(),
                self = comment.AspNetUsers.UserName == ownerName ? true : false
            };
        }

        public static List<DocCommentVM> Convert(this List<DocComment> comments, string ownerName)
        {
            var res = new List<DocCommentVM>();

            foreach(var item in comments)
            {
                res.Add(item.Convert(ownerName));
            }

            return res;
        }

        public static BoardTypeVM Convert(this BoardItemType item) 
        {
            return new BoardTypeVM() 
            {
                id = item.Id,
                title = item.Title
            };
        }

        public static List<BoardTypeVM> Convert(this List<BoardItemType> items) 
        {
            var res = new List<BoardTypeVM>();

            foreach (var item in items) 
            {
                res.Add(item.Convert());
            }

            return res;
        }

        public static BoardVM Convert(this Board item) 
        {
            return new BoardVM() 
            {
                id = item.Id,
                title = item.Title,
                owner = item.AspNetUsers.Convert()
            };
        }

        public static List<BoardVM> Convert(this List<Board> collection) 
        {
            var res = new List<BoardVM>();

            foreach (var item in collection) 
            {
                res.Add(item.Convert());
            }

            return res;
        }

        public static BoardItemVM Convert(this BoardItem item) 
        {
            return new BoardItemVM() 
            {
                id = item.Id,
                date = item.Date.HasValue ? item.Date.Value.ToShortDateString() : "-",
                owner = item.AspNetUsers.Convert(),
                positionX = item.PositionX.Value,
                positionY = item.PositionY.Value,
                title = item.Title,
                value = item.Value,
                zIndex = item.ZIndex.Value,
                typeId = item.TypeId.Value
            };
        }

        public static List<BoardItemVM> Convert(this List<BoardItem> collection) 
        {
            var res = new List<BoardItemVM>();

            foreach (var item in collection) 
            {
                res.Add(item.Convert());
            }

            return res;
        }

    }
}