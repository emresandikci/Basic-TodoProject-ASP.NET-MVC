using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListProject.Domain;
using TodoListProject.Models;

namespace TodoListProject.Repositories
{
    public class FriendRepo : IRepository<Friend>
    {
        private readonly ApplicationDbContext _db;

        public FriendRepo()
        {
            _db = Tools.TodoSingleton.getInstance();
        }

        public int Create(Friend item)
        {
            try
            {
                _db.Friends.Add(item);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteFriend(string Id, string currentUserId)
        {
            try
            {
                var curentItem = _db.Friends.Where(x => x.ApplicationUserId == currentUserId && x.friendId == Id).FirstOrDefault();   //Find(Id);
                //curentItem.isDeleted = false;
                _db.Friends.Remove(curentItem);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public ICollection<Friend> getAll()
        {
            return _db.Friends.Where(x => x.isDeleted == false).ToList();
        }

        public Friend getByItemId(string Id)
        {
            return _db.Friends.Find(Id);
        }

        public int Update(Friend item)
        {
            try
            {
                var currentItem = _db.Friends.Find(item.Id);
                _db.Entry(currentItem).CurrentValues.SetValues(item);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public ICollection<ApplicationUser> findFriendByEmail(string email)
        {
            return _db.Users.Where(x => x.Email.Contains(email)).ToList();//.Where(x => x.Email.Contains(email)).ToList();
        }

        public int Delete(string Id)
        {
            throw new NotImplementedException();
        }
    }
}