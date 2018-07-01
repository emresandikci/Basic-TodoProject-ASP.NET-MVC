using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListProject.Domain;
using TodoListProject.Models;

namespace TodoListProject.Repositories
{
    public class SharingTodoRepo : IRepository<SharingTodoWithFriend>
    {
        private readonly ApplicationDbContext _db;

        public SharingTodoRepo()
        {
            _db = Tools.TodoSingleton.getInstance();
        }

        public int Create(SharingTodoWithFriend item)
        {
            try
            {
                _db.SharingTodoWithFriends.Add(item);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(string Id)
        {
            try
            {
                var currentItem = _db.SharingTodoWithFriends.Find(Id);
                _db.SharingTodoWithFriends.Remove(currentItem);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public ICollection<SharingTodoWithFriend> getAll()
        {
            return _db.SharingTodoWithFriends.ToList();
        }

        public SharingTodoWithFriend getByItemId(string Id)
        {
            return _db.SharingTodoWithFriends.Find(Id);
        }

        public int Update(SharingTodoWithFriend item)
        {
            try
            {
                var currentItem = _db.SharingTodoWithFriends.Find(item.Id);
                _db.Entry(currentItem).CurrentValues.SetValues(item);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public ICollection<SharingTodoWithFriend> getSharingTodosByApplicationUserAndTodoId(string applicationUserId, string TodoId)
        {
            try
            {
                return _db.SharingTodoWithFriends.Where(x => x.ApplicationUserId == applicationUserId).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}