using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListProject.Domain;
using TodoListProject.Models;

namespace TodoListProject.Repositories
{
    public class TaskRepo : IRepository<Domain.Task>
    {
        private readonly ApplicationDbContext _db;
        public TaskRepo()
        {
            _db = Tools.TodoSingleton.getInstance();
        }

        public int Create(Task item)
        {
            try
            {
                _db.Tasks.Add(item);
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
                var currentItem = _db.Tasks.Find(Id);
                //currentItem.isDeleted = true;
                _db.Tasks.Remove(currentItem);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int CompleteIt(string Id)
        {
            try
            {
                var currentItem = _db.Tasks.Find(Id);
                currentItem.isCompleted = true;
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public ICollection<Task> getAll()
        {
            return _db.Tasks.Where(x => x.isDeleted == false).OrderByDescending(x=>x.CreatedDate).ToList();
        }

        public Task getByItemId(string Id)
        {
            return _db.Tasks.Find(Id);
        }

        public int Update(Task item)
        {
            try
            {
                var currentItem = _db.Tasks.Find(item.Id);
                _db.Entry(currentItem).CurrentValues.SetValues(item);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}