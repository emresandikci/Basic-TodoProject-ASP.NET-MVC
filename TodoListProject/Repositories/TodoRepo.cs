using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListProject.Domain;
using TodoListProject.Models;

namespace TodoListProject.Repositories
{
    public class TodoRepo : IRepository<Todo>
    {
        private readonly ApplicationDbContext _db;
        public TodoRepo()
        {
            _db = Tools.TodoSingleton.getInstance();
        }

        public int Create(Todo item)
        {
            try
            {
                _db.Todos.Add(item);
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
                var currentItem = _db.Todos.Find(Id);
                currentItem.isDeleted = true;
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public ICollection<Todo> getAll()
        {
            return _db.Todos.Where(x => x.isDeleted == false).OrderByDescending(x=>x.CreatedDate).ToList();
        }

        public Todo getByItemId(string Id)
        {
            return _db.Todos.Find(Id);
        }

        public int Update(Todo item)
        {
            try
            {
                var currentItem = _db.Todos.Find(item.Id);
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